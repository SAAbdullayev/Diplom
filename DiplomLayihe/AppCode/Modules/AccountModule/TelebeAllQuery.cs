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
    public class TelebeAllQuery : IRequest<IEnumerable<DiplomUser>>
    {


        public class TelebeAllQueryHandler : IRequestHandler<TelebeAllQuery, IEnumerable<DiplomUser>>
        {
            private readonly DiplomDbContext db;

            public TelebeAllQueryHandler(DiplomDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<DiplomUser>> Handle(TelebeAllQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Users.Where(u=>u.GrupId != 0)
                    .ToListAsync(cancellationToken);

                return model;
            }
        }
    }
}
