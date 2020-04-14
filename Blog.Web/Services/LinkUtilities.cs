using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Blog.Web.Services
{
    public class LinkUtilities : ILinkUtilities
    {
        IHttpContextAccessor _httpContextAccessor;

        public LinkUtilities(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetPermalink(string urlSegment)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = _httpContextAccessor.HttpContext.Request.Scheme,
                Host = _httpContextAccessor.HttpContext.Request.Host.Host,
                Path = GetPath(urlSegment)
            };

            return uriBuilder.Uri.AbsoluteUri;
        }

        public string GetPathEncoded(string urlSegment)
        {
            return WebUtility.UrlEncode($"/Post/{urlSegment}");
        }

        public string GetPath(string urlSegment)
        {
            return $"/Post/{urlSegment}";
        }
    }
}
