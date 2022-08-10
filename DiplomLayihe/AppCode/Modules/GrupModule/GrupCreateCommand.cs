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

namespace DiplomLayihe.AppCode.Modules.GrupModule
{
    public class GrupCreateCommand : IRequest<Gruplar>
    {
        public string Name { get; set; }
        public string IxtisasName { get; set; }

        public class GrupCreateCommandHandler : IRequestHandler<GrupCreateCommand, Gruplar>
        {
            private readonly DiplomDbContext db;
            private readonly IActionContextAccessor ctx;

            public GrupCreateCommandHandler(DiplomDbContext db,
                IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Gruplar> Handle(GrupCreateCommand request, CancellationToken cancellationToken)
            {
                var group = new Gruplar();
                group.Name = request.Name;
                group.CreatedDate = DateTime.UtcNow.AddHours(4);
                group.CreatedById = ctx.GetPrincipalId();

                var ixtisasId = db.Ixtisaslar.FirstOrDefault(i => i.Name == request.IxtisasName);

                group.ParentIxtisasId = ixtisasId.Id;


                await db.Gruplar.AddAsync(group, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return group;
            }
        }
    }
}
