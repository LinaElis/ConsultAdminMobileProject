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
        private ProjectViewModel _projectViewModel;
  

        public EmployeeProjectsPage()
        {
            _logger.LoggText("EmployeeProjectsPage");
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            _projectViewModel = new ProjectViewModel();
            base.OnAppearing();
            await _projectViewModel.FillContractList();
            BindingContext = _projectViewModel;
        }

        //private void TabbedPage_OnClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new ProjectsTabbedPage());
        //}

        //private void ProjectPage_OnClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new ProjectsPage());
        //}

        //private void ProjectsEditPage_OnClicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel()));
        //}

        //private async void EditTapped(object sender, EventArgs e)
        //{
        //    await _projectViewModel.LoadClients();
        //    await _projectViewModel.GetContract();
        //    Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel())));

        //    // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        //}

        private async void AddTapped(object sender, EventArgs e)
        {
            _projectViewModel = new ProjectViewModel();
            await _projectViewModel.LoadClients();
            Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel())));

            // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        }

        private void DeleteField(object sender, EventArgs e)
        {
            //TODO: Andvänd metoden _projectViewModel.DeleteClientContract här i knappen för att ta bort tillagt projekt.
        }
    }
}
