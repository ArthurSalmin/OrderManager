using OrderManager.Commands;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ViewModels.EntitiesViewModels
{
    class ProjectViewModel : PageContextViewModel, INotifyPropertyChanged
    {
        private ManagerContext _db;

        private string _name;
        private string _timeOfImplementation;
        private string _plannedBudget;
        private string _realBudget;
        private string _projectStatus;
        private string _customer;
        private string _responsibleExecutor;

        private RelayCommand _getProject;

        public ProjectViewModel()
        {
            _db = new ManagerContext();
        }

        public override Uri PageUri
        {
            get
            {
                return _pageUri;
            }
            set
            {
                _pageUri = value;
                OnPropertyChanged("PageUri");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string TimeOfImplementation
        {
            get { return _timeOfImplementation; }
            set
            {
                _timeOfImplementation = value;
                OnPropertyChanged("TimeOfImplementation");
            }
        }

        public string PlannedBudget
        {
            get { return _plannedBudget; }
            set
            {
                _plannedBudget = value;
                OnPropertyChanged("PlannedBudget");
            }
        }

        public string RealBudget
        {
            get { return _realBudget; }
            set
            {
                _realBudget = value;
                OnPropertyChanged("RealBudget");
            }
        }

        public string ProjectStatus
        {
            get { return _projectStatus; }
            set
            {
                _projectStatus = value;
                OnPropertyChanged("ProjectStatus");
            }
        } 

        public string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
            }
        }

        public string ResponsibleExecutor
        {
            get { return _responsibleExecutor; }
            set
            {
                _responsibleExecutor = value;
                OnPropertyChanged("ResponsibleExecutor");
            }
        }

        public RelayCommand GetProject
        {
            get
            {
                return _getProject ??
                    (_getProject = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Projects project = selectedItem as Projects;
                        _name = project.Name;
                        _timeOfImplementation = project.TimeOfImplementation;
                        _plannedBudget = project.PlannedBudget.ToString();
                        _projectStatus = project.ProjectStatus;
                        _customer = _db.Customers.FirstOrDefault(x => x.Id == project.IdCustomer).FullName;
                        _responsibleExecutor = _db.Executors.FirstOrDefault(x => x.Id == project.IdResponsibleExecutor).FullName;
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
