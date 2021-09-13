using Newtonsoft.Json;
using Saniteau.Facturation.Payment.Services.Dto.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Saniteau.Facturation.Payment.Services
{
    public class HttpMethodCaller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpMethodCaller(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<TResult> CallPostMethod<TResult>(string url, dynamic requestContent, Dictionary<string, string> headers, AuthToken authToken)
        {
            return await CallPostMethodWithBody<TResult>(HttpMethod.Post, url, requestContent, headers, authToken);
        }
        public async Task<TResult> CallPatchMethod<TResult>(string url, dynamic requestContent, Dictionary<string, string> headers, AuthToken authToken)
        {
            return await CallPatchMethodWithBody<TResult>(HttpMethod.Patch, url, requestContent, headers, authToken);
        }

        public async Task<TResult> CallGetMethod<TResult>(string url, Dictionary<string, string> headers, AuthToken authToken)
        {
            return await CallMethodWithNoBody<TResult>(HttpMethod.Get, url, headers, authToken);
        }
        public async Task<TResult> CallDeleteMethod<TResult>(string url, Dictionary<string, string> headers, AuthToken authToken)
        {
            return await CallMethodWithNoBody<TResult>(HttpMethod.Delete, url, headers, authToken);
        }

        private async Task<TResult> CallMethodWithNoBody<TResult>(HttpMethod httpMethod, string url, Dictionary<string, string> headers, AuthToken authToken)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en_US");
            request.Headers.Add("Authorization", $"Bearer {authToken.AccessToken}");
            foreach (var headerName in headers.Keys)
            {
                request.Headers.Add(headerName, headers[headerName]);
            }

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                TResult result = JsonConvert.DeserializeObject<TResult>(responseBody);
                return result;
            }
            else
            {
                throw new Exception($"Error while calling {url} : {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<TResult> CallPostMethodWithBody<TResult>(HttpMethod httpMethod, string url, dynamic requestContent, Dictionary<string, string> headers, AuthToken authToken)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en_US");
            request.Headers.Add("Authorization", $"Bearer {authToken.AccessToken}");
            foreach (var headerName in headers.Keys)
            {
                request.Headers.Add(headerName, headers[headerName]);
            }
            string stringContent = JsonConvert.SerializeObject(requestContent, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var content = new StringContent(stringContent, Encoding.UTF8, "application/json");
            request.Content = content;

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                TResult result = JsonConvert.DeserializeObject<TResult>(responseBody);
                return result;
            }
            else
            {
                throw new Exception($"Error while calling {url} : {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<TResult> CallPatchMethodWithBody<TResult>(HttpMethod httpMethod, string url, dynamic requestContent, Dictionary<string, string> headers, AuthToken authToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer {authToken.AccessToken}");

                string stringContent = JsonConvert.SerializeObject(requestContent, Formatting.None, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                var content = new StringContent(stringContent, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = content };
                request.Headers.Add("Authorization", $"Bearer {authToken.AccessToken}");
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    TResult result = JsonConvert.DeserializeObject<TResult>(responseBody);
                    return result;
                }
            }

        }

    }
}
