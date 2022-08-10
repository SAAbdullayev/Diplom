using DiplomLayihe.AppCode.Modules.AccountModule;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities.Membership;
using DiplomLayihe.Models.FormModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using DiplomLayihe.AppCodee.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin,Admin")]
    public class TelebeController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly IMediator mediator;
        private readonly IWebHostEnvironment env;
        private readonly IActionContextAccessor ctx;
        private readonly UserManager<DiplomUser> userManager;
        private readonly SignInManager<DiplomUser> signInManager;

        public TelebeController(DiplomDbContext db,
            IMediator mediator,
            IWebHostEnvironment env,
            IActionContextAccessor ctx,
            UserManager<DiplomUser> userManager,
            SignInManager<DiplomUser> signInManager)
        {
            this.db = db;
            this.mediator = mediator;
            this.env = env;
            this.ctx = ctx;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var group = db.Gruplar.Where(g => g.DeletedById == null);

            ViewBag.Groups = group;

            var data = await mediator.Send(new TelebeAllQuery());

            return View(data);
        }


        public async Task<IActionResult> Details(int id)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Id == id);


            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }






        public async Task<IActionResult> RegisterForStudent()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var groups = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Groups = groups;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterForStudent(RegisterFormModel model)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var groups = await db.Gruplar.Where(i => i.DeletedById == null).ToListAsync();

            ViewBag.Groups = groups;

            if (model?.file == null)
            {
                ctx.AddModelError("ProfileImg", "Fayl Sechilmeyib!");
            }


            if (ctx.ModelIsValid())
            {
                string fileExtension = Path.GetExtension(model.file.FileName);

                string name = $"userStudent-{Guid.NewGuid()}{fileExtension}";
                string physicalPath = Path.Combine(env.ContentRootPath, "wwwroot", "photouploads", "images", name);

                using (FileStream fs = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                {
                    await model.file.CopyToAsync(fs);
                }

                var groupId = await db.Gruplar.FirstOrDefaultAsync(g => g.DeletedById == null && g.Name == model.GrupName);


                var user = new DiplomUser
                {
                    ProfileImg = name,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username,
                    EmailConfirmed = true,
                    GrupId = groupId.Id,
                    PhoneNumberConfirmed = true
                };



                var result = await userManager.CreateAsync(user, model.Password);

                await userManager.AddToRoleAsync(user, "Telebe");

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }


            }

            return View(model);
        }
    }
}
