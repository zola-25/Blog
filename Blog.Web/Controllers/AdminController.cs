using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.ViewModels;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> BlogPost(NewPost newPost)
        {
            if(_blogContext.Posts.Any(c=>c.UrlSegment==newPost.UrlSegment))
            {
                ModelState.AddModelError("Blog.ViewModels.NewPost.Permalink", String.Format("Url Segment {0} already exists", newPost.UrlSegment));
            }

            if (ModelState.IsValid)
            {
                var dbPost = _mapper.Map<Data.Models.Post>(newPost);
                await _blogContext.AddAsync(dbPost);
                await _blogContext.SaveChangesAsync();
                return Redirect($"/Post/{dbPost.UrlSegment}");
            }
            return View(newPost);
        }
    }
}