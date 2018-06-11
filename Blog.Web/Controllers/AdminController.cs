using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.ViewModels;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View(new NewPost());
        }

        [HttpPost]
        public async Task<IActionResult> Admin(NewPost newPost)
        {
            if(_bloggingContext.Posts.Any(c=>c.Permalink==newPost.Permalink))
            {
                ModelState.AddModelError("Blog.ViewModels.NewPost.Permalink", String.Format("Permalink {0} already exists", newPost.Permalink));
            }

            if (ModelState.IsValid)
            {
                var dbPost = _mapper.Map<Data.Models.Post>(newPost);
                dbPost.CreationDate = DateTime.Now;
                await _bloggingContext.AddAsync(dbPost);
                await _bloggingContext.SaveChangesAsync();
                return RedirectToAction("Post", new { permalink = dbPost.Permalink });
            }
            return View(newPost);
        }
    }
}