using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.ExamsModule
{
    public class ExamsAllQuery : IRequest<IEnumerable<Exams>>
    {


        public class ExamsAllQueryHandler : IRequestHandler<ExamsAllQuery, IEnumerable<Exams>>
        {
            private readonly DiplomDbContext db;

            public ExamsAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<Exams>> Handle(ExamsAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Exams
                          .Where(ah => ah.DeletedById == null).ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
