using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class Contract
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ClientId { get; set; }

        public int ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ContractName { get; set; }

        public string ClientName { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public Collection<EmployeeContract> EmployeeContracts { get; set; }

        //public string StartDates => StartDate.ToString("dd - dddd");

        //public string EndDates => EndDate.ToString("dd - dddd");
    }
}
