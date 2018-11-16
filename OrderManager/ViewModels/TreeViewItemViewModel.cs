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
    class TreeViewItemViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TreeViewItemViewModel> _treeViewItemViewModels;
        private string _title;
        private string _customer;

        public TreeViewItemViewModel()
        {

        }

        virtual public ObservableCollection<TreeViewItemViewModel> TreeViewItemViewModels
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

        virtual public string Title
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

        public string Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
