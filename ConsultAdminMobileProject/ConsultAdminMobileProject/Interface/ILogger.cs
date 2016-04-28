using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin;

namespace ConsultAdminMobileProject.Interface
{
    public interface ILogger
    {
        void SetUser(string ID, string FirstName, string LastName);

        void LoggEvent(string logevent, string key, string value);

        void LoggEvent(string logevent, Dictionary<string, string> dictionary);

        void LoggText(string text);

        void LoggError(string text);

        void LoggError(Exception exception, Insights.Severity severity = Insights.Severity.Error);

        void LoggError(Exception exception, string key, string value);

        void LoggError(Exception exception, Dictionary<string, string> dictionary, Insights.Severity severity = Insights.Severity.Error);
    }
}
