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
    public partial class DescriptionProjectsPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();
        private ProjectViewModel _projectViewModel;

        public DescriptionProjectsPage()
        {
            _logger.LoggText("DescriptionProjectsPage");

            InitializeComponent();
            _projectViewModel = new ProjectViewModel();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _projectViewModel.FillContractList();
            BindingContext = _projectViewModel;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //TODO: När man klickar på ett projekt kommer man till fliken Employees automatiskt, lista med anställda som hör till valt projekt.

            Navigation.PushModalAsync(new RelatedEmployeesProjectsPage());
        }
    }
}
