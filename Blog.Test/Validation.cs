using Blog.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Blog.Test
{
    public class Validation
    {
        //[Fact]
        //public void NewPostValidation()
        //{
        //    var newPost = new NewPost()
        //    {
        //        Title = "Title",
        //        Html = "Html",
        //        Permalink = "duplicate-permalink"
        //    };
            
        //    var validationContext = new ValidationContext(newPost);
        //    var result = newPost.Validate(validationContext);

        //    Assert.True(!String.IsNullOrWhiteSpace(result.First().ErrorMessage));
        //    Assert.True(result.First().MemberNames.Single() == "Permalink");

        //}
    }
}
