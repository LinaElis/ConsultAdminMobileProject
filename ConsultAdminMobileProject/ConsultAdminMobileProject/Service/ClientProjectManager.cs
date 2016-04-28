using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities.ConsultAdmin.Model;
using ConsultAdminMobileProject.Interface;
using Newtonsoft.Json;
using Xamarin;

namespace ConsultAdminMobileProject.Service
{
    public class ClientProjectManager
    {
        private readonly ILogger _logger = new PCLLogger();

        public async Task<List<TimeReport>> GetClientContract(int employeeId, int timeReportId)
        {
            List<TimeReport> timeReport = new List<TimeReport>();
            //Todo: Read the correct URL from config
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/employee/{employeeId}/timereports?&TimeReportId={timeReportId}";

            var handle = Insights.TrackTime("Time_GetClientContract");
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
                timeReport = JsonConvert.DeserializeObject<List<TimeReport>>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "EmployeeManager.GetClientContract"},
                    {"Key", timeReportId.ToString()}
            };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetContact-timer
                handle.Stop();
            }
            return timeReport;
        }

        public async Task SaveClientContract(TimeReport timeReport)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/employee/{timeReport.EmployeeId}/timereports";
            var handle = Insights.TrackTime("Time-SaveClientContract");
            handle.Start();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var requestJSON = JsonConvert.SerializeObject(timeReport);
                await httpClient.PostAsync(uri,
                new StringContent(requestJSON.ToString(), Encoding.UTF8, "application/json"));

                _logger.LoggEvent("SaveClientContract", new Dictionary<string, string>()
                {
                    {"EmployeeId", timeReport.EmployeeId.ToString()},
                    {"ClientId", timeReport.ClientId.ToString()},
                    {"StartDate", timeReport.StartDate.ToString()},
                    {"EndDate", timeReport.EndDate.ToString()}

                });

                handle.Stop();
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "SaveClientContract" } }, Insights.Severity.Error);
            }
            return;
        }

        public async Task EditClientContract(TimeReport timeReport)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/employee/{timeReport.EmployeeId}/timereports?id={timeReport.Id}";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var requestJSON = JsonConvert.SerializeObject(timeReport);
                var response = await httpClient.PutAsync(uri,
                new StringContent(requestJSON.ToString(), Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "EditClientContract" } }, Insights.Severity.Error);
            }
            return;
        }

        public async Task DeleteClientContract(TimeReport timeReport)
        {
            string uri = $"http://consultadminwebserver.azurewebsites.net/api/employee/{timeReport.EmployeeId}/timereports?id={timeReport.Id}";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var requestJSON = JsonConvert.SerializeObject(timeReport);
                await httpClient.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "DeleteClientContract" } }, Insights.Severity.Error);
            }
            return;
        }
    }
}
