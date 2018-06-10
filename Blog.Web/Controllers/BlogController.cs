using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.ViewModels;
using System;
using System.Xml;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic;
using Blog.Services;
using Blog.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDbContext _bloggingContext;
        private ISnapshotText _snapshotText;
        private IMapper _mapper;

        public BlogController(BlogDbContext blogContext, ISnapshotText snapshotText, IMapper mapper)
        {
            _bloggingContext = blogContext;
            _snapshotText = snapshotText;
            _mapper = mapper;
        }
        
        public IActionResult Post(string permalink)
        {
            var dbPost = _bloggingContext
                .Posts
                .Single(c => c.Permalink == permalink);

            var viewPost = _mapper.Map<Data.Models.Post, ViewModels.Post>(dbPost);

            return View("~/Views/Home/Index.cshtml", viewPost);
        }

        public IActionResult SearchRedirect(SearchRequest searchRequest)
        {
            // This prevents the problem of form resubmission when clicking back
            return RedirectToAction("SearchResults", new RouteValueDictionary(searchRequest));
        }

        public IActionResult SearchResults(SearchRequest searchRequest)
        {
            var posts = _bloggingContext
                .Posts
                .Where(c => c.Html.Contains(searchRequest.Text) || c.Title.Contains(searchRequest.Text));
            var searchResults = posts.Select(c => new SearchResult() { Post = _mapper.Map<Data.Models.Post, ViewModels.Post>(c), SnapshotText = _snapshotText.GetFirstNCharacters(c.Html, 200) + " ..." });
            return View("~/Views/Home/SearchResults.cshtml", searchResults);
        }

        public IActionResult Admin()
        {
            return View("~/Views/Home/Admin.cshtml", new NewPost());
        }

        public ActionResult CheckExistingPermalink(string permalink)
        {
            bool exists = _bloggingContext
                .Posts
                .Any(c => c.Permalink == permalink);

            return Json(!exists);
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
            return View("~/Views/Home/Admin.cshtml", newPost);
        }
    }
}
