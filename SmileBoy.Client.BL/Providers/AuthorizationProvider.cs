using SmileBoy.Client.Core.Helpers;
using SmileBoyClient.Core;
using SmileBoyClient.Core.Helpers;
using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.IContract.IProviders;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Core.Models;
using System;
using System.IO;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Providers
{
    public class AuthorizationProvider : IAuthoriazationProvider
    {
        private readonly IAuthorizationService<JwtResponse> _authorizationService;
        private readonly ISessionService<UserSession> _sessionService;
        private readonly ITokenStorage _tokenStorage;

        private readonly string _sessionPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "session.json");

        public AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        private AuthenticationState _authenticationState;
        public AuthenticationState AuthenticationState => _authenticationState ??= GetAuthentificationState();

        public AuthorizationProvider(IAuthorizationService<JwtResponse> authorizationService, ISessionService<UserSession> sessionService,
            ITokenStorage tokenStorage)
        {
            _authorizationService = authorizationService;
            _sessionService = sessionService;
            _tokenStorage = tokenStorage;
        }

        private  AuthenticationState GetAuthentificationState()
        {
            if (!_sessionService.TryRecover(_sessionPath, out UserSession session))
                return Anonymous;

            return AuthenticationStateChangedAsync(session.AccessToken, session.RefreshToken, false).Result;
        }

        /// <summary>
        /// Updates the authorization status and saves it to a session file on the local disk
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="saveSession"></param>
        /// <returns></returns>
        private async Task<AuthenticationState> AuthenticationStateChangedAsync(string accessToken, string refreshToken,
            bool saveSession = true)
        {
            _tokenStorage[Token.Access] = accessToken;
            _tokenStorage[Token.Refresh] = refreshToken;

            if (saveSession)
                await _sessionService.SaveAsync(_sessionPath, new UserSession
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });

            return BuildAuthentificationState(accessToken);
        }

        private AuthenticationState BuildAuthentificationState(string token)
        {
            if (string.IsNullOrEmpty(token))
                return _authenticationState = Anonymous;

            return _authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(token.ParseClaimFromJwt(), "jwt")));
        }

        public async Task<AuthenticationState> ExtendSession()
        {
            AuthorizationResult<JwtResponse> responce = await _authorizationService.RefreshAsync(_tokenStorage[Token.Refresh]);

            return await SignInAsync(responce);
        }

        public async Task<AuthenticationState> Login(string email, SecureString password)
        {
            AuthorizationResult<JwtResponse> result = await _authorizationService.AuthorizeAsync(email, password);

            return await SignInAsync(result);
        }

        private async Task<AuthenticationState> SignInAsync(AuthorizationResult<JwtResponse> authorization)
        {
            if(!authorization.IsSuccess)
            {
                AuthenticationState anonymous = Anonymous;
                anonymous.ErrorMessage = authorization.ErrorMessage;

                return anonymous;
            }

            var response = authorization.Response;
            return await AuthenticationStateChangedAsync(response.AccessToken, response.RefreshToken);
        }

        public async Task Logout()
        {
            await AuthenticationStateChangedAsync(accessToken: string.Empty, refreshToken: string.Empty);
            _tokenStorage.RemoveAll();
        }
    }
}
