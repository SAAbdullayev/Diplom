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

namespace DiplomLayihe.AppCode.Modules.LessonsModule
{
    public class LessonsCreateCommand : IRequest<Lessons>
    {
        public string GroupName { get; set; }
        public string TeacherName { get; set; }
        public string FennName { get; set; }
        public DateTime BashlamaVaxti { get; set; }
        public DateTime BitmeVaxti { get; set; }

        public class LessonsCreateCommandHandler : IRequestHandler<LessonsCreateCommand, Lessons>
        {
            private readonly DiplomDbContext db;
            private readonly IActionContextAccessor ctx;

            public LessonsCreateCommandHandler(DiplomDbContext db,
                IActionContextAccessor ctx)
            {
                this.db = db;
                this.ctx = ctx;
            }
            public async Task<Lessons> Handle(LessonsCreateCommand request, CancellationToken cancellationToken)
            {
                var groupId = await db.Gruplar.FirstOrDefaultAsync(i => i.Name == request.GroupName);
                var fennId = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.Name == request.FennName);
                var teacherId = await db.Users.FirstOrDefaultAsync(i => i.TedrisFenniId == fennId.Id);

                var lesson = new Lessons();
                lesson.FennId = fennId.Id;
                lesson.GroupId = groupId.Id;
                lesson.MuellimId = teacherId.Id;
                lesson.BashlamaVaxti = request.BashlamaVaxti;
                lesson.BitmeVaxti = request.BitmeVaxti;
                lesson.CreatedDate = DateTime.UtcNow.AddHours(4);
                lesson.CreatedById = ctx.GetPrincipalId();

                


                await db.Lessons.AddAsync(lesson, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return lesson;
            }
        }
    }
}
