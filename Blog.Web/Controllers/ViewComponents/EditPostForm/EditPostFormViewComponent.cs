using AutoMapper;
using Blog.Data.Models;
using Blog.Web.Services;
using Blog.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Controllers.ViewComponents.EditPostForm
{
    public class EditPostFormViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly BlogDbContext _blogContext;
        private readonly ILinkUtilities _linkUtilities;

        public EditPostFormViewComponent(IMapper mapper, BlogDbContext blogContext, ILinkUtilities linkUtilities)
        {
            _mapper = mapper;
            _blogContext = blogContext;
            _linkUtilities = linkUtilities;
        }

        public IViewComponentResult Invoke(int postId)
        {
            if(postId == 0)
            {
                return View("~/Views/Shared/Components/EditPostForm.cshtml", new EditablePost());
            } 
            
            var dbBlogPost = _blogContext.Posts.Single(c => c.Id == postId);
            var viewModel = _mapper.Map<EditablePost>(dbBlogPost);
            viewModel.Path = _linkUtilities.GetPath(viewModel.UrlSegment);

            return View("~/Views/Shared/Components/EditPostForm.cshtml", viewModel);
        }
    }
}
