using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using ConsultAdminMobileProject.Service;
using Xamarin;

namespace ConsultAdminMobileProject.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _loginRequired;
        private string _loginMessage;
        private readonly ILogger _logger = new PCLLogger();

        public string LoginMessage
        {
            get { return _loginMessage; }
            set
            {
                if (_loginMessage != value)
                {
                    SetPropertyField(nameof(LoginMessage), ref _loginMessage, value);
                }
            }
        }

        public bool LoginRequired
        {
            get { return _loginRequired; }
            set
            {
                if (_loginRequired != value)
                    EnableButton = true;
                SetPropertyField(nameof(LoginRequired), ref _loginRequired, value);
            }
        }

        public LoginViewModel()
        {
            LocalStorageManager localStorage = new LocalStorageManager();
            localStorage.GetUseridAndPassword();
            if (localStorage.UserId != "")
            {
                Login(localStorage.UserId.Trim(), localStorage.Password.Trim(), true);
            }
            LoginRequired = true;
        }

        public async void Login(string username, string password, bool rememberMe)
        {
            AccountManager accountManager = new AccountManager();
            LoginReponse logedinUser =
                await accountManager.DoLogin(username, password);

            if (logedinUser.StatusCode == 0)
            {
                try
                {
                    CurrentUser.EmployeeId = logedinUser.EmployeeId;
                    CurrentUser.UserId = logedinUser.UserId;
                    CurrentUser.LocationId = logedinUser.LocationId;
                    CurrentUser.FullName = logedinUser.FullName;
                    CurrentUser.DefaultClintId = logedinUser.DefaultClintId;
                    CurrentUser.DefaultContractId = 1;
                    //CurrentUser.DefaultContractId = logedinUser.DefaultContractId;
                    CurrentUser.StartTime = logedinUser.StartTime;
                    CurrentUser.EndTime = logedinUser.EndTime;
                    CurrentUser.LunchBreak = logedinUser.LunchBreak;
                    CurrentUser.EmployeeContracts = logedinUser.EmployeeContracts;
                }
                catch (Exception ex)
                {
                    _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "Login:SetCurrentUser" } });
                }

                try
                {
                    if (rememberMe)
                    {
                        LocalStorageManager localStorage = new LocalStorageManager();
                        localStorage.SaveUseridAndPassword(username, password);
                        _logger.LoggEvent("Save user for auto-login", new Dictionary<string, string>() { { "Username", username } });
                    }
                    else
                    {
                        LocalStorageManager localStorage = new LocalStorageManager();
                        localStorage.ClearUseridAndPassword();
                        _logger.LoggEvent("Clear username for auto-login", new Dictionary<string, string>() { { "Username", username } });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "Login:SetRememberMe" } });
                }
                Insights.Identify(CurrentUser.UserId.ToLower());


                Dictionary<string, string> myDict = new Dictionary<string, string>
                {
                    {"Username", username},
                    {"EmployeeId", CurrentUser.EmployeeId.ToString()}
                };
                _logger.LoggEvent("UserLogin", myDict);

                LoginMessage = "Login successfull, redirecting..";
                //Ändra till true sen när validering på ,user och pass finns
                LoginRequired = false;
            }
            else
            {
                _logger.LoggText("Invalid user login! " + username);
                LoginRequired = true;
                LoginMessage = "Username or Password is wrong";
            }
        }
    }
}
