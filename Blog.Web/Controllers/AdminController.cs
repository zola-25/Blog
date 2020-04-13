using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Blog.Web.ViewModels;

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
            return View(new Admin());
        }


    }
}