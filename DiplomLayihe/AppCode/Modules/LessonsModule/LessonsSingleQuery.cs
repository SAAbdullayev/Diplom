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
    public class LessonsSingleQuery : IRequest<Lessons>
    {
        public int Id { get; set; }


        public class LessonsSingleQueryHandler : IRequestHandler<LessonsSingleQuery, Lessons>
        {
            private readonly DiplomDbContext db;

            public LessonsSingleQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Lessons> Handle(LessonsSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Lessons
                      .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                return model;
            }
        }
    }
}
