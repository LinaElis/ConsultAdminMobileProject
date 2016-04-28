using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class Contract
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ProjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //Tidrapporten rapporteras på ContractName
        public string ContractName { get; set; }
    }
}
