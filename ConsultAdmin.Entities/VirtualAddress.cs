using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class VirtualAddress
    {
        public enum VirtualAddressTypes
        {
            Phone = 1,
            Email = 2
        }
        public string Address { get; set; }

        public VirtualAddressType AddressType { get; set; }
        public bool Primary { get; set; }
    }

    public class VirtualAddressType
    {
        public int AddressTypeId { get; set; }
        public string AddressType { get; set; }
    }
}
