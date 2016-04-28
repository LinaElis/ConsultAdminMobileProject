using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdminMobileProject.Service;

namespace ConsultAdminMobileProject.ViewModel
{
    public class EmployeeListViewModel : BaseViewModel
    {
        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                if (_employees != value)
                {
                    SetPropertyField(nameof(Employees), ref _employees, value);
                }
            }
        }


        public EmployeeListViewModel() { }

        public async Task<bool> FillEmployeeList()
        {
            var employeeManager = new EmployeeManager();
            var employees = await employeeManager.GetAllEmployees();
            if (employees != null && employees.Count > 0)
            {
                Employees = new ObservableCollection<Employee>(employees);

                return true;
            }
            return false;
        }

    }
}
