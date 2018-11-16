using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ViewModels
{
    class TreeViewItemCustomerViewModel : TreeViewItemViewModel
    {
        public override string Title { get; set ; }
        public override ObservableCollection<TreeViewItemViewModel> TreeViewItemViewModels { get; set; }
        public TreeViewItemCustomerViewModel()
        {

        }
        public TreeViewItemCustomerViewModel(string title, ObservableCollection<TreeViewItemViewModel> models)
        {
            Title = title;
            TreeViewItemViewModels = models;
        }

    }
}
