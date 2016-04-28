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
    public partial class NewsDetailPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();

        public NewsDetailPage(object newsClicked)
        {
            _logger.LoggText("NewsDetailPage");
            InitializeComponent();
            BindingContext = new NewsViewModel(newsClicked);
        }
    }
}
