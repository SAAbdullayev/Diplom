using DiplomLayihe.AppCode.Extensions;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.TedrisFennModule
{
    public class TedrisFennCreateCommand : IRequest<TedrisFennleri>
    {
        public string Name { get; set; }



        public class TedrisFennCreateCommandHandler : IRequestHandler<TedrisFennCreateCommand, TedrisFennleri>
        {
            private readonly DiplomDbContext db;
            private readonly IWebHostEnvironment env;
            private readonly IActionContextAccessor ctx;
            public TedrisFennCreateCommandHandler(DiplomDbContext db,
                IWebHostEnvironment env,
                IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<TedrisFennleri> Handle(TedrisFennCreateCommand request, CancellationToken cancellationToken)
            {
                var tedrisFennleri = new TedrisFennleri();
                tedrisFennleri.Name = request.Name;
                tedrisFennleri.CreatedDate = DateTime.UtcNow.AddHours(4);
                tedrisFennleri.CreatedById = ctx.GetPrincipalId();


                await db.TedrisFennleri.AddAsync(tedrisFennleri, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return tedrisFennleri;
            }
        }
    }
}
