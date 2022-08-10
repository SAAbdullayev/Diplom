using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.GrupModule
{
    public class GrupEditCommand : IRequest<Gruplar>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IxtisasName { get; set; }

        public class GrupEditCommandHandler : IRequestHandler<GrupEditCommand, Gruplar>
        {
            private readonly DiplomDbContext db;

            public GrupEditCommandHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Gruplar> Handle(GrupEditCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Gruplar
                       .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                if (entity == null)
                {
                    return null;
                }

                var ixtisasId = await db.Ixtisaslar.FirstOrDefaultAsync(i => i.Name == request.IxtisasName && i.DeletedById == null);

                entity.Name = request.Name;
                entity.ParentIxtisasId = ixtisasId.Id;
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
