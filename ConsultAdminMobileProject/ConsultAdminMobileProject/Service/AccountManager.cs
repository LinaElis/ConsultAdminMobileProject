using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdminMobileProject.Interface;
using Newtonsoft.Json;
using Xamarin;

namespace ConsultAdminMobileProject.Service
{
    public class AccountManager
    {
        private readonly ILogger _logger = new PCLLogger();
        public async Task<LoginReponse> DoLogin(string username, string password)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/Account/Login";
            LoginReponse logedinUser = new LoginReponse();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            LoginRequest login = new LoginRequest
            {
                UserName = username,
                Password = password
            };

            try
            {
                var requestJSON = JsonConvert.SerializeObject(login);
                HttpResponseMessage response = await httpClient.PostAsync(uri,
                    new StringContent(requestJSON.ToString(), Encoding.UTF8, "application/json")).ConfigureAwait(false);

                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON data into LoginReponse
                LoginReponse loginReponse = JsonConvert.DeserializeObject<LoginReponse>(responseBody);

                logedinUser = loginReponse;
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "DoLogin" } }, Insights.Severity.Error);
            }
            return logedinUser;
        }

        public async void ChangePassword(int userid, string oldpassword, string newpassword)
        {
            throw new NotImplementedException("Method for changing password is not yet implemented!");

        }

    }
}
