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
    public partial class RelatedEmployeesProjectsPage : ContentPage
    {
        private readonly ILogger _logger = new PCLLogger();

        public RelatedEmployeesProjectsPage()
        {
            _logger.LoggText("RelatedEmployeesProjectsPage");
            InitializeComponent();
        }      
    }
}
