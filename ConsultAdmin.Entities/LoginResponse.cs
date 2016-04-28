using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class LoginReponse
    {
        public int StatusCode { get; set; }

        public string StatusString { get; set; }

        public int EmployeeId { get; set; }

        public int LocationId { get; set; }

        public string FullName { get; set; }

        public string UserId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan LunchBreak { get; set; }

        public int DefaultClintId { get; set; }

        public int DefaultContractId { get; set; }

        public int FTEPercentage { get; set; }

        public List<EmployeeContract> EmployeeContracts { get; set; }

    }
}
