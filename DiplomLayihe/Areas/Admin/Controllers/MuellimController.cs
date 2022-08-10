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
    public class MuellimController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly IMediator mediator;
        private readonly IWebHostEnvironment env;
        private readonly IActionContextAccessor ctx;
        private readonly UserManager<DiplomUser> userManager;
        private readonly SignInManager<DiplomUser> signInManager;

        public MuellimController(DiplomDbContext db,
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

            var fenn = db.TedrisFennleri.Where(g => g.DeletedById == null);

            ViewBag.Fenn = fenn;

            var data = await mediator.Send(new MuellimAllQuery());

            return View(data);
        }



        public async Task<IActionResult> Details(int id)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var fenn = db.TedrisFennleri.Where(g => g.DeletedById == null);

            ViewBag.Fenn = fenn;

            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Id == id);


            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> RegisterForTeacher()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var fenn = db.TedrisFennleri.Where(g => g.DeletedById == null);

            ViewBag.Fenn = fenn;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterForTeacher(RegisterFormModel model)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;

            var fenn = db.TedrisFennleri.Where(g => g.DeletedById == null);

            ViewBag.Fenn = fenn;

            if (model?.file == null)
            {
                ctx.AddModelError("ProfileImg", "Fayl Sechilmeyib!");
            }


            if (ctx.ModelIsValid())
            {
                string fileExtension = Path.GetExtension(model.file.FileName);

                string name = $"userTeacher-{Guid.NewGuid()}{fileExtension}";
                string physicalPath = Path.Combine(env.ContentRootPath, "wwwroot", "photouploads", "images", name);

                using (FileStream fs = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                {
                    await model.file.CopyToAsync(fs);
                }

                var fennId = await db.TedrisFennleri.FirstOrDefaultAsync(g => g.DeletedById == null && g.Name == model.FennName);


                var user = new DiplomUser
                {
                    ProfileImg = name,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username,
                    EmailConfirmed = true,
                    TedrisFenniId = fennId.Id,
                    PhoneNumberConfirmed = true
                };



                var result = await userManager.CreateAsync(user, model.Password);

                await userManager.AddToRoleAsync(user, "Muellim");

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }


            }

            return View(model);
        }
    }
}
