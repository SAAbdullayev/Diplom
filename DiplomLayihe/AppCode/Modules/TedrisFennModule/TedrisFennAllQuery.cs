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
    public class TedrisFennAllQuery : IRequest<IEnumerable<TedrisFennleri>>
    {

        public class TedrisFennAllQueryHandler : IRequestHandler<TedrisFennAllQuery, IEnumerable<TedrisFennleri>>
        {
            private readonly DiplomDbContext db;

            public TedrisFennAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<TedrisFennleri>> Handle(TedrisFennAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.TedrisFennleri
                            .Where(ah => ah.DeletedById == null).ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
