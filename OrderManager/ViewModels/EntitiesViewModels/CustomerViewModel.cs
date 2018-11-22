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
    class CustomerViewModel : PageContextViewModel, INotifyPropertyChanged
    {
        private ManagerContext _db;

        private string _imagePath;
        private string _name;
        private string _adressInformation;

        private RelayCommand _getCustomer;

        public CustomerViewModel()
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

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
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

        public string AdressInformation
        {
            get { return _adressInformation; }
            set
            {
                _adressInformation = value;
                OnPropertyChanged("AdressInformation");
            }
        }

        public RelayCommand GetCustomer
        {
            get
            {
                return _getCustomer ??
                    (_getCustomer = new RelayCommand((SelectedItem) =>
                    {
                        if (SelectedItem == null) return;
                        string NameCustomer = SelectedItem as string;
                        //Customers customer = selectedItem as Customers;
                        Customers customer = _db.Customers.FirstOrDefault(x => x.FullName == NameCustomer);

                        _imagePath = customer.Image;
                        _name = customer.FullName;
                        _adressInformation = customer.AdressInformation;
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
