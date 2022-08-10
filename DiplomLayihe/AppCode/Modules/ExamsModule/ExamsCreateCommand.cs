using DiplomLayihe.AppCode.Extensions;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.ExamsModule
{
    public class ExamsCreateCommand : IRequest<Exams>
    {
        public string GroupName { get; set; }
        public string FennName { get; set; }
        public DateTime BashlamaVaxti { get; set; }
        public DateTime BitmeVaxti { get; set; }

        public class ExamsCreateCommandHandler : IRequestHandler<ExamsCreateCommand, Exams>
        {
            private readonly DiplomDbContext db;
            private readonly IActionContextAccessor ctx;

            public ExamsCreateCommandHandler(DiplomDbContext db,
                IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Exams> Handle(ExamsCreateCommand request, CancellationToken cancellationToken)
            {
                var groupId = await db.Gruplar.FirstOrDefaultAsync(i => i.Name == request.GroupName);
                var fennId = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.Name == request.FennName);

                var exams = new Exams();
                exams.FennId = fennId.Id;
                exams.GroupId = groupId.Id;
                exams.BashlamaVaxti = request.BashlamaVaxti;
                exams.BitmeVaxti = request.BitmeVaxti;
                exams.CreatedDate = DateTime.UtcNow.AddHours(4);
                exams.CreatedById = ctx.GetPrincipalId();




                await db.Exams.AddAsync(exams, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return exams;
            }
        }
    }
}
