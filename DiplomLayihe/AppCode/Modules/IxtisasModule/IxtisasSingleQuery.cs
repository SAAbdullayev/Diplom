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
    public class IxtisasSingleQuery : IRequest<Ixtisaslar>
    {
        public int Id { get; set; }

        public class IxtisasSingleQueryHandler : IRequestHandler<IxtisasSingleQuery, Ixtisaslar>
        {
            private readonly DiplomDbContext db;

            public IxtisasSingleQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Ixtisaslar> Handle(IxtisasSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Ixtisaslar
                      .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                return model;
            }
        }
    }
}
