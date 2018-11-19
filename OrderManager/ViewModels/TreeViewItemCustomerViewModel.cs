using OrderManager.Commands;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OrderManager.ViewModels
{
    class TreeViewItemCustomerViewModel : TreeViewItemViewModel<TreeViewItemCustomerViewModel>, INotifyPropertyChanged
    {
        private RelayCommand _editCustomer;
        private ManagerContext _db;
        private string _pageCustomerPath = "/Pages/CustomerInformationPage.xaml";
        public TreeViewItemCustomerViewModel()
        {
            _db = new ManagerContext();
            Context.PageUri = new Uri(_pageCustomerPath, UriKind.RelativeOrAbsolute);
        }

        public override string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public override ObservableCollection<TreeViewItemCustomerViewModel> TreeViewItemViewModels
        {
            get
            {
                return _treeViewItemViewModels;
            }
            set
            {
                _treeViewItemViewModels = value;
                OnPropertyChanged("TreeViewItemViewModels");
            }
        }
        public override PageContextViewModel Context { get; set; } = new PageContextViewModel();
        
        public override Page FramePage
        {
            get
            {
                if (_framePage == null && Context != null)
                {
                    _framePage = Application.LoadComponent(Context.PageUri) as Page;
                    _framePage.DataContext = Context;
                }
                return _framePage;
            }
        }
        
        public RelayCommand EditCustomer
        {
            get
            {
                return _editCustomer ??
                    (new RelayCommand((selectedItem)=> 
                    {
                        if (selectedItem == null) return;
                        Customers editedCustomer = selectedItem as Customers;
                        editedCustomer = _db.Customers.Find(editedCustomer.Id);
                        if (editedCustomer != null)
                        {
                            _db.Entry(editedCustomer).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
