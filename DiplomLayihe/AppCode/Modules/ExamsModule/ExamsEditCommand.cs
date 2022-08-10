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
    public class ExamsEditCommand : IRequest<Exams>
    {
        public int Id { get; set; }

        public string FennName { get; set; }
        public string GroupName { get; set; }
        public DateTime BashlamaVaxti { get; set; }
        public DateTime BitmeVaxti { get; set; }


        public class ExamsEditCommandHandler : IRequestHandler<ExamsEditCommand, Exams>
        {
            private readonly DiplomDbContext db;

            public ExamsEditCommandHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<Exams> Handle(ExamsEditCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Exams
                       .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                if (entity == null)
                {
                    return null;
                }

                var groupId = await db.Gruplar.FirstOrDefaultAsync(i => i.Name == request.GroupName);
                var fennId = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.Name == request.FennName);
                
                entity.FennId = fennId.Id;
                entity.GroupId = groupId.Id;
                entity.BashlamaVaxti = request.BashlamaVaxti;
                entity.BitmeVaxti = request.BitmeVaxti;
                await db.SaveChangesAsync(cancellationToken);

                return entity;
            }


        }
    }
}
