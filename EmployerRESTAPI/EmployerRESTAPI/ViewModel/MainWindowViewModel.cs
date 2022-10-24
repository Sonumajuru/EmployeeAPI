using EmployerRESTAPI.API;
using EmployerRESTAPI.Model;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

namespace EmployerRESTAPI.ViewModel
{
    public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        #region Fields

        private int _id;
        private string? _name;
        private string? _email;
        private string? _gender;
        private string? _status;
        private List<Employee>? _employees;
        private Employee? _selectedEmployee;
        private string? _textBoxSearch;
        private string? _displayMessage;

        #endregion

        #region Properties  

        public int ID
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                SetProperty(ref _email, value);
            }
        }
        public string Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }
        public string TextBoxSearch
        {
            get { return _textBoxSearch; }
            set
            {
                SetProperty(ref _textBoxSearch, value);
            }
        }
        public List<Employee> Employees
        {
            get { return _employees; }
            set
            {
                SetProperty(ref _employees, value);
            }
        }
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                SetProperty(ref _selectedEmployee, value);
            }
        }
        public string DisplayMessage
        {
            get { return _displayMessage; }
            set { SetProperty(ref _displayMessage, value); }
        }

        #endregion

        #region IDelegateCommands
        public DelegateCommand GetButtonClick { get; set; }
        public DelegateCommand SearchButtonClick { get; set; }
        public DelegateCommand PostButtonClick { get; set; }
        public DelegateCommand<Employee> DeleteButtonClick { get; set; }
        #endregion

        private CrudAPI crudAPI;
        private List<Employee> _employListee;

        #region Constructor
        /// <summary>
        /// Initalize perperies & delegate commands
        /// </summary>
        public MainWindowViewModel()
        {
            crudAPI = new CrudAPI();
            _employListee = new List<Employee>();
            GetButtonClick = new DelegateCommand(GetEmployees);
            SearchButtonClick = new DelegateCommand(SearchEmployees);
            PostButtonClick = new DelegateCommand(AddEmployee);
            DeleteButtonClick = new DelegateCommand<Employee>(DeleteEmployee);
        }
        #endregion

        #region Operations

        /// <summary>
        /// Gets employee data
        /// </summary>
        private async void GetEmployees()
        {
            _employListee.Clear();
            Employees = null;
            var response = await crudAPI.GetAllEmployees();
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Employee[] employees_array = JsonConvert.DeserializeObject<Employee[]>(result);
                foreach (Employee temp in employees_array)
                {
                    Employee employee = new Employee() { ID = temp.ID, Email = temp.Email, Gender = temp.Gender, Name = temp.Name, Status = temp.Status };
                    _employListee.Add(employee);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                DisplayMessage = "Search taking too long!";
            }

            Employees = _employListee;
        }

        /// <summary>
        /// Search for employees by name
        /// </summary>
        private async void SearchEmployees()
        {
            _employListee.Clear();
            Employees = null;
            var response = await crudAPI.FindEmployeeByName(_textBoxSearch);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Employee[] employees_array = JsonConvert.DeserializeObject<Employee[]>(result);
                foreach (Employee temp in employees_array)
                {
                    Employee employee = new Employee() { ID = temp.ID, Email = temp.Email, Gender = temp.Gender, Name = temp.Name, Status = temp.Status };
                    _employListee.Add(employee);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                DisplayMessage = "Employee " + _textBoxSearch + " doesn't exist";
            }
            Employees = _employListee;
        }

        /// <summary>
        /// Add employee
        /// </summary>
        private async void AddEmployee()
        {
            // Returns not authorized issue with Token verification might be
            var response = await crudAPI.AddEmployee(ID, Name, Email, Gender, Status);
            if (response.IsSuccessStatusCode)
            {
                // Display list after add
                GetEmployees();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                DisplayMessage = "Unauthorized action";
            }
        }

        /// <summary>
        /// Deletes employee data by ID
        /// </summary>
        /// <param name="employee"></param>
        private async void DeleteEmployee(Employee employee)
        {
            var response = await crudAPI.DeleteById(employee.ID);
            if (response.IsSuccessStatusCode)
            {
                // Display list after delete
                GetEmployees();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                DisplayMessage = "Delete " + employee.ID + " Unauthorized action";
            }
        }
        #endregion
    }
}
