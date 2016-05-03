using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdmin.Entities.ConsultAdmin.Model;
using ConsultAdminMobileProject.Fake;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using Newtonsoft.Json;
using Xamarin;

namespace ConsultAdminMobileProject.Service
{
    public class ClientProjectManager
    {
        private readonly ILogger _logger = new PCLLogger();


        public async Task<List<Contract>> GetContract(int employeeId, int contractId)
        {
            List<Contract> contracts = new List<Contract>();
            //Todo: Read the correct URL from config
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/Contract/id";

            var handle = Insights.TrackTime("Time_GetContract");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API with id as request args
                Task<string> contentsTask = client.GetStringAsync(uri);

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into Contact
                contracts = JsonConvert.DeserializeObject<List<Contract>>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "ClientProjectManager.GetContract"},
                    {"Key", contractId.ToString()}
            };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetContact-timer
                handle.Stop();
            }
            return contracts;
        }

        public async Task SaveContract(Contract contract)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/Contract";
            var handle = Insights.TrackTime("Time-SaveContract");
            handle.Start();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var requestJSON = JsonConvert.SerializeObject(contract);
                await httpClient.PostAsync(uri,
                new StringContent(requestJSON.ToString(), Encoding.UTF8, "application/json"));

                _logger.LoggEvent("SaveContract", new Dictionary<string, string>()
                {
                    {"EmployeeId", contract.EmployeeId.ToString()},
                    //{"EmployeeId", CurrentUser.EmployeeId.ToString()},
                    {"ClientId", contract.ClientId.ToString()},
                    {"ClientName", contract.ClientName },
                    {"ContractName", contract.ContractName },
                    {"StartDate", contract.StartDate.ToString("yyyy-MM-dd")},
                    {"EndDate", contract.EndDate.ToString("yyyy-MM-dd")}
                });

                handle.Stop();
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "SaveContract" } }, Insights.Severity.Error);
            }
            return;
        }

        public async Task EditContract(Contract contract)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/Contract/id";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var requestJSON = JsonConvert.SerializeObject(contract);
                var response = await httpClient.PutAsync(uri,
                new StringContent(requestJSON.ToString(), Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "EditContract" } }, Insights.Severity.Error);
            }
            return;
        }

        public async Task DeleteContract(Contract contract)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/Contract/id";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var requestJSON = JsonConvert.SerializeObject(contract);
                await httpClient.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "DeleteContract" } }, Insights.Severity.Error);
            }
            return;
        }
    }
}
