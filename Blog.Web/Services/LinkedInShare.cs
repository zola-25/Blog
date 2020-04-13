using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Web.Services
{
    public class LinkedInShare
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly string _httpClientName;
        //private readonly HttpClient _httpClient;

        ////https://stackoverflow.com/a/54597365/3910619

        //public LinkedInShare(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _httpClientName = "LinkedInShare";
        //    _httpClient = _httpClientFactory.CreateClient(_httpClientName);
        //}

        //public async Task<HttpResponse> Post<T>(string path, T parameters)
        //{
        //    var jsonString = JsonConvert.SerializeObject(parameters);
        //    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync(path, content);

        //    return new HttpResponse {
        //        StatusCode = (int)response.StatusCode,
        //        ReasonPhrase = response.ReasonPhrase,
        //        ContentString = await response.Content.ReadAsStringAsync()
        //    };
        //}

        //public class ShareMedia { 
        //    public string Status {get;set;} = "READY";
        //    public string Description {get;set;} 
        //    public string OriginalUrl {get;set;} 
        //    public string Title {get;set;}

        //}

        //public class RequestBody
        //{
        //    public string Author {get;set;}
        //}
        
    }
}
