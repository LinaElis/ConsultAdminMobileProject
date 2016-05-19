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
        private readonly ProjectViewModel _projectViewModel;


        private readonly ILogger _logger = new PCLLogger();
    
        public EmployeeProjectsEditPage(ProjectViewModel projectViewModel)
        {
            _logger.LoggText("EmployeeProjectsEditPage");

            InitializeComponent();
            _projectViewModel = projectViewModel;       
            LoadClientNameList();
            BindingContext = _projectViewModel;

            _logger.LoggEvent("EmployeeProjectsEditPage", new Dictionary<string, string>() { { "EmployeeId", _projectViewModel.EmployeeId.ToString() } });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetClientPicker();
        }

        private async void LoadClientNameList()
        {
            try
            {
                await _projectViewModel.LoadClients();

                foreach (var client in _projectViewModel.ClientNameList)
                {
                    ClientPicker.Items.Add(client);
                }

                foreach (var contract in _projectViewModel.ContractList)
                {
                    ContractPicker.Items.Add(contract);
                }

                ClientPicker.SelectedIndex = 0;
            }

            catch (Exception error)
            {
                throw new NotImplementedException("ClientNameList could not be loaded. Error message: " + error.ToString());
            }
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

        private async void Save(object sender, EventArgs e)
        {
            var saveSuccess = await _projectViewModel.SaveProjects();

            if (saveSuccess)
            {             
                SavedValues.Text = "Saved!";
                await Task.Delay(1000);
                await Navigation.PopModalAsync();
                //await Navigation.PushModalAsync(new EmployeeProjectsPage());
            }
            else
            {
                SavedValues.Text = "Something went wrong..";
            }
        }

        private void StartDate_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _projectViewModel?.StartDateProject();
        }

        private void EndDate_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _projectViewModel?.StartDateProject();
        }
    }
}
