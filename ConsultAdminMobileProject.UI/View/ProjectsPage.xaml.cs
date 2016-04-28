using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Service;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.UI.View
{
    public partial class ProjectsPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();

        public ProjectsPage()
        {
            _logger.LoggText("ProjectsPage");
            InitializeComponent();
        }

        private async void TabbedPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProjectsTabbedPage());
        }

        private void ProjectsListViewItems_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
