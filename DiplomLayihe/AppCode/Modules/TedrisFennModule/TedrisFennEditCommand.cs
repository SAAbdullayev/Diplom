using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.TedrisFennModule
{
    public class TedrisFennEditCommand : IRequest<TedrisFennleri>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public class TedrisFennEditCommandHandler : IRequestHandler<TedrisFennEditCommand, TedrisFennleri>
        {
            private readonly DiplomDbContext db;

            public TedrisFennEditCommandHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<TedrisFennleri> Handle(TedrisFennEditCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.TedrisFennleri
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
