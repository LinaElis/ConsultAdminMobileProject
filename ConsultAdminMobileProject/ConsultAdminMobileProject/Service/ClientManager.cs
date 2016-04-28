using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdmin.Entities.ConsultAdmin.Model;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using Newtonsoft.Json;
using Xamarin;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.Service
{
    public class ClientManager
    {
        //Todo: Read the correct URL from config
        private string Url = $"http://consultadminwebserver.azurewebsites.net/api/employeecontract?EmployeeId={CurrentUser.EmployeeId}";

        private readonly ILogger _logger = new PCLLogger();

        public async Task<List<TimeReport>> GetAllClients()
        {
            List<TimeReport> clients = new List<TimeReport>();

            var handle = Insights.TrackTime("Time_GetAllClients");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API for clients
                Task<string> contentsTask = client.GetStringAsync(Url);

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into ContactManager (List of contacts)
                clients = JsonConvert.DeserializeObject<List<TimeReport>>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "ClientManager.GetAllClients"}
                };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetAllClients-timer
                handle.Stop();
            }

            return clients;
        }

        public async Task<Client> GetClient(int id)
        {
            Client client = new Client();

            var handle = Insights.TrackTime("Time_GetClient");
            handle.Start();

            try
            {
                HttpClient httpClient = new HttpClient();

                // Set pref result as JSON
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API with id as request args
                Task<string> contentsTask = httpClient.GetStringAsync(Url + id.ToString());

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into Contact
                client = JsonConvert.DeserializeObject<Client>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "ClientManager.GetClient"},
                    {"Key", id.ToString()}
            };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetContact-timer
                handle.Stop();
            }
            return client;
        }
    }

}
