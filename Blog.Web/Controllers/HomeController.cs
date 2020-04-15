using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Blog.Data.Models;
using AutoMapper;
using Blog.Web.ViewModels;
using System.Net;
using System;
using Blog.Web.Services;
using System.Xml;
using System.IO;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDbContext _blogContext;
        private readonly IMapper _mapper;
        private readonly ILinkUtilities _linkUtilities;

        public HomeController(BlogDbContext blogContext, IMapper mapper, ILinkUtilities linkUtilities)
        {
            _blogContext = blogContext;
            _mapper = mapper;
            _linkUtilities = linkUtilities;
        }

        public IActionResult Index()
        {
            var dbPost = _blogContext
                .Posts
                .Where(c=>!c.Hidden)
                .OrderByDescending(c => c.CreationDate)
                .FirstOrDefault();

            if(dbPost == null)
            {
                return View("NoneFound");
            }

            var viewPost = _mapper.Map<Data.Models.Post, Web.ViewModels.Post>(dbPost);

            viewPost.Permalink = _linkUtilities.GetPermalink(dbPost.UrlSegment);
            viewPost.Path = _linkUtilities.GetPath(dbPost.UrlSegment);

            return View(viewPost);
        }


        [Route("/Post/{urlSegment}")]
        public IActionResult Post(string urlSegment)
        {
            var dbPost = _blogContext
                .Posts
                .SingleOrDefault(c => !c.Hidden && c.UrlSegment == urlSegment);

            if(dbPost == null)
            {
                return NotFound();
            }

            var viewPost = _mapper.Map<Data.Models.Post, Web.ViewModels.Post>(dbPost);

            viewPost.Permalink = _linkUtilities.GetPermalink(dbPost.UrlSegment);
            viewPost.Path = _linkUtilities.GetPath(dbPost.UrlSegment);

            return View("~/Views/Home/Index.cshtml", viewPost);
        }

        [Route("/sitemap.xml")]
        public IActionResult SitemapXml()
        {
            var posts = _blogContext.Posts
                .Where(c => !c.Hidden)
               .OrderBy(c => c.CreationDate)
               .ToList();

            using (var xml = XmlWriter.Create(Response.Body, new XmlWriterSettings { Indent = true }))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                xml.WriteStartElement("url");
                xml.WriteElementString("loc", "https://solores-software.net");
                xml.WriteEndElement();
                foreach (var post in posts)
                {
                    var uriBuilder = new UriBuilder() {
                        Scheme = "https",
                        Host = "solores-software.net",
                        Path = $"/Post/{post.UrlSegment}"
                    };

                    xml.WriteStartElement("url");
                    xml.WriteElementString("loc", uriBuilder.Uri.AbsoluteUri);
                    xml.WriteElementString("lastmod", post.LastModifiedDate.ToString("yyyy-MM-dd"));
                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
                return Content(xml.ToString(), "application/xml");
            }
        }
    }
}
