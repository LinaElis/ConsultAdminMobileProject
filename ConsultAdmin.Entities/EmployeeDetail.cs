using System;
using System.Collections.Generic;

namespace ConsultAdmin.Entities
{
    public class EmployeeDetail : Employee
    {
        public int FTEPercentage { get; set; }
        public new List<VirtualAddress> EmployeeVirtualAddresses { get; set; } = new List<VirtualAddress>();
        public new List<PhysicalAddress> EmployeePhysicalAddresses { get; set; } = new List<PhysicalAddress>();
        public new List<EmployeeContract> EmployeeContracts { get; set; } = new List<EmployeeContract>();
    }
}
