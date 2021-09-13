using Saniteau.Facturation.Payment.Services.Dto.Authentication;
using Saniteau.Facturation.Payment.Services.Dto.PayPal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace Saniteau.Facturation.Payment.Services
{
    public class AccessTokenManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccessTokenManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task<AuthToken> GetAccessToken()
        {
            var token = GetSavedToken();
            if(token is null || token.Expiration.Subtract(DateTime.Now).TotalSeconds <= 0)
            {
                token = await CreateToken();
                SaveToken(token);
            }
            return token;
        }

        private AuthToken GetSavedToken()
        {
            if(!File.Exists(Constants.SavedTokenPath)) { return null; }

            AuthToken authToken = JsonConvert.DeserializeObject<AuthToken>(File.ReadAllText(Constants.SavedTokenPath));
            return authToken;
        }

        private void SaveToken(AuthToken token)
        {
            string json = JsonConvert.SerializeObject(token);
            File.WriteAllText(Constants.SavedTokenPath, json);
        }

        private async Task<AuthToken> CreateToken()
        {
            var now = DateTime.Now;
            var request = new HttpRequestMessage(HttpMethod.Post, Constants.PaypalOAuthUrl);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en_US");
            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{Constants.ClientId}:{Constants.Secret}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };
            request.Content = new FormUrlEncodedContent(form);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                token paypalToken = JsonConvert.DeserializeObject<token>(responseBody);
                var token = new AuthToken();
                token.AccessToken = paypalToken.access_token;
                token.AppId = paypalToken.app_id;
                token.Scope = paypalToken.scope;
                token.TokenType = paypalToken.token_type;
                int nbSecondsBeforeExpiration = Convert.ToInt32(paypalToken.expires_in);
                token.Expiration = now.AddSeconds(nbSecondsBeforeExpiration);
                return token;
            }
            else
            {
                throw new Exception($"Could not get token : {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
