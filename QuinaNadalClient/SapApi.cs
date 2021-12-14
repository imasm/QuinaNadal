using Newtonsoft.Json;
using QuinaNadalServer;
using QuinaNadalServer.Models;
using RestSharp;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace QuinaNadalClient
{
    public class ApiClient
    {
        private readonly Uri _serverUrl;
        private readonly RestClient _restClient;

        public ApiClient(Uri serverUrl)
        {            
            _serverUrl = serverUrl;
            _restClient = new RestClient();
            _restClient.BaseUrl = _serverUrl;
        }       

        public Taulell GetTaulell()
        {
            string url = $"api/quina/{Keys.KeyGet}";
            var request = new RestRequest(url, Method.GET);


            var response = _restClient.Execute<Taulell>(request);
            if (response.ErrorException != null)
            {
                throw new ApiCallException(response.ErrorException?.Message,
                    response.ErrorException.InnerException);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var exception = new ApiCallException(response.StatusCode, response.StatusDescription);
                throw exception;
            }

            return response.Data;
        }

        public void SetTaulell(Taulell taulell)
        {
            string url = $"api/quina/{Keys.KeySet}";
            var request = new RestRequest(url, Method.PUT);
            request.AddJsonBody(taulell);

            var response = _restClient.Execute(request);
            if (response.ErrorException != null)
            {
                throw new ApiCallException(response.ErrorException?.Message,
                    response.ErrorException.InnerException);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var exception = new ApiCallException(response.StatusCode, response.StatusDescription);
                throw exception;
            }
        }
        
        private static void LogRequest(IRestClient restClient, IRestRequest request, IRestResponse response, long durationMs)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                // This will generate the actual Uri used in the request
                uri = restClient.BuildUri(request),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers,
                // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            var message = string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
                durationMs, JsonConvert.SerializeObject(requestToLog),
                JsonConvert.SerializeObject(responseToLog));

            Debug.WriteLine(message);
        }



    }
}
