using OrderManager.Commands;
using OrderManager.Models;
using OrderManager.ViewModels.EntitiesViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace OrderManager.ViewModels.TreeViewItemViewModels
{
    class TreeViewItemExecutorViewModel : TreeViewItemViewModel<TreeViewItemExecutorViewModel>, INotifyPropertyChanged
    {
        private RelayCommand _editExecutor;
        private ManagerContext _db;
        private string _pageExecutorPath = "/PagesExecutorInformationPage.xaml";
        public TreeViewItemExecutorViewModel()
        {
            _db = new ManagerContext();
            Context = new ExecutorViewModel();
            Context.PageUri = new Uri(_pageExecutorPath, UriKind.RelativeOrAbsolute);
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
        public override ObservableCollection<TreeViewItemExecutorViewModel> TreeViewItemViewModels
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
        public override PageContextViewModel Context { get; set; }

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

        public RelayCommand EditExecutor
        {
            get
            {
                return _editExecutor ??
                    (_editExecutor = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null) return;
                    Executors editedExecutor = selectedItem as Executors;
                    editedExecutor = _db.Executors.Find(editedExecutor.Id);
                    if (editedExecutor != null)
                    {
                        _db.Entry(editedExecutor).State = EntityState.Modified;
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
