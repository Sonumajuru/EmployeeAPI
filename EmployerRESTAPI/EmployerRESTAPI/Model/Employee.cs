using System.Collections.Generic;
using System.ComponentModel;
using Prism.Mvvm;

namespace EmployerRESTAPI.Model
{
    public class Employee : BindableBase, INotifyPropertyChanged
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
    }
}
