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
    public partial class EmployeeListViewPage : ContentPage
    {
        private readonly EmployeeListViewModel _employeeListViewModel;
        private readonly ProjectViewModel _projectViewModel;
        private readonly ILogger _logger = new PCLLogger();

        public EmployeeListViewPage()
        {
            _employeeListViewModel = new EmployeeListViewModel();

            _logger.LoggText("EmployeeListViewPage");

            InitializeComponent();

            BindingContext = _employeeListViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var result = await _employeeListViewModel.FillEmployeeList();
        }

        private async void EmployeeListViewItems_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            var employeeClicked = e.Item;
            EmployeeViewModel employeeViewModel = new EmployeeViewModel(employeeClicked);
            await employeeViewModel.GetExtendedEmployee();

            ProjectViewModel projectViewModels = new ProjectViewModel();
            await projectViewModels.FillContractList();

            ProjectViewModel projectViewModel = new ProjectViewModel();
            projectViewModel.LoggedIn(employeeClicked);

            await Navigation.PushAsync(new EmployeeTabbedRootPage(employeeViewModel));
        }
    }
}
