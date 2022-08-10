using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities.Membership;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Modules.AccountModule
{
    public class MuellimAllQuery : IRequest<IEnumerable<DiplomUser>>
    {
        public class MuellimAllQueryHandler : IRequestHandler<MuellimAllQuery, IEnumerable<DiplomUser>>
        {
            private readonly DiplomDbContext db;

            public MuellimAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<DiplomUser>> Handle(MuellimAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Users.Where(u => u.TedrisFenniId != 0)
                    .ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
