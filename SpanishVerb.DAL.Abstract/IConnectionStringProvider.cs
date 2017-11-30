namespace SpanishVerb.DAL.Abstract
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string model, string providerConnectionString);
        string GetConnectionStringFromPath(string model, string path);
    }
}