using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Service;
using ConsultAdminMobileProject.ViewModel;
using Xamarin.Forms;
using System.ComponentModel;

namespace ConsultAdminMobileProject.UI.View
{
    public partial class EmployeeProjectsEditPage : ContentPage
    {
        private readonly ProjectViewModel _projectViewModel = new ProjectViewModel();

        private readonly ILogger _logger = new PCLLogger();
        private List<int> startDate = new List<int>();
        private List<string> endDate = new List<string>();

        public EmployeeProjectsEditPage(ProjectViewModel projectViewModel)
        {
            _logger.LoggText("EmployeeProjectsEditPage");

            InitializeComponent();

            _projectViewModel = projectViewModel;

            //LoadClientNameList();


            if (_projectViewModel.ClientNameList != null)
            {
                foreach (var client in _projectViewModel.ClientNameList)
                {
                    ClientPicker.Items.Add(client);
                }

                foreach (var contract in _projectViewModel.ContractList)
                {
                    ContractPicker.Items.Add(contract);
                }
            }

            if (_projectViewModel.ClientNameList != null)
            {
                foreach (var client in _projectViewModel.ClientNameList)
                {
                    ClientPicker.Items.Add(client);
                    ClientPicker.SelectedIndex = _projectViewModel.ClientIndex;
                }

                foreach (var contract in _projectViewModel.ContractList)
                {
                    ContractPicker.Items.Add(contract);
                    ClientPicker.SelectedIndex = _projectViewModel.ContractIndex;
                }
            }

            BindingContext = _projectViewModel;

            _logger.LoggEvent("EmployeeProjectsEditPage", new Dictionary<string, string>() { { "EmployeeId", _projectViewModel.EmployeeId.ToString() } });
        }

        private async void LoadClientNameList()
        {
            try
            {
                await _projectViewModel.LoadClients();
            }

            catch (Exception error)
            {
                throw new NotImplementedException("ClientNameList could not be loaded. Error message: " + error.ToString());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetClientPicker();
        }

        private void SetClientPicker()
        {
            ClientPicker.SelectedIndexChanged += (sender, args) =>
            {
                _projectViewModel.ClientIndexChanged(ClientPicker.SelectedIndex);

                ContractPicker.Items.Clear();

                foreach (var contract in _projectViewModel.ContractList)
                {
                    ContractPicker.Items.Add(contract);
                }

                if (ContractPicker.Items.Any())
                {
                    ContractPicker.SelectedIndex = 0;
                }
            };
        }

        //private void SetDatePicker()
        //{
        //    DateTime inspected_at = DateTime.Now;

        //    int year = inspected_at.Year;
        //    int month = inspected_at.Month;
        //    int day = inspected_at.Day;

        //    StartDate.
        //}

        private void DeleteField(object sender, EventArgs e)
        {
            //TODO: Andvänd metoden _projectViewModel.DeleteClientContract här i knappen för att ta bort tillagt projekt.
        }

        private async void Save(object sender, EventArgs e)
        {
            try
            {
                await _projectViewModel.SaveProjects();
                SavedValues.Text = "Time report has been saved!";
            }
            catch (Exception)
            {
                SavedValues.Text = "Something went wrong..";
            }
        }
    }
}
