﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdmin.Entities.ConsultAdmin.Model;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using ConsultAdminMobileProject.Service;
using Xamarin;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.ViewModel
{
    public class ProjectViewModel : BaseViewModel
    {
        private readonly ILogger _logger = new PCLLogger();
        private readonly EmployeeManager _employeeManager = new EmployeeManager();
        private readonly ClientProjectManager _clientProjectManager = new ClientProjectManager();
        private readonly TimeReport _timeReport = new TimeReport();
        private Contract _contract = new Contract();

        private string _clientName;
        private string _contractName;
        private string _description;
        private DateTime _endDate;
        private DateTime _startDate;
        private int _clientIndex;
        private int _contractIndex;
        private int _employeeId;
        //private bool _enableSaveButton;
        private List<Contract> _contracts; 

        public ProjectViewModel()
        { }

        public int SelectedClientId { get; set; }
        public int SelectedContractId { get; set; }
        public int TimeReportId { get; set; }
        //public int ProjectId { get; set; }
        public int ContractId { get; set; }

        //public static List<int> Id { get; set; }
        public static List<DateTime> StartDateList { get; set; }
        public static List<DateTime> EndDateList { get; set; }
        public List<EmployeeContract> EmployeeContractList { get; set; }
        public List<TimeReport> ClientList { get; set; }
        public List<Contract> Contract { get; set; }
        //public List<Contract> Contract
        //{
        //    get { return _contracts; }
        //    set
        //    {
        //        if (_contracts != value)
        //            SetPropertyField(nameof(Contract), ref _contracts, value);
        //    }
        //}

        public ObservableCollection<Contract> ContractObservable
        {
            get { return _contractObservable;}
            set
            {
                if (_contractObservable != value)
                    SetPropertyField(nameof(ContractObservable), ref _contractObservable, value);
            }
        }

        private ObservableCollection<Contract> _contractObservable;


        public List<string> ClientNameList { get; set; }
        public List<int> DistinctClientIdList { get; set; }
        public List<string> ContractList { get; set; }
        public List<string> ContractNameList { get; set; }
        public List<int> ContractIdList { get; set; }

        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                if (_employeeId != value)
                SetPropertyField(nameof(EmployeeId), ref _employeeId, value);
            }
        }

        public string UserFullname { get; set; }


        public string ClientName
        {
            get { return _clientName; }
            set
            {
                if (_clientName != value)
                SetPropertyField(nameof(ClientName), ref _clientName, value);
            }
        }

        public int ClientIndex
        {
            get { return _clientIndex; }
            set
            {
                if (_clientIndex != value)
                SetPropertyField(nameof(ClientIndex), ref _clientIndex, value);
            }
        }

        public string ContractName
        {
            get { return _contractName; }
            set
            {
                if (_contractName != value)
                SetPropertyField(nameof(ContractName), ref _contractName, value);
            }
        }

        public int ContractIndex
        {
            get { return _contractIndex; }
            set
            {
                if (_contractIndex != value)
                SetPropertyField(nameof(ContractIndex), ref _contractIndex, value);
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                SetPropertyField(nameof(StartDate), ref _startDate, value);
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                SetPropertyField(nameof(EndDate), ref _endDate, value);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                SetPropertyField(nameof(Description), ref _description, value);
            }
        }

        public void ClientIndexChanged(int index)
        {
            if (ClientList.Count < index) return;
            var selectedClient = ClientNameList[index];
            var matchingClients = ClientList.Where(x => x.ClientName == selectedClient).ToList();

            ContractList.Clear();

            ContractList = new List<string>();

            if (matchingClients == null) return;
            foreach (var contract in matchingClients)
            {
                ContractList.Add(contract.ContractName);
            }
        }

        public void LoggedIn(object param)
        {
            var employeeClicked = param as Employee;
            if (employeeClicked != null) EmployeeId = employeeClicked.EmployeeId;

            if (EmployeeId == CurrentUser.EmployeeId)
            {
                EnableButton = false;
            }
        }

        public async Task LoadClients()
        {
            ClientManager manager = new ClientManager();

            List<TimeReport> clientList = await manager.GetAllClients();

            if (clientList != null && clientList.Count > 0)
            {
                ClientList = clientList;
            }

            ClientNameList = new List<string>();

            if (ClientList != null)
            {
                ClientNameList.AddRange(ClientList.Select(x => x.ClientName).Distinct());
                var cl = ClientList.FirstOrDefault();
                ContractNameList = new List<string>();
                if (cl != null)
                {
                    foreach (var client in ClientList)
                    {
                        if (client.ClientName == cl.ClientName)
                            ContractNameList.Add(client.ClientName);
                    }
                }

                ContractNameList.AddRange(ClientList.Where(x => x.ClientName == ClientList.FirstOrDefault().ClientName).Select(x => x.ContractName).Distinct());

                var firstClient = ContractNameList.First();
                var matchingClients = ClientList.Where(x => x.ClientName == firstClient).ToList();

                ContractList = new List<string>();

                foreach (var contract in matchingClients)
                {
                    ContractList.Add(contract.ContractName);
                }
            }
        }

        public List<EmployeeContract> ClientEmployeeContractList(int clientId)
        {
            return EmployeeContractList.FindAll(item => item.ClientId == clientId);
        }

        public void GetClientIdAndContractId()
        {
            var distinctClientId = ClientList.GroupBy(x => x.ClientId).Select(y => y.First());
            List<int> clientIdList = new List<int>();
            foreach (var clientId in distinctClientId)
            {
                clientIdList.Add(clientId.ClientId);
            }

            DistinctClientIdList = clientIdList;

            int selectedClientId = clientIdList[_clientIndex];

            List<int> contractIdList = new List<int>();

            foreach (var contractId in ClientList.Where(x => x.ClientId == selectedClientId))
            {
                contractIdList.Add(contractId.ContractId);
            }

            SelectedClientId = selectedClientId;
            ContractIdList = contractIdList;
            SelectedContractId = ContractIdList[ContractIndex];
        }


        public Contract CurrentContract { get; set; }

        public void StartDateProject()
        {
            try
            {
                var startDate = CurrentContract.StartDate;
                var endDate = CurrentContract.EndDate;
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void ContractSaveAndEditValues()
        {
            var clientName = ClientList.FirstOrDefault(x => x.ClientId == SelectedClientId);
            string selectedClientname = (clientName != null) ? clientName.ClientName : "";
            var contractName = ClientList.FirstOrDefault(x => x.ContractId == SelectedContractId);
            string selectedContractName = (contractName != null) ? contractName.ContractName : "";

            var startDate = StartDate.Date;
            var endDate = EndDate.Date;

            _contract.EmployeeId = CurrentUser.EmployeeId;
            _contract.ClientId = SelectedClientId;
            _contract.ClientName = selectedClientname;
            _contract.ContractName = selectedContractName;
            _contract.StartDate = startDate;
            _contract.EndDate = endDate;
        }

        public async Task<bool> SaveProjects()
        {
            try
            {
                GetClientIdAndContractId();

                ContractSaveAndEditValues();

                await _clientProjectManager.SaveContract(_contract);

                MessagingCenter.Send<ProjectViewModel>(this, "ProjectSaved");

                return true;

                SavedValues(_contract);
            }
            catch (Exception ex)
            {
                _logger.LoggError(ex, new Dictionary<string, string>() { { "Function", "DemoSaveProject_OnClicked" } }, Insights.Severity.Error);
            }
            return false;
        }

        public void SavedValues(Contract contract)
        {
            _contract = contract;
        }

        public async Task EditProjects()
        {
            SelectedContractId = ContractIdList[ContractIndex];
            _contract.Id = ContractId;
            ContractSaveAndEditValues();
            await _clientProjectManager.EditContract(_contract);
        }

        public async Task DeleteProjects()
        {
            //_contract.Id = ContractId;
            //_contract.EmployeeId = EmployeeId;
            await _clientProjectManager.DeleteContract(_contract);
        }

        public async Task GetContract()
        {
            var contractList = await _clientProjectManager.GetContract(EmployeeId, ContractId);
            //_timeReport = timeReportList.FirstOrDefault<TimeReport>();
            _contract = contractList.FirstOrDefault<Contract>();

            if (_timeReport != null)
            {
                GetClientIdAndContractId();

                if (DistinctClientIdList != null)
                {
                    for (int i = 0; i < DistinctClientIdList.Count; i++)
                    {
                        if (DistinctClientIdList[i] == _timeReport.ClientId)
                        {
                            ClientIndex = i;
                            break;
                        }
                    }

                    ClientIndexChanged(ClientIndex);

                    for (int i = 0; i < ContractIdList.Count; i++)
                    {
                        if (ContractIdList[i] == _timeReport.ContractId)
                        {
                            ContractIndex = i;
                            break;
                        }
                    }
                }

                StartDate = _contract.StartDate;
                EndDate = _contract.EndDate;
            }
        }

        public async Task FillContractList()
        {
            //UserFullname = CurrentUser.FullName;
            ContractManager cm = new ContractManager();
            var contracts = await cm.GetAllContracts();
                    ContractObservable = new ObservableCollection<Contract>();
            if (contracts != null)
                foreach (var contract in contracts)
                {
                    ContractObservable.Add(contract);
                }
            //List<Contract> contract = await cm.GetAllContracts();

            //Contract = new List<Contract>(contract);

            //if (contract != null && contract.Count > 0)
            //{
            //    Contract = contract;
            //}

            //var _cont = contract;

            //for (int i = 0; i < _cont.Count; i++)
            //{
            //    Contract.Add(new Contract()
            //    {
            //        ClientName = _cont.Select(x => x.ClientName).ToString(),
            //        ContractName = _cont.Select(x => x.ContractName).ToString()
            //    });
            //}

            string s = "";
            //foreach (var contracts in contract)
            //{
            //        Contract.Add(contracts);
            //}
        }

        //public async Task GetClientContractToFillValues()
        //{

        //    var clientContractList = await _employeeManager.GetClientContract(EmployeeId, TimeReportId);
        //    _timeReport = clientContractList.FirstOrDefault<TimeReport>();

        //    if (_timeReport != null)
        //    {
        //        GetClientIdAndContractId();

        //        if (DistinctClientIdList != null)
        //        {
        //            for (int i = 0; i < DistinctClientIdList.Count; i++)
        //            {
        //                if (DistinctClientIdList[i] == _timeReport.ClientId)
        //                {
        //                    ClientIndex = i;
        //                    break;
        //                }
        //            }

        //            ClientIndexChanged(ClientIndex);

        //            for (int i = 0; i < ContractIdList.Count; i++)
        //            {
        //                if (ContractIdList[i] == _timeReport.ContractId)
        //                {
        //                    ContractIndex = i;
        //                    break;
        //                }
        //            }
        //        }

        //        StartDate = _timeReport.StartDate;
        //        EndDate = _timeReport.EndDate;
        //    }
        //}

    }
}
