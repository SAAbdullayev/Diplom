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
    public class ExamsSingleQuery : IRequest<Exams>
    {
        public int Id { get; set; }


        public class ExamsSingleQueryHandler : IRequestHandler<ExamsSingleQuery, Exams>
        {
            private readonly DiplomDbContext db;

            public ExamsSingleQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Exams> Handle(ExamsSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Exams
                     .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                return model;
            }
        }
    }
}
