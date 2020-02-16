namespace Blog.Web.Services
{
    public interface IDatabaseCreator
    {
        void CreateDatabase(string connectionString);
    }
}