using Moq;
using SmileBoy.Client.Core.Helpers;
using SmileBoy.Client.Core.Providers;
using SmileBoyClient.Core.IContract;
using SmileBoyClient.Core.IContract.IService;
using SmileBoyClient.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmileBoy.Client.Tests
{
    public class AuthorizationProviderTest
    {
        #region FakeToken
        private const string TestToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        #endregion

        [Fact]
        public async Task Login_Authorize_Success()
        {
            string email = "test";
            var password = new System.Security.SecureString();

            var mockAuthService = new Mock<IAuthorizationService<JwtResponse>>();
            mockAuthService.Setup(auth => auth.AuthorizeAsync(email, password))
                .Returns(Task.FromResult(new AuthorizationResult<JwtResponse>(new JwtResponse() { AccessToken = TestToken})));

            var mockSessionService = new Mock<ISessionService<UserSession>>();
            mockSessionService.Setup(session => session.SaveAsync("test", new UserSession()))
                .Returns(Task.CompletedTask);

            var mockStorage = new Mock<ITokenStorage>();
            mockStorage.Setup(storage => storage[Token.Access]).Verifiable();

            var provider = new AuthorizationProvider(mockAuthService.Object, mockSessionService.Object, mockStorage.Object);

            //Act
            var state = await provider.Login(email, password);

            Assert.True(state.IsAuthentication);
            Assert.True(string.IsNullOrEmpty(state.ErrorMessage));
            Assert.Equal("John Doe", state.GetClaim("name"));
        }

        [Fact]
        public async Task Login_Authorize_Filed()
        {
            string email = "test";
            var password = new System.Security.SecureString();

            var mockAuthService = new Mock<IAuthorizationService<JwtResponse>>();
            mockAuthService.Setup(auth => auth.AuthorizeAsync(email, password))
                .Returns(Task.FromResult(new AuthorizationResult<JwtResponse>() { ErrorMessage = "Error" }));

            var mockSessionService = new Mock<ISessionService<UserSession>>();
            mockSessionService.Setup(session => session.SaveAsync("test", new UserSession()))
                .Returns(Task.CompletedTask);

            var mockStorage = new Mock<ITokenStorage>();
            mockStorage.Setup(storage => storage[Token.Access]).Verifiable();

            var provider = new AuthorizationProvider(mockAuthService.Object, mockSessionService.Object, mockStorage.Object);

            //Act
            var state = await provider.Login(email, password);

            Assert.False(state.IsAuthentication);
            Assert.False(string.IsNullOrEmpty(state.ErrorMessage));
            Assert.Equal("Error", state.ErrorMessage);
            Assert.Null(state.GetClaim("name"));
        }

        [Fact]
        public async Task ExtendSession_IsSuccess()
        {
            string refreshToken = "test";
            

            var mockAuthService = new Mock<IAuthorizationService<JwtResponse>>();
            mockAuthService.Setup(auth => auth.RefreshAsync(refreshToken))
                .Returns(Task.FromResult(new AuthorizationResult<JwtResponse>(new JwtResponse() { AccessToken = TestToken })));

            var mockSessionService = new Mock<ISessionService<UserSession>>();
            mockSessionService.Setup(session => session.SaveAsync("test", new UserSession()))
                .Returns(Task.CompletedTask);

            var mockStorage = new Mock<ITokenStorage>();
            mockStorage.Setup(storage => storage[Token.Refresh]).Returns(refreshToken);

            var provider = new AuthorizationProvider(mockAuthService.Object, mockSessionService.Object, mockStorage.Object);

            //Act
            var state = await provider.ExtendSession();

            Assert.True(state.IsAuthentication);
            Assert.True(string.IsNullOrEmpty(state.ErrorMessage));
        }
        [Fact]
        public async Task ExtendSession_Failed()
        {
            string refreshToken = "test";


            var mockAuthService = new Mock<IAuthorizationService<JwtResponse>>();
            mockAuthService.Setup(auth => auth.RefreshAsync(refreshToken))
                .Returns(Task.FromResult(new AuthorizationResult<JwtResponse>() { ErrorMessage = "Error" }));

            var mockSessionService = new Mock<ISessionService<UserSession>>();
            mockSessionService.Setup(session => session.SaveAsync("test", new UserSession()))
                .Returns(Task.CompletedTask);

            var mockStorage = new Mock<ITokenStorage>();
            mockStorage.Setup(storage => storage[Token.Refresh]).Returns(refreshToken);

            var provider = new AuthorizationProvider(mockAuthService.Object, mockSessionService.Object, mockStorage.Object);

            //Act
            var state = await provider.ExtendSession();

            Assert.False(state.IsAuthentication);
            Assert.False(string.IsNullOrEmpty(state.ErrorMessage));
            Assert.Equal("Error", state.ErrorMessage);
        }

        [Fact]
        public async Task Logout_IsSuccess()
        {
            var mockAuthService = new Mock<IAuthorizationService<JwtResponse>>();

            var mockSessionService = new Mock<ISessionService<UserSession>>();

            var mockStorage = new Mock<ITokenStorage>();
            mockStorage.Setup(storage => storage.RemoveAll()).Verifiable();

            var provider = new AuthorizationProvider(mockAuthService.Object, mockSessionService.Object, mockStorage.Object);

            //Act
            await provider.Logout();
            var state = provider.AuthenticationState;

            Assert.False(state.IsAuthentication);
            Assert.True(string.IsNullOrEmpty(state.ErrorMessage));
        }

    }
}
