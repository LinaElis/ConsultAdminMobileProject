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
    public class ContractManager
    {
        private readonly string _url =
            "http://consultadminwebserver.azurewebsites.net/api/Contract";
        private readonly ILogger _logger = new PCLLogger();

        public async Task<List<Contract>> GetAllContracts()
        {
            List<Contract> contract = new List<Contract>();

            var handle = Insights.TrackTime("Time_GetAllContracts");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API for contacts
                Task<string> contentsTask = client.GetStringAsync(_url);

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into ContractManager (List of contracts)
                contract = JsonConvert.DeserializeObject<List<Contract>>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "ServiceConsumer.GetAllContracts"}
                };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetAllContracts-timer
                handle.Stop();
            }

            return contract;
        }
    }
}
