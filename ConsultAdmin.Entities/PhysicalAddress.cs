using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class PhysicalAddress
    {
        public enum PhysicalAddressTypes
        {
            Home = 1,
            Office = 2
        }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public PhysicalAddressType AddressType { get; set; }
    }

    public class PhysicalAddressType
    {
        public int AddressTypeId { get; set; }
        public string AddressType { get; set; }
    }
}
