using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdmin.Entities.ConsultAdmin.Model;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using ConsultAdminMobileProject.Service;

namespace ConsultAdminMobileProject.ViewModel
{
    public class ProjectViewModel : BaseViewModel
    {
        private readonly ILogger _logger = new PCLLogger();
        private readonly EmployeeManager _employeeManager = new EmployeeManager();
        private readonly ClientProjectManager _clientProjectManager = new ClientProjectManager();
        private TimeReport _timeReport = new TimeReport();


        private string _clientName;
        private string _contractName;
        private TimeSpan _endDate;
        private TimeSpan _startDate;
        private int _clientIndex;
        private int _contractIndex;
        private int _employeeId;
        private bool _enableSaveButton;

        public ProjectViewModel()
        { }

        public int SelectedClientId { get; set; }
        public int SelectedContractId { get; set; }
        public int TimeReportId { get; set; }
        public int ProjectId { get; set; }

        //public static List<int> Id { get; set; }
        public static List<DateTime> StartDateList { get; set; }
        public static List<DateTime> EndDateList { get; set; }
        public List<EmployeeContract> EmployeeContractList { get; set; }
        public List<TimeReport> ClientList { get; set; }
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
                    EnableSaveButton = true;
                SetPropertyField(nameof(EmployeeId), ref _employeeId, value);
            }
        }

        public bool EnableSaveButton
        {
            get
            {
                return _enableSaveButton;
            }
            set { SetPropertyField(nameof(EnableSaveButton), ref _enableSaveButton, value); }
        }

        public string ClientName
        {
            get { return _clientName; }
            set
            {
                if (_clientName != value)
                    EnableSaveButton = true;
                SetPropertyField(nameof(ClientName), ref _clientName, value);
            }
        }

        public int ClientIndex
        {
            get { return _clientIndex; }
            set
            {
                if (_clientIndex != value)
                    EnableSaveButton = true;
                SetPropertyField(nameof(ClientIndex), ref _clientIndex, value);
            }
        }

        public string ContractName
        {
            get { return _contractName; }
            set
            {
                if (_contractName != value)
                    EnableSaveButton = true;
                SetPropertyField(nameof(ContractName), ref _contractName, value);
            }
        }

        public int ContractIndex
        {
            get { return _contractIndex; }
            set
            {
                if (_contractIndex != value)
                    EnableSaveButton = true;
                SetPropertyField(nameof(ContractIndex), ref _contractIndex, value);
            }
        }

        public TimeSpan StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                    EnableSaveButton = true;
                SetPropertyField(nameof(StartDate), ref _startDate, value);
            }
        }

        public TimeSpan EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                    EnableSaveButton = true;
                SetPropertyField(nameof(EndDate), ref _endDate, value);
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

        public async Task SaveProjects()
        {
            GetClientIdAndContractId();
            TimeReportSaveAndEditValues();
            await _clientProjectManager.SaveClientContract(_timeReport);
        }

        public async Task EditProjects()
        {
            SelectedContractId = ContractIdList[ContractIndex];
            _timeReport.Id = TimeReportId;
            TimeReportSaveAndEditValues();
            await _clientProjectManager.EditClientContract(_timeReport);
        }

        public async Task DeleteProjects()
        {
            _timeReport.Id = ProjectId;
            _timeReport.EmployeeId = EmployeeId;
            await _clientProjectManager.DeleteClientContract(_timeReport);
        }

        public void TimeReportSaveAndEditValues()
        {
            var clientName = ClientList.FirstOrDefault(x => x.ClientId == SelectedClientId);
            string selectedClientname = (clientName != null) ? clientName.ClientName : "";
            var contractName = ClientList.FirstOrDefault(x => x.ContractId == SelectedContractId);
            string selectedContractName = (contractName != null) ? contractName.ContractName : "";

            _timeReport.EmployeeId = CurrentUser.EmployeeId;
            _timeReport.ClientId = SelectedClientId;
            _timeReport.ClientName = selectedClientname;
            _timeReport.ContractId = SelectedContractId;
            _timeReport.ContractName = selectedContractName;
            _timeReport.StartDate = StartDate;
            _timeReport.EndDate = EndDate;
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
