using DiplomLayihe.AppCode.Extensions;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.IxtisasModule
{
    public class IxtisasCreateCommand : IRequest<Ixtisaslar>
    {
        public string Name { get; set; }


        public class IxtisasCreateCommandHandler : IRequestHandler<IxtisasCreateCommand, Ixtisaslar>
        {
            private readonly DiplomDbContext db;
            private readonly IActionContextAccessor ctx;

            public IxtisasCreateCommandHandler(DiplomDbContext db,
                IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Ixtisaslar> Handle(IxtisasCreateCommand request, CancellationToken cancellationToken)
            {
                var ixtisas = new Ixtisaslar();
                ixtisas.Name = request.Name;
                ixtisas.CreatedDate = DateTime.UtcNow.AddHours(4);
                ixtisas.CreatedById = ctx.GetPrincipalId();


                await db.Ixtisaslar.AddAsync(ixtisas, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return ixtisas;
            }
        }
    }
}
