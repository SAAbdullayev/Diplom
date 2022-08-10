using DiplomLayihe.AppCode.Modules.ExamsModule;
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
    public class ExamsController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IMediator mediator;

        public ExamsController(IMediator mediator, DiplomDbContext db,
            UserManager<DiplomUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.mediator = mediator;
        }
        [Authorize(Policy = "admin.exams.index")]
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;



            var data = await mediator.Send(new ExamsAllQuery());

            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = gruplar;
            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;

            return View(data);
        }


        [Authorize(Policy = "admin.exams.details")]
        public async Task<IActionResult> Details(ExamsSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var entity = await mediator.Send(query);



            var gruplar = await db.Gruplar.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == entity.GroupId);

            ViewBag.Gruplar = gruplar;
            var fenn = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.DeletedById == null && i.Id == entity.FennId);

            ViewBag.Fenn = fenn;

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }


        [Authorize(Policy = "admin.exams.create")]
        public async Task<IActionResult> Create()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();
            ViewBag.Gruplar = gruplar;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();
            ViewBag.Fenn = fenn;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.exams.create")]
        public async Task<IActionResult> Create(ExamsCreateCommand command)
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

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;
            return View(command);
        }


        [Authorize(Policy = "admin.exams.edit")]
        public async Task<IActionResult> Edit(ExamsSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var Gruplar = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Gruplar = Gruplar;

            var fenn = await db.TedrisFennleri.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Fenn = fenn;

            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }

            var command = new ExamsEditCommand();
            var gruplar = await db.Gruplar.FirstOrDefaultAsync(i => i.Id == entity.GroupId);
            var fennName = await db.TedrisFennleri.FirstOrDefaultAsync(i => i.Id == entity.FennId);
            command.FennName = fennName.Name;
            command.GroupName = gruplar.Name;
            command.BashlamaVaxti = entity.BashlamaVaxti;
            command.BitmeVaxti = entity.BitmeVaxti;

            return View(command);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.exams.edit")]
        public async Task<IActionResult> Edit(int id, ExamsEditCommand command)
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
        [Authorize(Policy = "admin.exams.delete")]
        public async Task<IActionResult> Delete(ExamsRemoveCommand command)
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

        private bool ExamsExists(int id)
        {
            return db.Exams.Any(e => e.Id == id);
        }
    }
}
