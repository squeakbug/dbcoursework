using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;

namespace ViewModel
{
    public class CommonStatusBarViewModel : BaseNotify
    {
        private string _statusString;
        private string _employeeName;
        private string _currentControlName;

        public CommonStatusBarViewModel()
        { }

        public string StatusString
        {
            get => _statusString;
            set
            {
                _statusString = value;
                NotifyPropertyChanged(nameof(StatusString));
            }
        }

        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                NotifyPropertyChanged(nameof(EmployeeName));
            }
        }

        public string CurrentControlName
        {
            get => _currentControlName;
            set
            {
                _currentControlName = value;
                NotifyPropertyChanged(nameof(CurrentControlName));
            }
        }
    }
}
