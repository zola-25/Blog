namespace Blog.Web.Services
{
    public interface ISnapshotText
    {
        string GetFirstNCharacters(string html, int numCharacters);
    }
}