using DiplomLayihe.AppCode.Modules.LessonsModule;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin,Admin")]
    public class LessonsController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IMediator mediator;

        public LessonsController(IMediator mediator, DiplomDbContext db,
            UserManager<DiplomUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.mediator = mediator;
        }
        [Authorize(Policy = "admin.lessons.index")]
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            

            var data = await mediator.Send(new LessonsAllQuery());

            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;
            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;

            return View(data);
        }


        //[HttpGet]
        //[Authorize(Policy = "admin.grup.index")]
        //public async Task<IActionResult> Index(string search)
        //{
        //    var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

        //    ViewBag.Ixtisas = ixtisas;

        //    var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

        //    ViewBag.User = userAbout;
        //    ViewData["Getsearch"] = search;

        //    var items = from x in db.Lessons
        //                .Where(cc => cc.DeletedById == null)
        //                select x;

        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        items = items.Where(i => i.FennId.Contains(search));
        //    }

        //    return View(await items.AsNoTracking().ToListAsync());
        //}



        [Authorize(Policy = "admin.lessons.details")]
        public async Task<IActionResult> Details(LessonsSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;
            var entity = await mediator.Send(query);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }


        [Authorize(Policy = "admin.lessons.create")]
        public async Task<IActionResult> Create()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.lessons.create")]
        public async Task<IActionResult> Create(LessonsCreateCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;
            return View(command);
        }



        [Authorize(Policy = "admin.lessons.edit")]
        public async Task<IActionResult> Edit(LessonsSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var Gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = Gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;

            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }


            var command = new LessonsEditCommand();
            var gruplar = await db.Gruplar.FirstOrDefaultAsync(i => i.Id == entity.GroupId);
            var fennName = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.Id == entity.FennId);
            var muellim = await db.Users.FirstOrDefaultAsync(i => i.TedrisFenniId == entity.FennId);
            command.FennName = fennName.Name;
            command.GroupName = gruplar.Name;
            command.MuellimName = muellim.Name;
            command.BashlamaVaxti = entity.BashlamaVaxti;
            command.BitmeVaxti = entity.BitmeVaxti;

            return View(command);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.lessons.edit")]
        public async Task<IActionResult> Edit(int id, LessonsEditCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;
            if (id != command.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);

                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }



        [HttpPost]
        [Authorize(Policy = "admin.lessons.delete")]
        public async Task<IActionResult> Delete(LessonsRemoveCommand command)
        {
            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;

            var muellimler = await db.Users.Where(i => i.TedrisFenniId != 0).ToListAsync();

            ViewBag.Muellimler = muellimler;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var response = await mediator.Send(command);

            return Json(response);
        }

        private bool LessonsExists(int id)
        {
            return db.Lessons.Any(e => e.Id == id);
        }
    }
}
