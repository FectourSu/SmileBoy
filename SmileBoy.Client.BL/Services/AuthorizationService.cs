using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Core.Models;
using System;
using IdentityModel.Client;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Text.Json;

namespace SmileBoy.Client.Core.Services
{
    public class AuthorizationService<TResponse> : IAuthorizationService<TResponse>
        where TResponse : class, new()
    {
        private readonly HttpClient _httpClient;

        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Implementation of authorization 
        /// based on the Bearer scheme and the Oauth 2.0 approach
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        public async Task<AuthorizationResult<TResponse>> AuthorizeAsync(string email, SecureString password)
        {
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(ConfigurationManager.AppSettings["Address"]);
            var credential = new NetworkCredential(email, password);

            var response = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = ConfigurationManager.AppSettings["ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                Scope = ConfigurationManager.AppSettings["Scope"],

                UserName = credential.UserName,
                Password = credential.Password
            });

            return await CreateResult(response);
        }
        
        private async Task<AuthorizationResult<TResponse>> CreateResult(TokenResponse response)
        {
            var result = new AuthorizationResult<TResponse>();

            if (response.IsError)
            {
                result.ErrorMessage = string.IsNullOrEmpty(response.ErrorDescription) ?
                    "No response from the server" :
                    response.ErrorDescription;

                return result;
            }

            result.Response = await Deserialize(response.HttpResponse.Content);

            return result;
        }

        private async Task<TResponse> Deserialize(HttpContent content)
        {
            var responseString = await content.ReadAsStringAsync();

            var deserializeResult = JsonSerializer.Deserialize<TResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return deserializeResult;
        }
        /// <summary>
        /// Executes a request on the remote server to update the access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<AuthorizationResult<TResponse>> RefreshAsync(string refreshToken)
        {
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(ConfigurationManager.AppSettings["Address"]);
            
            var response = await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = ConfigurationManager.AppSettings["ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                Scope = ConfigurationManager.AppSettings["Scope"],

                RefreshToken = refreshToken
            });

            return await CreateResult(response);
        }
    }
}
