using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Service;
using ConsultAdminMobileProject.ViewModel;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.UI.View
{
    public partial class EmployeeGeneralPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();
        private readonly EmployeeViewModel _employeeViewModel;

        public EmployeeGeneralPage(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel != null)
            {
                _employeeViewModel = employeeViewModel;
            }
            _logger.LoggText("EmployeeGeneralPage");
            InitializeComponent();
            BindingContext = _employeeViewModel;
        }

        private async void HomeAddressTapped(object sender, EventArgs e)
        {
            if (_employeeViewModel.HasHomeAddress)
            {
                await _employeeViewModel.NavigateToClickedHomeAddress();
            }
        }

        private async void WorkAddressTapped(object sender, EventArgs e)
        {
            if (_employeeViewModel.HasWorkAddress)
            {
                await _employeeViewModel.NavigateToClickedWorkAddress();
            }
        }
    }
}
