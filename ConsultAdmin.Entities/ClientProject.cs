using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class ClientProject
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ProjectName { get; set; }
    }
}

