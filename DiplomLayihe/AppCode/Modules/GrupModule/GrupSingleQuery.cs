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
    public class GrupSingleQuery : IRequest<Gruplar>
    {
        public int Id { get; set; }

        public class GrupSingleQueryHandler : IRequestHandler<GrupSingleQuery, Gruplar>
        {
            private readonly DiplomDbContext db;

            public GrupSingleQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Gruplar> Handle(GrupSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Gruplar
                      .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                return model;
            }
        }
    }
}
