using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class EmployeeContract 
    {
        public int EmployeeId { get; set; }

        public string EmployeeFullname { get; set; }

        public string Role { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public int ContractId { get; set; }

        public string ContractName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int FTEPercentage { get; set; }
    }
}
