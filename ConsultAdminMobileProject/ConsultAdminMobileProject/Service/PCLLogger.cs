using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.Interface;
using Xamarin;

namespace ConsultAdminMobileProject.Service
{
    public class PCLLogger : ILogger
    {
        public void LoggError(string text)
        {
            Insights.Track(text);
        }

        public void LoggError(Exception exception, Insights.Severity severity = Insights.Severity.Error)
        {
            try
            {
                if (Insights.IsInitialized)
                {
                    Insights.Report(exception, severity);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void LoggError(Exception exception, Dictionary<string, string> dictionary, Insights.Severity severity = Insights.Severity.Error)
        {
            try
            {
                if (Insights.IsInitialized)
                {
                    Insights.Report(exception, dictionary, severity);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void LoggError(Exception exception, string key, string value)
        {
            try
            {
                if (Insights.IsInitialized)
                {
                    Insights.Report(exception, key, value);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void LoggEvent(string logevent, Dictionary<string, string> dictionary)
        {
            try
            {
                if (Insights.IsInitialized)
                {
                    Insights.Track(logevent, dictionary);
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void LoggEvent(string logevent, string key, string value)
        {
            try
            {
                if (Insights.IsInitialized)
                {
                    Insights.Track(logevent, new Dictionary<string, string> { { key, value } });
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void LoggText(string text)
        {
            Insights.Track(text);
        }

        public void SetUser(string ID, string FirstName, string LastName)
        {
            //try
            //{
            //    if (Insights.IsInitialized)
            //    {
            //        var traits = new Dictionary<string, string>
            //        {
            //            {Insights.Traits.FirstName, FirstName},
            //            {Insights.Traits.LastName, LastName}
            //        };
            //        Insights.Identify(IdrottID, traits);
            //    }
            //}
            //catch (Exception)
            //{
            //    //throw;
            //}
        }
    }
}
