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
    public class LessonsEditCommand : IRequest<Lessons>
    {
        public int Id { get; set; }

        public string FennName { get; set; }
        public string MuellimName { get; set; }
        public string GroupName { get; set; }
        public DateTime BashlamaVaxti { get; set; }
        public DateTime BitmeVaxti { get; set; }

        public class LessonsEditCommandHandler : IRequestHandler<LessonsEditCommand, Lessons>
        {
            private readonly DiplomDbContext db;

            public LessonsEditCommandHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Lessons> Handle(LessonsEditCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Lessons
                       .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                if (entity == null)
                {
                    return null;
                }

                var groupId = await db.Gruplar.FirstOrDefaultAsync(i => i.Name == request.GroupName);
                var fennId = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.Name == request.FennName);
                var teacherId = await db.Users.FirstOrDefaultAsync(i => i.TedrisFenniId == fennId.Id && i.Name == request.MuellimName);

                entity.FennId = fennId.Id;
                entity.GroupId = groupId.Id;
                entity.MuellimId = teacherId.Id;
                entity.BashlamaVaxti = request.BashlamaVaxti;
                entity.BitmeVaxti = request.BitmeVaxti;
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
