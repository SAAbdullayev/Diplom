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
    public class JurnalController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IActionContextAccessor ctx;

        public JurnalController(DiplomDbContext db,
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

            

            if (User.IsInRole("Telebe"))
            {
                var groups = await db.Gruplar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == userAbout.GrupId);
                ViewBag.Groups = groups;
                var ixtisas = await db.Ixtisaslar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == groups.ParentIxtisasId);
                ViewBag.Ixtisas = ixtisas;
                var lessons = await db.Lessons.Where(l => l.GroupId == groups.Id).ToListAsync();
                ViewBag.Lessons = lessons;
                var fenn = await db.TedrisFennleri.Where(g => g.DeletedById == null).ToListAsync();
                ViewBag.Fenn = fenn;
            }
            else if (User.IsInRole("Muellim"))
            {
                var fenn = await db.TedrisFennleri.FirstOrDefaultAsync(g => g.DeletedById == null && g.Id == userAbout.TedrisFenniId);
                ViewBag.Fenn = fenn;
                var groups = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();
                ViewBag.Groups = groups;
                var lessons = await db.Lessons.Where(l => l.MuellimId == userAbout.Id).ToListAsync();
                ViewBag.Lessons = lessons;
            }
            return View();
        }

        public async Task<IActionResult> Jurnaletraflibaxish(int Id)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.User = userAbout;

            var lesson = await db.Lessons.FirstOrDefaultAsync(l => l.Id == Id);

            var groups = await db.Gruplar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == lesson.GroupId);
            ViewBag.Groups = groups;


            if (User.IsInRole("Telebe"))
            {
                var ixtisas = await db.Ixtisaslar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == groups.ParentIxtisasId);
                ViewBag.Ixtisas = ixtisas;
                var qiymet = await db.Qiymetler.FirstOrDefaultAsync(i => i.LessonId == Id && i.TelebeId == userAbout.Id);
                ViewBag.Qiymet = qiymet;
            }
            else if (User.IsInRole("Muellim"))
            {
                var fenn = await db.TedrisFennleri.FirstOrDefaultAsync(g => g.DeletedById == null && g.Id == userAbout.TedrisFenniId);
                ViewBag.Fenn = fenn;
                var telebeler = await db.Users.Where(i => i.GrupId == groups.Id).ToListAsync();
                ViewBag.Students = telebeler;
                var qiymet = await db.Qiymetler.Where(i => i.LessonId == Id).ToListAsync();
                ViewBag.Qiymet = qiymet;
                
            }

            return View();
        }
    }
}
