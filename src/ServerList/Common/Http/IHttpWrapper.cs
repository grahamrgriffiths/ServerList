namespace Common.Http
{
    public interface IHttpWrapper
    {
        Task<string> HttpGetAsync(string requestUri, string localCacheFile, TimeSpan? timeout = null);
    }
}
