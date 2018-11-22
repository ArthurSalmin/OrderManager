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
    class ExecutorViewModel : PageContextViewModel, INotifyPropertyChanged
    {
        private string _name;
        private string _imagePath;
        private string _contacts;

        private RelayCommand _getExecutor;

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

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        public string Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        public RelayCommand GetExecutor
        {
            get
            {
                return _getExecutor ??
                    (_getExecutor = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Executors executor = selectedItem as Executors;
                        _imagePath = executor.Image;
                        _name = executor.FullName;
                        _contacts = executor.Contacts;
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
