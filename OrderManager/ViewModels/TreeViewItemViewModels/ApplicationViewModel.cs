using OrderManager.Commands;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrderManager.ViewModels.TreeViewItemViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private ManagerContext _db;

        private RelayCommand _addProject;
        private RelayCommand _deleteProject;
        private RelayCommand _addQuest;
        private RelayCommand _deleteQuest;
        private RelayCommand _addCustomer;
        private RelayCommand _deleteCustomer;
        private RelayCommand _addExecutor;
        private RelayCommand _deleteExecutor;

        //public TreeViewItemExecutorViewModel _treeViewItemExecutorViewModel { get; set; }
        //public TreeViewItemCustomerViewModel _treeViewItemCustomerViewModel { get; set; }
        //public TreeViewItemProjectViewModel _treeViewItemProjectViewModel { get; set; }
        //public TreeViewItemQuestViewModel _treeViewItemQuestViewModel { get; set; }

        
        
        public ObservableCollection<TreeViewItemCustomerViewModel> CustomersModel { get; set; } =
            new ObservableCollection<TreeViewItemCustomerViewModel>();

        public ObservableCollection<TreeViewItemExecutorViewModel> ExecutorsModel { get; set; } =
            new ObservableCollection<TreeViewItemExecutorViewModel>();
        
        public ObservableCollection<TreeViewItemProjectViewModel> ProjectsModel { get; set; } =
            new ObservableCollection<TreeViewItemProjectViewModel>();

        private string _pageCustomerPath = "/Pages/CustomerInformationPage.xaml";
        public string PageCustomerPath
        {
            get { return _pageCustomerPath; }
            set
            {
                _pageCustomerPath = value;
                OnPropertyChanged("PageCustomerPath");
            }
        }

        public ApplicationViewModel()
        {
            _db = new ManagerContext();
            var projects = _db.Projects.Select(GetProject).ToList();
            foreach (var item in projects)
            {
                ProjectsModel.Add(item);
            }

            var customers = _db.Customers.Select(GetCustomer).ToList();
            foreach (var item in customers)
            {
                CustomersModel.Add(item);
            }

            var executors = _db.Executors.Select(GetExecutor).ToList();
            foreach (var item in executors)
            {
                ExecutorsModel.Add(item);
            }

            //_treeViewItemCustomerViewModel = new TreeViewItemCustomerViewModel();
            //_treeViewItemExecutorViewModel = new TreeViewItemExecutorViewModel();
            //_treeViewItemProjectViewModel = new TreeViewItemProjectViewModel();
            //_treeViewItemQuestViewModel = new TreeViewItemQuestViewModel();
        }
        
        public RelayCommand AddProject
        {
            get
            {
                return _addProject ??
                    (_addProject = new RelayCommand((o) =>
                    {
                        Projects newProject = new Projects();
                        _db.Projects.Add(newProject);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteProject
        {
            get
            {
                return _deleteProject ??
                    (_deleteProject = new RelayCommand((selectedItem) => 
                    {
                        if (selectedItem == null) return;
                        Projects deletedProject = selectedItem as Projects;
                        _db.Projects.Remove(deletedProject);
                        _db.SaveChanges();
                    }));
            }
        }
        private RelayCommand _getPath;
        private string _pathPage;
        public string PathPage
        {
            get { return _pathPage; }
            set
            {
                _pathPage = value;
                OnPropertyChanged("GetPath");
            }
        }

        private RelayCommand _selectedItem;
        public RelayCommand SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("GetPath");
            }
        }

        public RelayCommand GetPath
        {
            get
            {
                return _getPath ??
                    (_getPath = new RelayCommand((selectedItem)=> 
                    {
                        if (selectedItem == null) return;
                        TreeViewItemViewModel<object> f = selectedItem as TreeViewItemViewModel<object>;
                        PathPage = f.Context.PageUri.AbsolutePath;
                    }));
            }
        }

        //private RelayCommand _getCustomerName;
        //private string _imagePath;
        //private string _name;
        //private string _adressInformation;
        //public RelayCommand GetCustomerName
        //{
        //    get
        //    {
        //        return _getCustomerName ??
        //            (_getCustomerName = new RelayCommand((SelectedItem) =>
        //            {
        //                if (SelectedItem == null) return;
        //                string NameCustomer = SelectedItem as string;
        //                //Customers customer = selectedItem as Customers;
        //                Customers customer = _db.Customers.FirstOrDefault(x => x.FullName == NameCustomer);

        //                _imagePath = customer.Image;
        //                _name = customer.FullName;
        //                _adressInformation = customer.AdressInformation;
        //            }));
        //    }
        //}

        public RelayCommand AddQuest
        {
            get
            {
                return _addQuest ??
                    (_addQuest = new RelayCommand((o) =>
                    {
                        Quests newQuest = new Quests();
                        _db.Quests.Add(newQuest);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteQuest
        {
            get
            {
                return _deleteQuest ??
                    (_deleteQuest = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Quests deletedQuest = selectedItem as Quests;
                        _db.Quests.Remove(deletedQuest);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand AddExecutor
        {
            get
            {
                return _addExecutor ??
                    (_addExecutor = new RelayCommand((o) =>
                    {
                        Executors newExecutor = new Executors();
                        _db.Executors.Add(newExecutor);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteExecutor
        {
            get
            {
                return _deleteExecutor ??
                    (_deleteExecutor = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Executors deletedExecutor = selectedItem as Executors;
                        _db.Executors.Remove(deletedExecutor);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand AddCustomer
        {
            get
            {
                return _addCustomer ??
                    (_addCustomer = new RelayCommand((o) =>
                    {
                        Customers newCustomer = new Customers();
                        _db.Customers.Add(newCustomer);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteCustomer
        {
            get
            {
                return _deleteCustomer ??
                    (_deleteCustomer = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Customers deletedCustomer = selectedItem as Customers;
                        _db.Customers.Remove(deletedCustomer);
                        _db.SaveChanges();
                    }));
            }
        }

        private TreeViewItemProjectViewModel GetProject(Projects project)
        {
            string responsibleExecutorName = _db.Executors.FirstOrDefault(y => y.Id == project.IdResponsibleExecutor).FullName;
            string customerName = _db.Customers.FirstOrDefault(y => y.IdProject == project.Id).FullName;
            var selectedQuest = from quest in _db.Quests
                                where quest.IdProject == project.Id
                                select quest;
            ObservableCollection<Quests> Quests = new ObservableCollection<Quests>(selectedQuest.ToList());
            ObservableCollection<TreeViewItemProjectViewModel> questsModel = new ObservableCollection<TreeViewItemProjectViewModel>();

            foreach (var item in Quests)
            {
                questsModel.Add(new TreeViewItemProjectViewModel
                {
                    Title = item.Description,
                    TreeViewItemViewModels = new ObservableCollection<TreeViewItemProjectViewModel>
                    {
                        new TreeViewItemProjectViewModel
                        {
                            Title = _db.Executors.FirstOrDefault(x => x.Id == item.IdExecutor).FullName
                        }
                    }
                });
            }

            ObservableCollection<TreeViewItemProjectViewModel> coll = new ObservableCollection<TreeViewItemProjectViewModel>
            {
                new TreeViewItemProjectViewModel
                {
                    Title = customerName
                },
                new TreeViewItemProjectViewModel
                {
                    Title = responsibleExecutorName
                },
                new TreeViewItemProjectViewModel
                {
                    Title = "Quests",
                    TreeViewItemViewModels = questsModel
                }
            };

            TreeViewItemProjectViewModel ret = new TreeViewItemProjectViewModel();
            ret.Title = project.Name;
            ret.TreeViewItemViewModels = coll;
            return ret;
        }

        private TreeViewItemCustomerViewModel GetCustomer(Customers customer)
        {
            var selectedProjects = from project in _db.Projects
                                   where project.IdCustomer == customer.Id
                                   select project;
            ObservableCollection<Projects> Projects = new ObservableCollection<Projects>(selectedProjects.ToList());
            ObservableCollection<TreeViewItemCustomerViewModel> customerModel = new ObservableCollection<TreeViewItemCustomerViewModel>();

            foreach (var item in Projects)
            {
                customerModel.Add(new TreeViewItemCustomerViewModel
                {
                    Title = item.Name
                });
            }

            TreeViewItemCustomerViewModel cust = new TreeViewItemCustomerViewModel();
            cust.Title = customer.FullName;
            cust.TreeViewItemViewModels = customerModel;
            return cust;
        }

        private TreeViewItemExecutorViewModel GetExecutor(Executors executor)
        {
            var selectedQuests = from quest in _db.Quests
                                 where quest.IdExecutor == executor.Id
                                 select quest;
            ObservableCollection<Quests> Quests = new ObservableCollection<Quests>(selectedQuests.ToList());
            ObservableCollection<TreeViewItemExecutorViewModel> executorModel = new ObservableCollection<TreeViewItemExecutorViewModel>();

            foreach (var item in Quests)
            {
                executorModel.Add(new TreeViewItemExecutorViewModel
                {
                    Title = item.Description
                });
            }

            TreeViewItemExecutorViewModel exec = new TreeViewItemExecutorViewModel();
            exec.Title = executor.FullName;
            exec.TreeViewItemViewModels = executorModel;
            return exec;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
