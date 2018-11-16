using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ViewModels
{
    class TreeViewItemExecutorViewModel : TreeViewItemViewModel
    {
        public override ObservableCollection<TreeViewItemViewModel> TreeViewItemViewModels { get; set; }
        public override string Title { get; set; }

        public TreeViewItemExecutorViewModel()
        {

        }
        
        private string _customer;
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

        public TreeViewItemExecutorViewModel(string title, ObservableCollection<TreeViewItemViewModel> models )
        {
            Title = title;
            TreeViewItemViewModels = models;
        }
    }
}
