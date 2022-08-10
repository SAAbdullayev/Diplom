using DiplomLayihe.AppCode.Modules.GrupModule;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities.Membership;
using DiplomLayihe.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin,Admin")]
    public class GrupController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IMediator mediator;

        public GrupController(IMediator mediator, DiplomDbContext db,
            UserManager<DiplomUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [Authorize(Policy = "admin.grup.index")]
        public async Task<IActionResult> Index(GrupAllQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var data = await mediator.Send(new GrupAllQuery());

            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;

            return View(data);
        }

        [HttpGet]
        [Authorize(Policy = "admin.grup.index")]
        public async Task<IActionResult> Index(string search)
        {
            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;

            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            ViewData["Getsearch"] = search;

            var items = from x in db.Gruplar
                        .Where(cc => cc.DeletedById == null) select x;

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(i => i.Name.Contains(search));
            }

            return View(await items.AsNoTracking().ToListAsync());
        }



        [Authorize(Policy = "admin.grup.details")]
        public async Task<IActionResult> Details(GrupSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;
            var entity = await mediator.Send(query);

            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }


        [Authorize(Policy = "admin.grup.create")]
        public async Task<IActionResult> Create()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout; 
            
            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.grup.create")]
        public async Task<IActionResult> Create(GrupCreateCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;
            return View(command);
        }


        [Authorize(Policy = "admin.grup.edit")]
        public async Task<IActionResult> Edit(GrupSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }

            var command = new GrupEditCommand();
            command.Name = entity.Name;

            var ixtisasName = await db.Ixtisaslar.FirstOrDefaultAsync(i => i.Id == entity.ParentIxtisasId);
            command.IxtisasName = ixtisasName.Name;

            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;
            return View(command);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.grup.edit")]
        public async Task<IActionResult> Edit(int id, GrupEditCommand command)
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
            var ixtisas = await db.Ixtisaslar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Ixtisas = ixtisas;
            return View(command);
        }



        [HttpPost]
        [Authorize(Policy = "admin.grup.delete")]
        public async Task<IActionResult> Delete(GrupRemoveCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var response = await mediator.Send(command);

            return Json(response);
        }

        private bool GruplarExists(int id)
        {
            return db.Gruplar.Any(e => e.Id == id);
        }
    }
}
