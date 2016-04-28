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
    public partial class HomeScreenMenuPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();

        public HomeScreenMenuPage()
        {
            ////TODO: make padding work for hamburger icon

            _logger.LoggText("HomeScreenMenuPage");
            InitializeComponent();
        }

        private async void Home_OnClicked(object sender, EventArgs e)
        {
            NewsListViewModel newsListModel = new NewsListViewModel();
            await newsListModel.FillNewsList();
            App.MasterDetailPage.Detail = new NavigationPage(new HomeScreenPage(newsListModel));
            App.MasterDetailPage.IsPresented = false;
        }

        private async void MenuEmployees_OnClicked(object sender, EventArgs e)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new EmployeeListViewPage());
            App.MasterDetailPage.IsPresented = false;
        }

        private async void MenuProjects_OnClicked(object sender, EventArgs e)
        {
            App.MasterDetailPage.Detail = new NavigationPage(new ProjectsTabbedPage());
            App.MasterDetailPage.IsPresented = false;
        }

        private async void MenuLogout_OnClicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Close the application", "Do you also want to logout from the application?", "Yes", "No");
            if (response == true)
            {
                LocalStorageManager localStorage = new LocalStorageManager();
                localStorage.ClearUseridAndPassword();
            }
            DependencyService.Get<IApplicationUtilities>().CloseApp();
        }

        //private async void MenuTimeReports_OnClicked(object sender, EventArgs e)
        //{
        //    //TimeReportListViewModel timeReportListViewModel = new TimeReportListViewModel();
        //    //NavigationPage navigationPage = new NavigationPage();
        //    //App.MasterDetailPage.Detail = new NavigationPage(new TimeReportListPage(navigationPage));
        //    //App.MasterDetailPage.IsPresented = false;
        //}

        //private async void MenuSettings_OnClicked(object sender, EventArgs e)
        //{
        //    //TimeReportViewModel timeReportViewModel = new TimeReportViewModel();
        //    //await timeReportViewModel.LoadClients();
        //    //App.MasterDetailPage.Detail = new NavigationPage(new SettingsPage());
        //    //App.MasterDetailPage.IsPresented = false;

        //    //TimeReportViewModel timeReportViewModel = new TimeReportViewModel();
        //    //await timeReportViewModel.LoadClients();
        //    //App.MasterDetailPage.Detail = new NavigationPage(new SettingsPage(timeReportViewModel));
        //    //App.MasterDetailPage.IsPresented = false;
        //}     
    }
}
