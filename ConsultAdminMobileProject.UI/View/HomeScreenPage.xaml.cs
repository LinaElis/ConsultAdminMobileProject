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
    public partial class HomeScreenPage : ContentPage
    {
        private readonly NewsListViewModel _newsListViewModel;
        private readonly ILogger _logger = new PCLLogger();

        public HomeScreenPage(NewsListViewModel newsListViewModel)
        {
            if (newsListViewModel != null)
            {
                _newsListViewModel = newsListViewModel;
            }
            _logger.LoggText("HomeScreenPage");
            InitializeComponent();
            BindingContext = _newsListViewModel;
        }

        private void NewsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            var newsClicked = e.Item;
            Navigation.PushAsync(new NewsDetailPage(newsClicked));
        }
    }
}
