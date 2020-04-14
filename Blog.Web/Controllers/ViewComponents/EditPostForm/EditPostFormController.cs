using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Data.Models;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.ViewComponents.EditPostForm
{
    [Authorize(Roles = "Administrator")]
    public class EditPostFormController : Controller
    {
        private readonly BlogDbContext _blogContext;
        private readonly IMapper _mapper;

        public EditPostFormController(BlogDbContext blogContext, IMapper mapper)
        {
            _blogContext = blogContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("admin/post/{postId}")]
        public IActionResult Get(int postId)
        {
            if (postId == 0 || _blogContext.Posts.Any(c => c.Id == postId))
            {
                return ViewComponent("EditPostForm", new { postId = postId });
            }
            return NotFound(new { postId });
        }

        [HttpPost]
        [Route("admin/post/{postId}")]
        public async Task<IActionResult> AddOrUpdate(int postId, [FromForm]EditablePost newPost)
        {
            bool isNew = postId == 0;
            if (isNew && _blogContext.Posts.Any(c => c.UrlSegment == newPost.UrlSegment))
            {
                ModelState.AddModelError("Blog.ViewModels.NewPost.Permalink", string.Format("Url Segment {0} already exists", newPost.UrlSegment));
            }

            if (ModelState.IsValid)
            {
                var dbPost = _mapper.Map<Data.Models.Post>(newPost);
                dbPost.LastModifiedDate = DateTime.Today;
                _blogContext.Update(dbPost); // Performs add or update
                await _blogContext.SaveChangesAsync();
                return Ok(isNew ? "Post Added" : "Post Updated");
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("admin/post/{postId}")]
        public async Task<IActionResult> Delete(int postId)
        {
            var toDelete = _blogContext.Posts.Single(c => c.Id == postId);
            _blogContext.Remove(toDelete);
            await _blogContext.SaveChangesAsync();

            return Ok();
        }

        public ActionResult CheckExistingUrlSegment(string urlSegment, int postId)
        {
            bool isNew = postId == 0;
            bool exists = _blogContext
                .Posts
                .Where(c=> isNew || c.Id != postId)
                .Any(c => c.UrlSegment == urlSegment);

            return Json(!exists);
        }
    }
}