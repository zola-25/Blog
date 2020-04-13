using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.Data.Models;
using AutoMapper;
using Blog.Web.Services;
using Microsoft.AspNetCore.Routing;
using Blog.Web.ViewModels;

namespace Blog.Controllers
{
    public class SearchController : Controller
    {
        private BlogDbContext _bloggingContext;
        private ISnapshotText _snapshotText;
        private IMapper _mapper;

        public SearchController(BlogDbContext blogContext, ISnapshotText snapshotText, IMapper mapper)
        {
            _bloggingContext = blogContext;
            _snapshotText = snapshotText;
            _mapper = mapper;
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
            var searchResults = posts.Select(c => 
            new SearchResult() { 
                Post = _mapper.Map<Data.Models.Post, Web.ViewModels.Post>(c), 
                SnapshotText = _snapshotText.GetFirstNCharacters(c.Html, 200) + " ..." }
            );
            return View(searchResults);
        }

        public ActionResult CheckExistingPermalink(string permalink)
        {
            bool exists = _bloggingContext
                .Posts
                .Any(c => c.UrlSegment == permalink);

            return Json(!exists);
        }
    }
}
