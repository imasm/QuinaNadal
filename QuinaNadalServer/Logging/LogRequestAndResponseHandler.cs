using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace QuinaNadalServer.Logging
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        private static readonly NLogServerLogger logger = new NLogServerLogger(); 

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            logger.Debug($"{request.Method} {request.RequestUri}");

            // log request body
            string requestBody = await request.Content.ReadAsStringAsync();
            logger.Debug(requestBody);

            // let other handlers process the request
            HttpResponseMessage result = await base.SendAsync(request, cancellationToken);

            logger.Debug($"{(int)result.StatusCode} {result.StatusCode}");
            if (result.Content != null)
            {
                string responseBody = await result.Content.ReadAsStringAsync();
                logger.Debug(responseBody);
            }

            return result;
        }
    }
}
