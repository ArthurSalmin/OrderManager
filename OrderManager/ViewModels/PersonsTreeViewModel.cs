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

namespace OrderManager.ViewModels
{
    class PersonsTreeViewModel : INotifyPropertyChanged
    {
        private ManagerContext _db;
        
        private RelayCommand _addCustomer;
        private RelayCommand _deleteCustomer;
        private RelayCommand _addExecutor;
        private RelayCommand _deleteExecutor;

        private TreeViewItemExecutorViewModel _treeViewItemExecutorViewModel;
        private TreeViewItemCustomerViewModel _treeViewItemCustomerViewModel;

        public ObservableCollection<TreeViewItemCustomerViewModel> CustomerModel { get; set; } =
            new ObservableCollection<TreeViewItemCustomerViewModel>();

        public ObservableCollection<TreeViewItemExecutorViewModel> ExecutorModel { get; set; } =
            new ObservableCollection<TreeViewItemExecutorViewModel>();

        public PersonsTreeViewModel()
        {
            _db = new ManagerContext();
            _treeViewItemCustomerViewModel = new TreeViewItemCustomerViewModel();
            _treeViewItemExecutorViewModel = new TreeViewItemExecutorViewModel();
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

        private TreeViewItemCustomerViewModel GetCustomer(Customers customers)
        {



            TreeViewItemCustomerViewModel cust = new TreeViewItemCustomerViewModel();

            return cust;
        }

        //private TreeViewItemExecutorViewModel GetExecutor(Executors executors)
        //{

        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
