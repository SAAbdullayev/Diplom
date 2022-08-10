using DiplomLayihe.AppCode.Extensions;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
using DiplomLayihe.Models.Entities.Membership;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Muellim, Telebe, Superadmin")]
    public class KabinetController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IActionContextAccessor ctx;

        public KabinetController(DiplomDbContext db,
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
            }
            else if(User.IsInRole("Muellim"))
            {
                var fenn = await db.TedrisFennleri.FirstOrDefaultAsync(g => g.DeletedById == null && g.Id == userAbout.TedrisFenniId);
                ViewBag.Fenn = fenn;
            }

            var users = await db.Users.ToListAsync();
            ViewBag.Users = users;

            var news = db.LastNewsandEvents.Where(l => l.DeletedById == null).ToList();

            return View(news);
        }


        public async Task<IActionResult> DersCedveli()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.User = userAbout;

            if (User.IsInRole("Telebe"))
            {
                var groups = await db.Gruplar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == userAbout.GrupId);
                var ixtisas = await db.Ixtisaslar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == groups.ParentIxtisasId);
                var lesson = await db.Lessons.Where(i => i.DeletedById == null && i.GroupId == groups.Id).ToListAsync();
                var fenn = await db.TedrisFennleri.Where(g => g.DeletedById == null).ToListAsync();
                var users = await db.Users.ToListAsync();
                ViewBag.Ixtisas = ixtisas;
                ViewBag.Lessons = lesson;
                ViewBag.Fenn = fenn;
                ViewBag.Groups = groups;
                ViewBag.Users = users;
            }
            else if (User.IsInRole("Muellim"))
            {
                var fenn = await db.TedrisFennleri.FirstOrDefaultAsync(g => g.DeletedById == null && g.Id == userAbout.TedrisFenniId);
                var lessons = await db.Lessons.Where(i => i.DeletedById == null && i.FennId == fenn.Id).ToListAsync();
                var groups = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();
                ViewBag.Fenn = fenn;
                ViewBag.Lessons = lessons;
                ViewBag.Groups = groups;

            }


            return View();
        }



        [Authorize(Roles = "Muellim")]
        public async Task<IActionResult> Qiymetlendirme(int Id)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.User = userAbout;

            var fenn = await db.TedrisFennleri.FirstOrDefaultAsync(tf=>tf.Id == userAbout.TedrisFenniId);

            var lesson = await db.Lessons.FirstOrDefaultAsync(l => l.Id == Id);

            var group = await db.Gruplar.FirstOrDefaultAsync(g => g.Id == lesson.GroupId);

            var students = await db.Users.Where(g => g.GrupId == group.Id).ToListAsync();

            var qiymet = await db.Qiymetler.Where(g => g.LessonId == Id).ToListAsync();

            ViewBag.Group = group;
            ViewBag.Lessons = lesson;
            ViewBag.Students = students;
            ViewBag.Fenn = fenn;
            ViewBag.Qiymet = qiymet;



            return View();
        }



        [HttpPost]
        [Authorize(Roles = "Muellim")]
        public async Task<IActionResult> Qiymetlendirme(int selectedMark, int telebeId, int lessonId, int muellimId)
        {

            var telebe = await db.Users.FirstOrDefaultAsync(u => u.Id == telebeId);

            var group = await db.Gruplar.FirstOrDefaultAsync(g => g.Id == telebe.GrupId);

            var qiymet = await db.Qiymetler.FirstOrDefaultAsync(q => q.LessonId == lessonId && q.TelebeId == telebeId && q.TeacherId == muellimId && q.GroupId == group.Id);

            if (qiymet == null)
            {
                Qiymetler newQiymet = new Qiymetler
                {
                    Qiymet = selectedMark,
                    TelebeId = telebeId,
                    LessonId = lessonId,
                    GroupId = group.Id,
                    TeacherId = muellimId,
                    CreatedById = ctx.GetPrincipalId(),
                    CreatedDate = DateTime.UtcNow.AddHours(4)
                };

                await db.Qiymetler.AddAsync(newQiymet);
                await db.SaveChangesAsync();
            }
            else
            {
                qiymet.Qiymet = selectedMark;
                qiymet.CreatedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync();
            }

            return Json(new { status = 200 });
        }


    }
}
