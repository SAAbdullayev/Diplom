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
    public class GrupAllQuery : IRequest<IEnumerable<Gruplar>>
    {

        public class GrupAllQueryHandler : IRequestHandler<GrupAllQuery, IEnumerable<Gruplar>>
        {
            private readonly DiplomDbContext db;

            public GrupAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<Gruplar>> Handle(GrupAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Gruplar
                          .Where(ah => ah.DeletedById == null).ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
