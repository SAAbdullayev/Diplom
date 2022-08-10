using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.LessonsModule
{
    public class LessonsAllQuery : IRequest<IEnumerable<Lessons>>
    {


        public class LessonsAllQueryHandler : IRequestHandler<LessonsAllQuery, IEnumerable<Lessons>>
        {
            private readonly DiplomDbContext db;

            public LessonsAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<Lessons>> Handle(LessonsAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Lessons
                          .Where(ah => ah.DeletedById == null).ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
