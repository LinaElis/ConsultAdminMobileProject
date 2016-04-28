using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using Newtonsoft.Json;
using Xamarin;

namespace ConsultAdminMobileProject.Service
{
    public class NewsManager
    {
        //Todo: Read the correct URL from config
        private readonly string _url =
            "http://consultadminwebserver.azurewebsites.net/api/news?LocationId=" + CurrentUser.LocationId.ToString();
        private readonly ILogger _logger = new PCLLogger();

        public async Task<List<News>> GetAllNews()
        {
            List<News> news = new List<News>();

            var handle = Insights.TrackTime("Time_GetAllNews");
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

                // Deserialize the JSON data into ContactManager (List of contacts)
                news = JsonConvert.DeserializeObject<List<News>>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "ServiceConsumer.GetAllNews"}
                };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetAllContacts-timer
                handle.Stop();
            }

            return news;
        }

        public async Task<News> GetContact(int id)
        {
            News news = new News();

            var handle = Insights.TrackTime("Time_GetOneNews");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API with id as request args
                Task<string> contentsTask = client.GetStringAsync(_url + id.ToString());

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into Contact
                news = JsonConvert.DeserializeObject<News>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "ServiceConsumer.GetContact"},
                    {"Key", id.ToString()}
            };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetContact-timer
                handle.Stop();
            }
            return news;
        }

    }
}
