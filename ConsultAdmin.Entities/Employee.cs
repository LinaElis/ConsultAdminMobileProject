using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultAdmin.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public DateTime? BirthDate { get; set; }

        public string PrimaryPhoneNo { get; set; }
        public string PrimaryEmail { get; set; }

        //Tillagd av Lina för att gå vidare med EmployeeInfoEditPage
        public string Info { get; set; }

        public List<VirtualAddress> EmployeeVirtualAddresses { get; set; }
        public List<PhysicalAddress> EmployeePhysicalAddresses { get; set; }
        public List<EmployeeContract> EmployeeContracts { get; set; }

        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }

        public string FullName => FirstName + " " + LastName;

        //Tillagd av Kalle för att kunna ta bort hela rader i från listvien XAML-koden
        public bool HasName { get { return !string.IsNullOrWhiteSpace(FullName); } }
        public bool HasLocation { get { return !string.IsNullOrWhiteSpace(LocationName); } }
        public bool HasImage { get { return !string.IsNullOrWhiteSpace(ImageURL); } }
        public bool HasTitle { get { return !string.IsNullOrWhiteSpace(Title); } }
        public bool HasPhone { get { return !string.IsNullOrWhiteSpace(PrimaryPhoneNo); } }
        public bool HasEmail { get { return !string.IsNullOrWhiteSpace(PrimaryEmail); } }
    }
}
