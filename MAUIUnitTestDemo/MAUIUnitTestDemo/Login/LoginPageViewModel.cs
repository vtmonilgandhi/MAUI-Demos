    using System;
using System.Windows.Input;

namespace MAUIUnitTestDemo.Login
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IAlertService alertService;

        private string userName;
        private string password;
        private bool isLoading;

        private ICommand loginCommand;

        public LoginPageViewModel(IAuthenticationService authenticationService, IAlertService alertService)
        {
            this.authenticationService = authenticationService;
            this.alertService = alertService;
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand => loginCommand ?? (loginCommand = new Command(Login));

        private async void Login()
        {
            if (String.IsNullOrWhiteSpace(UserName) || String.IsNullOrWhiteSpace(Password))
            {
                await alertService.ShowAlert("Warning", "Please, enter correct username and password");
            }
            else
            {
                IsLoading = true;
                bool isAuthenticated = await authenticationService.Login(UserName, Password);
            }
        }
    }
}

