using Microsoft.Extensions.Logging;
using System.Net;

namespace Common.Http
{
    /// <summary>
    /// Centralise http functionality
    /// </summary>
    public class HttpWrapper : IHttpWrapper
    {
        private readonly ILogger _logger;
        private HttpClient _httpClient;

        public HttpWrapper(ILogger<HttpWrapper> logger)
        {
            _logger = logger;
            SetupClient();
        }

        public HttpWrapper()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            // ensure single instance of http client is used
            _httpClient = new HttpClient();

            // support newest security protocols on each request
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            // Default is 2 minutes: https://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.dnsrefreshtimeout(v=vs.110).aspx
            ServicePointManager.DnsRefreshTimeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;

            // Increases the concurrent outbound connections
            ServicePointManager.DefaultConnectionLimit = 1024;
        }
        /// <summary>
        /// Wrapper method to issue http get async
        /// </summary>
        /// <param name="requestUri">The URI of the request</param>
        /// <param name="timeout">Optional timeout for the request</param>
        /// <returns>The response content as a string</returns>
        public async Task<string> HttpGetAsync(string requestUri, string localCacheFile, TimeSpan? timeout = null)
        {
            string responseContent;
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync(requestUri, GetCancellationTokenFromTimeout(timeout)).ConfigureAwait(false);
                responseMessage.EnsureSuccessStatusCode();
                responseContent = await responseMessage.Content.ReadAsStringAsync();
                File.WriteAllText(Path.GetTempPath() + $"\\{localCacheFile}", responseContent);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed to fetch data, loading from local cache");
                responseContent = File.ReadAllText(Path.GetTempPath() + $"\\{localCacheFile}");
                _logger.LogError(ex, ex.Message);
            }

            return responseContent;
        }

        /// <summary>
        /// Helper method to setup a CancellationToken (from a custom timeout) for the http request
        /// </summary>
        /// <param name="timeout">Optional, custom timeout</param>
        /// <returns>CancellationToken</returns>
        private CancellationToken GetCancellationTokenFromTimeout(TimeSpan? timeout)
        {
            // set default timeout if not supplied
            if (!timeout.HasValue)
            {
                timeout = TimeSpan.FromSeconds(Constants.DEFAULT_REQUEST_TIMEOUT_IN_SECONDS);
            }

            // https://stackoverflow.com/questions/46874693/re-using-httpclient-but-with-a-different-timeout-setting-per-request
            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout.Value);

            return cts.Token;
        }
    }

}
