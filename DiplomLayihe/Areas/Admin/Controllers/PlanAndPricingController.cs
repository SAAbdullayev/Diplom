﻿using DiplomLayihe.AppCode.Modules.PlanAndPricingModule;
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
    public class PlanAndPricingController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IMediator mediator;

        public PlanAndPricingController(IMediator mediator, DiplomDbContext db,
            UserManager<DiplomUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.mediator = mediator;
        }


        [Authorize(Policy = "admin.planandpricing.index")]
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var data = await mediator.Send(new PlanAndPricingAllQuery());
            return View(data);
        }


        [HttpGet]
        [Authorize(Policy = "admin.planandpricing.index")]
        public async Task<IActionResult> Index(string search)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            ViewData["Getsearch"] = search;

            var items = from x in db.PlanAndPricing.Where(cc => cc.DeletedById == null) select x;

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(i => i.PlanType.Contains(search) || i.PlanTime.Contains(search));
            }

            return View(await items.AsNoTracking().ToListAsync());
        }


        [Authorize(Policy = "admin.planandpricing.details")]
        public async Task<IActionResult> Details(PlanAndPricingSingleQuery query)
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


        [Authorize(Policy = "admin.planandpricing.create")]
        public async Task<IActionResult> Create()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.planandpricing.create")]
        public async Task<IActionResult> Create(PlanAndPricingCreateCommand command)
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


        [Authorize(Policy = "admin.planandpricing.edit")]
        public async Task<IActionResult> Edit(PlanAndPricingSingleQuery query)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }

            var command = new PlanAndPricingEditCommand();
            command.PlanType = entity.PlanType;
            command.PlanPrice = entity.PlanPrice;
            command.PlanTime = entity.PlanTime;
            return View(command);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.planandpricing.edit")]
        public async Task<IActionResult> Edit(int id, PlanAndPricingEditCommand command)
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
        [Authorize(Policy = "admin.planandpricing.delete")]
        public async Task<IActionResult> Delete(PlanAndPricingRemoveCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var response = await mediator.Send(command);

            return Json(response);
        }

        private bool PlanAndPricingExists(int id)
        {
            return db.PlanAndPricing.Any(e => e.Id == id);
        }

    }
}
