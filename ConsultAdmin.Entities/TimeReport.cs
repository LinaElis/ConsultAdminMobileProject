using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    using System;

    namespace ConsultAdmin.Model
    {
        public class TimeReport
        {
            public int Id { get; set; }

            public int EmployeeId { get; set; }

            public int ClientId { get; set; }

            public string ClientName { get; set; }

            public int ContractId { get; set; }

            public string ContractName { get; set; }

            public DateTime Date { get; set; }

            public TimeSpan StartTime { get; set; }

            public TimeSpan EndTime { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public TimeSpan LunchBreak { get; set; }

            public string Comments { get; set; }

            public bool Submitted { get; set; }

            public bool Invoiced { get; set; }

            public double TotalTime
            {
                get
                {
                    double TotalTime = (EndTime - StartTime).TotalDays;
                    return TotalTime;
                }
            }
            //public decimal TotalTime { get; set; }

            // To Sort TimeReports by date.
            public int CompareTo(TimeReport timeReport)
            {
                return (int)(Date - timeReport.Date).Days;
            }
        }
    }
}
