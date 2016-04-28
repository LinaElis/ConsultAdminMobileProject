using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EmployeeManager
    {
        private readonly ILogger _logger = new PCLLogger();

        private const string Url = "http://consultadminwebserver.azurewebsites.net/api/employee/";

        public async Task<EmployeeDetail> GetEmployeeById(int employeeId)
        {
            EmployeeDetail employee = null;

            var handle = Insights.TrackTime("Time_GetEmployeeById");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API for Employees

                Task<string> contentsTask = client.GetStringAsync(Url + employeeId);

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into TimeReportManager (List of TimeReports)
                employee = JsonConvert.DeserializeObject<EmployeeDetail>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "TimeReportListManager.GetEmployeeById"}
                };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetAllContacts-timer
                handle.Stop();
            }
            return employee;
        }

        public async Task<EmployeeDetail> GetEmployee(int id)
        {
            EmployeeDetail employee = new EmployeeDetail();

            var handle = Insights.TrackTime("Time_GetOneEmployee");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API with id as request args
                Task<string> contentsTask = client.GetStringAsync(Url + id.ToString());

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into Contact
                employee = JsonConvert.DeserializeObject<EmployeeDetail>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "EmployeeManager.GetEmployee"},
                    {"Key", id.ToString()}
            };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetContact-timer
                handle.Stop();
            }
            return employee;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            var handle = Insights.TrackTime("Time_GetAllEmployees");
            handle.Start();

            try
            {
                HttpClient client = new HttpClient();

                // Set pref result as JSON
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // Call the API for contacts
                Task<string> contentsTask = client.GetStringAsync(Url);

                // await! control returns to the caller and the task continues to run on another thread
                string contents = await contentsTask.ConfigureAwait(false);

                // Deserialize the JSON data into ContactManager (List of contacts)
                employees = JsonConvert.DeserializeObject<List<Employee>>(contents);
            }
            catch (Exception ex)
            {
                Dictionary<string, string> myDictionary = new Dictionary<string, string>
                {
                    {"Function", "EmployeeManager.GetAllEmployees"}
                };
                _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
            }
            finally
            {
                // Stop the GetAllEmployees-timer
                handle.Stop();
            }

            return employees;
        }

        ////*1 TODO: This is going to be replaced by something that finds the signed in Employee.
        //private EmployeeDetail GetSignedInEmployee(int employeeId, List<EmployeeDetail> employees)
        //{
        //    EmployeeDetail emp = null;
        //    foreach (EmployeeDetail employee in employees)
        //    {
        //        if (employee.EmployeeId == employeeId)
        //        {
        //            return employee;
        //        }
        //    }
        //    return emp;
        //}

        //public int FTEPercentage;
        //// Gets Employee by ID from the API
        //public async Task<List<TimeReportListViewModel>> GetTimeReportsByEmployeeId(int employeeId, string startDate, string endDate, bool Submitted)
        //{
        //    List<TimeReportListViewModel> timeReports = null;

        //    var handle = Insights.TrackTime("Time_GetTimeReportsByEmployeeId");
        //    handle.Start();

        //    try
        //    {
        //        HttpClient client = new HttpClient();

        //        // Set pref result as JSON
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // Call the API for Employees
        //        string url = Url + employeeId + "/timereports?StartDate=" + startDate + "&EndDate=" + endDate;

        //        Task<string> contentsTask = client.GetStringAsync(url);

        //        // await! control returns to the caller and the task continues to run on another thread
        //        string contents = await contentsTask.ConfigureAwait(false);

        //        // Deserialize the JSON data into TimeReportManager (List of TimeReports)
        //        timeReports = JsonConvert.DeserializeObject<List<TimeReportListViewModel>>(contents);
        //    }
        //    catch (Exception ex)
        //    {
        //        Dictionary<string, string> myDictionary = new Dictionary<string, string>
        //        {
        //            {"Function", "TimeReportListManager.GetTimeReportsByEmployeeId"}
        //        };
        //        _logger.LoggError(ex, myDictionary, (Xamarin.Insights.Severity.Error));
        //    }
        //    finally
        //    {
        //        // Stop the GetAllContacts-timer
        //        handle.Stop();
        //    }
        //    return timeReports;
        //}

        //public async Task SubmitTimeReport(DateTime startDate, DateTime endDate)
        //{
        //    string uri = $"http://consultadminwebserver.azurewebsites.net/api/TimeReportSubmit?employeeid={CurrentUser.EmployeeId}&startDate={startDate.ToString("yyyy-MM-dd")}&endDate={endDate.ToString("yyyy-MM-dd")}";
        //    var handle = Insights.TrackTime("Time-SubmitTimeReport");
        //    handle.Start();

        //    HttpClient httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    try
        //    {

        //        //var requestJSON = JsonConvert.SerializeObject(timeReport);
        //        await httpClient.PostAsync(uri, new StringContent("", Encoding.UTF8, "application/json"));

        //        _logger.LoggEvent("SubmitTimeReport", new Dictionary<string, string>()
        //        {
        //            {"EmployeeId", CurrentUser.EmployeeId.ToString()},
        //            {"StartDate", startDate.ToString("yyyy-MM-dd")},
        //            {"EndDate", endDate.ToString("yyyy-MM-dd")}
        //        });

        //        handle.Stop();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "SaveTimeReport" } }, Insights.Severity.Error);
        //    }

        //    return;
        //}            
    }
}
