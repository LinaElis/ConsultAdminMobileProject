using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;

namespace ConsultAdminMobileProject.Model
{
    public static class CurrentUser
    {
        public static int EmployeeId { get; set; }

        public static string UserId { get; set; }

        public static string FullName { get; set; }

        public static int LocationId { get; set; }

        public static TimeSpan StartTime { get; set; }

        public static TimeSpan EndTime { get; set; }

        public static TimeSpan LunchBreak { get; set; }

        public static int DefaultClintId { get; set; }

        public static int DefaultContractId { get; set; }

        public static List<EmployeeContract> EmployeeContracts { get; set; }
    }
}
