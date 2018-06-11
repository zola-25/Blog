﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.ViewModels;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        private BlogDbContext _blogContext;
        private IMapper _mapper;

        public AdminController(BlogDbContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(new NewPost());
        }

        [HttpPost]
        public async Task<IActionResult> Admin(NewPost newPost)
        {
            if(_blogContext.Posts.Any(c=>c.Permalink==newPost.Permalink))
            {
                ModelState.AddModelError("Blog.ViewModels.NewPost.Permalink", String.Format("Permalink {0} already exists", newPost.Permalink));
            }

            if (ModelState.IsValid)
            {
                var dbPost = _mapper.Map<Data.Models.Post>(newPost);
                dbPost.CreationDate = DateTime.Now;
                await _blogContext.AddAsync(dbPost);
                await _blogContext.SaveChangesAsync();
                return RedirectToAction("Post", "Home", new { permalink = dbPost.Permalink });
            }
            return View(newPost);
        }
    }
}