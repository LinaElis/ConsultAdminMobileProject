using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConsultAdmin.Entities;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Service;
using Plugin.ExternalMaps;
using Xamarin;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly ILogger _logger = new PCLLogger();
        private string _info;

        public string Info
        {
            get { return _info; }
            set
            {
                if (_info != value)
                    EnableButton = true;
                SetPropertyField(nameof(Info), ref _info, value);
            }
        }

        public EmployeeDetail EmployeeDetailModel { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public DateTime? BirthDate { get; set; }

        public string FullName { get; }

        //Virtual Addresses
        public string PrimaryPhone { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public string PrimaryEmail { get; set; }
        public string AdditionalEmail { get; set; }

        //Physical Addresses
        public List<PhysicalAddress> EmployeePhysicalAddresses { get; set; }
        public List<VirtualAddress> EmployeeVirtualAddresses { get; set; }

        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }

        //Bool check all of the virtual addresses to see if they contain anything and bind the IsVisible property to it in the view (hides the empty ones)

        public bool HasPrimaryPhone { get { return !string.IsNullOrWhiteSpace(PrimaryPhone); } }
        public bool HasHomePhone { get { return !string.IsNullOrWhiteSpace(HomePhone); } }
        public bool HasOfficePhone { get { return !string.IsNullOrWhiteSpace(OfficePhone); } }
        public bool HasAnyPhone { get { return (HasPrimaryPhone && HasHomePhone && HasOfficePhone); } }
        public bool HasPrimaryEmail { get { return !string.IsNullOrWhiteSpace(PrimaryEmail); } }
        public bool HasAdditionalEmail { get { return !string.IsNullOrWhiteSpace(AdditionalEmail); } }

        //Bool check all of the physical addresses to see if they contain anything and bind the IsVisible property to it in the view (hides the empty ones)
        public bool HasHomeAddress { get { return !string.IsNullOrWhiteSpace(HomeAddress); } }
        public bool HasWorkAddress { get { return !string.IsNullOrWhiteSpace(WorkAddress); } }

        public EmployeeViewModel(object param)
        {
            var employeeClicked = param as Employee;
            if (employeeClicked != null)
            {
                EmployeeId = employeeClicked.EmployeeId;
                FirstName = employeeClicked.FirstName;
                LastName = employeeClicked.LastName;
                LocationID = employeeClicked.LocationID;
                LocationName = employeeClicked.LocationName;
                ImageURL = employeeClicked.ImageURL;
                Title = employeeClicked.Title;
                FullName = employeeClicked.FullName;

                Dictionary<string, string> myDict = new Dictionary<string, string>
                {
                    {"EmployeeId", EmployeeId.ToString()},
                    {"FirstName", FirstName},
                    {"LastName", LastName},
                    {"LocationID", LocationID.ToString()}
                };
                _logger.LoggEvent("Load EmployeeViewModel", myDict);
            }
            TapCommand = new Command(OnTapped);
        }

        public EmployeeViewModel() { }

        public async Task GetExtendedEmployee()
        {
            var handle = Insights.TrackTime("GetExtendedEmployee", new Dictionary<string, string>() { { "EmployeeId", EmployeeId.ToString() } });
            handle.Start();

            EmployeeManager em = new EmployeeManager();
            EmployeeDetail employeeDetailModel = await em.GetEmployee(EmployeeId);
            if (employeeDetailModel != null)
            {

                var primaryEmail = employeeDetailModel.EmployeeVirtualAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 4 && x.Primary);
                PrimaryEmail = (primaryEmail != null) ? primaryEmail.Address : "";

                var additionalEmail = employeeDetailModel.EmployeeVirtualAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 4 && !x.Primary);
                AdditionalEmail = (additionalEmail != null) ? additionalEmail.Address : "";

                var primaryPhone = employeeDetailModel.EmployeeVirtualAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 1);
                PrimaryPhone = (primaryPhone != null) ? primaryPhone.Address : "";

                var homePhone = employeeDetailModel.EmployeeVirtualAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 2);
                HomePhone = (homePhone != null) ? homePhone.Address : "";

                var officePhone = employeeDetailModel.EmployeeVirtualAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 3);
                OfficePhone = (officePhone != null) ? officePhone.Address : "";

                var homeAddress = employeeDetailModel.EmployeePhysicalAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 1);
                var homeStreetAddress = (homeAddress != null) ? string.Format("{0} {1}", homeAddress.AddressLine1, homeAddress.AddressLine2) : "";
                var homeZipAndArea = (homeAddress != null) ? string.Format("{0} {1}", homeAddress.ZipCode, homeAddress.City) : "";
                HomeAddress = (homeAddress != null) ? (homeStreetAddress + Environment.NewLine + homeZipAndArea) : "";

                var workAddress = employeeDetailModel.EmployeePhysicalAddresses.FirstOrDefault(x => x.AddressType.AddressTypeId == 2);
                var workStreetAddress = (homeAddress != null) ? string.Format("{0} {1}", workAddress.AddressLine1, workAddress.AddressLine2) : "";
                var workZipAndArea = (homeAddress != null) ? string.Format("{0} {1}", workAddress.ZipCode, workAddress.City) : "";
                WorkAddress = (workAddress != null) ? (workStreetAddress + Environment.NewLine + workZipAndArea) : "";

                var employeePhysicalAddresses = employeeDetailModel.EmployeePhysicalAddresses;
                EmployeePhysicalAddresses = employeePhysicalAddresses;

                var employeeVirtualAddresses = employeeDetailModel.EmployeeVirtualAddresses;
                EmployeeVirtualAddresses = employeeVirtualAddresses;
            }
            handle.Stop();
        }

        public async Task NavigateToClickedHomeAddress()
        {
            string countryCode = string.Empty;
            if (EmployeePhysicalAddresses != null)
            {
                foreach (var address in EmployeePhysicalAddresses.Where(x => x.AddressType.AddressTypeId == 1))
                {
                    await CrossExternalMaps.Current.NavigateTo(FullName, address.AddressLine2, LocationName, address.AddressLine1, address.City, address.Country, countryCode);
                }
            }
        }

        public async Task NavigateToClickedWorkAddress()
        {
            string countryCode = string.Empty;
            if (EmployeePhysicalAddresses != null)
            {
                foreach (var address in EmployeePhysicalAddresses.Where(x => x.AddressType.AddressTypeId == 2))
                {
                    await CrossExternalMaps.Current.NavigateTo(FullName, address.AddressLine2, LocationName, address.AddressLine1, address.City, address.Country, countryCode);
                }
            }
        }

        public ICommand TapCommand { get; }

        private void OnTapped(object s)
        {
            var mailAddress = EmployeeVirtualAddresses.Where(x => x.AddressType.AddressTypeId == 4);

            if (s == null) return;
            foreach (var address in mailAddress)
            {
                if (s.ToString() == address.Address)
                {
                    Device.OpenUri(new Uri(string.Format("mailto:{0}", s)));
                    continue;
                }
                else
                {
                    Device.OpenUri(new Uri(string.Format("tel:{0}", s)));
                }
            }
        }
    }
}
