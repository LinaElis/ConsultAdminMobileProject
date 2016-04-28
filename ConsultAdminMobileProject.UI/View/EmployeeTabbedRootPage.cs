using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.ViewModel;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.UI.View
{
    public class EmployeeTabbedRootPage : TabbedPage
    {
        public EmployeeTabbedRootPage(EmployeeViewModel employeeViewModel)
        {
            this.Children.Add(new EmployeeGeneralPage(employeeViewModel) { Title = "General" });
            this.Children.Add(new EmployeeProjectsPage() { Title = "Project" });
        }
    }
}
