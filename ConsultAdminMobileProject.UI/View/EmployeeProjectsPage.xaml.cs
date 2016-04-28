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
    public partial class EmployeeProjectsPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();
        private readonly ProjectViewModel _projectViewModel = new ProjectViewModel();
  

        public EmployeeProjectsPage()
        {          
            _logger.LoggText("EmployeeProjectsPage");
            InitializeComponent();
            BindingContext = _projectViewModel;
        }

        private void TabbedPage_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProjectsTabbedPage());
        }

        private void ProjectPage_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ProjectsPage());
        }

        //protected override void OnDisappearing()
        //{
        //    ToolbarItems.Clear();
        //    base.OnDisappearing();
        //}

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}

        private async void EditTapped(object sender, EventArgs e)
        {
            await _projectViewModel.LoadClients();
            Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel())));

            // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        }
        private async void AddTapped(object sender, EventArgs e)
        {
            await _projectViewModel.LoadClients();
            Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel())));

            // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        }

        //private void EditClicked(object sender, EventArgs e)
        //{
        //    Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(_navigation)));

        //    // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        //}

        //private void AddClicked(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
