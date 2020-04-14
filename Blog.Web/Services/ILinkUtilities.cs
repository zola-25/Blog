namespace Blog.Web.Services
{
    public interface ILinkUtilities
    {
        string GetPath(string urlSegment);
        string GetPermalink(string urlSegment);
        string GetPathEncoded(string urlSegment);
    }
}