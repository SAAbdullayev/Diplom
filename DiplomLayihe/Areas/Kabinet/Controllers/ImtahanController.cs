using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Areas.Kabinet.Controllers
{
    [Area("Kabinet")]
    public class ImtahanController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IActionContextAccessor ctx;

        public ImtahanController(DiplomDbContext db,
            UserManager<DiplomUser> userManager,
            IActionContextAccessor ctx)
        {
            this.db = db;
            this.userManager = userManager;
            this.ctx = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.User = userAbout;


            var groups = await db.Gruplar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == userAbout.GrupId);
            var ixtisas = await db.Ixtisaslar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == groups.ParentIxtisasId);
            var exams = await db.Exams.Where(i => i.DeletedById == null && i.GroupId == groups.Id).ToListAsync();
            var fenn = await db.TedrisFennleri.Where(g => g.DeletedById == null).ToListAsync();
            var users = await db.Users.ToListAsync();
            ViewBag.Ixtisas = ixtisas;
            ViewBag.Exams = exams;
            ViewBag.Fenn = fenn;
            ViewBag.Groups = groups;
            ViewBag.Users = users;

            return View();
        }
    }
}
