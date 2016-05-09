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
  

        public EmployeeProjectsPage(ProjectViewModel projectViewModel)
        {
            if (projectViewModel != null)
            {
                _projectViewModel = projectViewModel;
            }

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

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}

        private async void EditTapped(object sender, EventArgs e)
        {
            await _projectViewModel.LoadClients();
            await _projectViewModel.GetContract();
            Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel())));

            // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        }
        private async void AddTapped(object sender, EventArgs e)
        {
            await _projectViewModel.LoadClients();
            Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel())));

            // TODO: Gå till editläge på knappen. Om id == inloggat id syns ikonen och går att använda, annars disabla.
        }

        private void ProjectsEditPage_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EmployeeProjectsEditPage(new ProjectViewModel()));
        }

        private void DeleteField(object sender, EventArgs e)
        {
            //TODO: Andvänd metoden _projectViewModel.DeleteClientContract här i knappen för att ta bort tillagt projekt.
        }
    }
}
