using DiplomLayihe.AppCode.Extensions;
using DiplomLayihe.Models.DataContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Providers
{
    public class DiplomClaimProvider : IClaimsTransformation
    {
        private readonly DiplomDbContext db;

        public DiplomClaimProvider(DiplomDbContext db)
        {
            this.db = db;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated && principal.Identity is ClaimsIdentity identity)
            {
                int userId = principal.GetPrincipalId().Value;


                var user = db.Users.FirstOrDefault(u => u.Id == userId);

                if (user == null)
                    goto l1;


                var claim = identity.FindFirst(c => !c.Type.Equals(ClaimTypes.NameIdentifier)
                && !c.Type.Equals(ClaimTypes.Name)
                && !c.Type.Equals(ClaimTypes.Email));

                while (claim != null)
                {
                    identity.RemoveClaim(claim);

                    claim = identity.FindFirst(c => !c.Type.Equals(ClaimTypes.NameIdentifier)
                                                        && !c.Type.Equals(ClaimTypes.Name)
                                                        && !c.Type.Equals(ClaimTypes.Email));
                }


                if (!string.IsNullOrWhiteSpace(user.Name) && !string.IsNullOrWhiteSpace(user.Surname))
                {
                    identity.AddClaim(new Claim("FullName", $"{user.Name}  {user.Surname}"));
                }
                else if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                {
                    identity.AddClaim(new Claim("FullName", $"{user.PhoneNumber}"));
                }
                else
                {
                    identity.AddClaim(new Claim("FullName", $"{user.Email}"));
                }


                #region Roles

                var roLes = await (from ur in db.UserRoles
                                   join r in db.Roles on ur.RoleId equals r.Id
                                   where ur.UserId == userId
                                   select r.Name).ToArrayAsync();


                foreach (var role in roLes)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }


                #endregion


                var cLaims = await (from rc in db.RoleClaims
                                    join ur in db.UserRoles on rc.RoleId equals ur.RoleId
                                    where ur.UserId == userId && rc.ClaimValue.Equals("1")
                                    select rc.ClaimType)
                              .Union(from uc in db.UserClaims
                                     where uc.UserId == userId && uc.ClaimValue.Equals("1")
                                     select uc.ClaimType)
                              .ToListAsync();


                foreach (var cLaimName in cLaims)
                {
                    identity.AddClaim(new Claim(cLaimName, "1"));
                }
            }



        l1:
            return principal;
        }
    
    }
}
