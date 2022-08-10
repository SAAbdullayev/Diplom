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
    public class TedrisFennSingleQuery : IRequest<TedrisFennleri>
    {
        public int Id { get; set; }


        public class TedrisFennSingleQueryHandler : IRequestHandler<TedrisFennSingleQuery, TedrisFennleri>
        {
            private readonly DiplomDbContext db;

            public TedrisFennSingleQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<TedrisFennleri> Handle(TedrisFennSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.TedrisFennleri
                     .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                return model;
            }
        }
    }
}
