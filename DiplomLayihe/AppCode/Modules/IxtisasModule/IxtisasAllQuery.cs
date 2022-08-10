using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using DiplomLayihe.Models.Entities;
using System.Threading.Tasks;
using System.Threading;
using DiplomLayihe.Models.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DiplomLayihe.AppCode.Modules.IxtisasModule
{
    public class IxtisasAllQuery : IRequest<IEnumerable<Ixtisaslar>>
    {


        public class IxtisasAllQueryHandler : IRequestHandler<IxtisasAllQuery, IEnumerable<Ixtisaslar>>
        {
            private readonly DiplomDbContext db;

            public IxtisasAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<Ixtisaslar>> Handle(IxtisasAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
