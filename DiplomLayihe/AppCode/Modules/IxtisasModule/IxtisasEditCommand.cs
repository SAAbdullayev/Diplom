using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.IxtisasModule
{
    public class IxtisasEditCommand : IRequest<Ixtisaslar>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public class IxtisasEditCommandHandler : IRequestHandler<IxtisasEditCommand, Ixtisaslar>
        {
            private readonly DiplomDbContext db;

            public IxtisasEditCommandHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Ixtisaslar> Handle(IxtisasEditCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Ixtisaslar
                       .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                if (entity == null)
                {
                    return null;
                }

                entity.Name = request.Name;
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
