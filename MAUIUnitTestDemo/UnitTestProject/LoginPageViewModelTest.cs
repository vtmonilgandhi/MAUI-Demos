using System;
using MAUIUnitTestDemo.Login;
using Moq;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace UnitTestProject
{
    public class LoginPageViewModelTest
    {
        private readonly Mock<IAuthenticationService> authenticationServiceMock = new Mock<IAuthenticationService>();
        private readonly Mock<IAlertService> alertServiceMock = new Mock<IAlertService>();

        public static IEnumerable<object[]> GetLoginCreds()
        {
            yield return new object[] { "user", "" };
            yield return new object[] { "user", " " };
            yield return new object[] { "user", null };
        }

        public static IEnumerable<object[]> GetLoginCredsWithAlert()
        {
            yield return new object[] { "user", "", "Please, enter correct username and password" };
            yield return new object[] { "user", " ", "Please, enter correct username and password" };
            yield return new object[] { "user", null, "Please, enter correct username and password" };
        }

        [Theory]
        [InlineData("user", "pass")]
        public void LogInWithValidCreds_LoadingIndicatorShown(string userName, string password)
        {
            LoginPageViewModel model = CreateViewModelAndLogin(userName, password);

            Assert.True(model.IsLoading);
        }

        [Theory]
        [InlineData("user", "pass")]
        public void LogInWithValidCreds_AuthenticationRequested(string userName, string password)
        {
            CreateViewModelAndLogin(userName, password);

            authenticationServiceMock.Verify(x => x.Login(userName, password), Times.Once);
        }

        [Theory]
        [InlineData("", "pass")]
        [InlineData("   ", "pass")]
        [InlineData(null, "pass")]
        public void LogInWithEmptyuserName_AuthenticationNotRequested(string userName, string password)
        {
            CreateViewModelAndLogin(userName, password);

            authenticationServiceMock.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("", "pass", "Please, enter correct username and password")]
        [InlineData("   ", "pass", "Please, enter correct username and password")]
        [InlineData(null, "pass", "Please, enter correct username and password")]
        public void LogInWithEmptyUserName_AlertMessageShown(string userName, string password, string message)
        {
            CreateViewModelAndLogin(userName, password);

            alertServiceMock.Verify(x => x.ShowAlert(It.IsAny<string>(), message));
        }

        [Theory]
        [MemberData(nameof(GetLoginCreds))]
        public void LogInWithEmptypassword_AuthenticationNotRequested(string userName, string password)
        {
            CreateViewModelAndLogin(userName, password);

            authenticationServiceMock.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [MemberData(nameof(GetLoginCredsWithAlert))]
        public void LogInWithEmptypassword_AlertMessageShown(string userName, string password, string message)
        {
            CreateViewModelAndLogin(userName, password);

            alertServiceMock.Verify(x => x.ShowAlert(It.IsAny<string>(), message));
        }

        private LoginPageViewModel CreateViewModelAndLogin(string userName, string password)
        {
            var model = new LoginPageViewModel(authenticationServiceMock.Object, alertServiceMock.Object);

            model.UserName = userName;
            model.Password = password;

            model.LoginCommand.Execute(null);

            return model;
        }
    }
}

