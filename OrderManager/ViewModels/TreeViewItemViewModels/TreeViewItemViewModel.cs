using OrderManager.ViewModels.EntitiesViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace OrderManager.ViewModels.TreeViewItemViewModels
{
    abstract class TreeViewItemViewModel<T> where T : class
    {
        protected ObservableCollection<T> _treeViewItemViewModels;
        protected string _title;
        protected Page _framePage;

        public abstract PageContextViewModel Context { get; set; } 
        public abstract Page FramePage { get; }
        public abstract ObservableCollection<T> TreeViewItemViewModels { get; set; }
        public abstract string Title { get; set; }
    }
}
