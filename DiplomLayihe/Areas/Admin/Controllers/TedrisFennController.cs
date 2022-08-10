using DiplomLayihe.AppCode.Modules.TedrisFennModule;
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
    public class TedrisFennController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IMediator mediator;

        public TedrisFennController(IMediator mediator, DiplomDbContext db,
            UserManager<DiplomUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.tedrisfenn.index")]
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var data = await mediator.Send(new TedrisFennAllQuery());
            return View(data);
        }


        [HttpGet]
        [Authorize(Policy = "admin.tedrisfenn.index")]
        public async Task<IActionResult> Index(string search)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            ViewData["Getsearch"] = search;

            var items = from x in db.TedrisFennleri.Where(cc => cc.DeletedById == null) select x;

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(i => i.Name.Contains(search));
            }

            return View(await items.AsNoTracking().ToListAsync());
        }



        [Authorize(Policy = "admin.tedrisfenn.details")]
        public async Task<IActionResult> Details(TedrisFennSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var entity = await mediator.Send(query);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }


        [Authorize(Policy = "admin.tedrisfenn.create")]
        public async Task<IActionResult> Create()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.tedrisfenn.create")]
        public async Task<IActionResult> Create(TedrisFennCreateCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }



        [Authorize(Policy = "admin.tedrisfenn.edit")]
        public async Task<IActionResult> Edit(TedrisFennSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }

            var command = new TedrisFennEditCommand();
            command.Name = entity.Name;
            return View(command);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.tedrisfenn.edit")]
        public async Task<IActionResult> Edit(int id, TedrisFennEditCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
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
        [Authorize(Policy = "admin.tedrisfenn.delete")]
        public async Task<IActionResult> Delete(TedrisFennRemoveCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var response = await mediator.Send(command);

            return Json(response);
        }

        private bool TedrisFennleriExists(int id)
        {
            return db.TedrisFennleri.Any(e => e.Id == id);
        }
    }
}
