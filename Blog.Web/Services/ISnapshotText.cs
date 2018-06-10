namespace Blog.Services
{
    public interface ISnapshotText
    {
        string GetFirstNCharacters(string html, int numCharacters);
    }
}