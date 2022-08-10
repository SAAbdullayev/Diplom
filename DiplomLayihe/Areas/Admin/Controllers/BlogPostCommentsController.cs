﻿using DiplomLayihe.AppCode.Modules.BlogPostCommentModule;
using DiplomLayihe.Models.DataContext;
using DiplomLayihe.Models.Entities;
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
    public class BlogPostCommentsController : Controller
    {
        private readonly DiplomDbContext db;
        private readonly UserManager<DiplomUser> userManager;
        private readonly IMediator mediator;

        public BlogPostCommentsController(IMediator mediator, DiplomDbContext db,
            UserManager<DiplomUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.mediator = mediator;
        }


        [Authorize(Policy = "admin.blogpostcomments.index")]
        public async Task<IActionResult> Index()
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var data = await mediator.Send(new BlogPostCommentsAllQuery());
            return View(data);
        }


        [HttpGet]
        [Authorize(Policy = "admin.blogpostcomments.index")]
        public async Task<IActionResult> Index(string search)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            ViewData["Getsearch"] = search;

            var items = from x in db.BlogPostComments.Where(cc => cc.DeletedById == null) select x;

            if (!String.IsNullOrEmpty(search))
            {
                items = items.Where(i => i.Name.Contains(search) || i.Email.Contains(search) || i.Comment.Contains(search));
            }

            return View(await items.AsNoTracking().ToListAsync());
        }


        [Authorize(Policy = "admin.blogpostcomments.details")]
        public async Task<IActionResult> Details(BlogPostCommentsSingleQuery query)
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


        //[HttpPost]
        //[Authorize(Policy = "admin.blogpostcomments.create")]
        //public async Task<IActionResult> Create(int id, string name, string email, string message)
        //{
        //    var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

        //    ViewBag.User = userAbout;

        //    BlogPostComments newComment = new BlogPostComments
        //    {
        //        ParentId = id,
        //        Name = name,
        //        Email = email,
        //        Comment = message

        //    };
        //    await db.BlogPostComments.AddAsync(newComment);
        //    await db.SaveChangesAsync();


        //    return Json(new { status = 200 });
        //}




        [HttpPost]
        [Authorize(Policy = "admin.blogpostcomments.delete")]
        public async Task<IActionResult> Delete(BlogPostCommentsRemoveCommand command)
        {
            var userAbout = await userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.User = userAbout;
            var response = await mediator.Send(command);

            return Json(response);
        }

        private bool BlogPostCommentsExists(int id)
        {
            return db.BlogPostComments.Any(e => e.Id == id);
        }

    }
}
