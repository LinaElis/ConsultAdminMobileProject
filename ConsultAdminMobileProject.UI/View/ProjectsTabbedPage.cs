using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.ViewModel;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.UI.View
{
    public class ProjectsTabbedPage : TabbedPage
    {
        public ProjectsTabbedPage()
        {
            this.Children.Add(new DescriptionProjectsPage() { Title = "Description" });
            this.Children.Add(new RelatedEmployeesProjectsPage() { Title = "Employees" });
        }     
    }

}
