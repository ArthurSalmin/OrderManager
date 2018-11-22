using OrderManager.Commands;
using OrderManager.Models;
using OrderManager.ViewModels.EntitiesViewModels;
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

namespace OrderManager.ViewModels.TreeViewItemViewModels
{
    class TreeViewItemProjectViewModel : TreeViewItemViewModel<TreeViewItemProjectViewModel>, INotifyPropertyChanged
    {
        private RelayCommand _editProject;
        private ManagerContext _db;

        private string _pageProjectPath = "/Pages/ProjectInformationPage.xaml";
        public TreeViewItemProjectViewModel()
        {
            _db = new ManagerContext();
            Context = new ProjectViewModel();
            Context.PageUri = new Uri(_pageProjectPath, UriKind.RelativeOrAbsolute);
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
        public override ObservableCollection<TreeViewItemProjectViewModel> TreeViewItemViewModels
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
        
        public RelayCommand EditProject
        {
            get
            {
                return _editProject ??
                    (_editProject = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Projects editedProject = selectedItem as Projects;
                        editedProject = _db.Projects.Find(editedProject.Id);
                        if (editedProject != null)
                        {
                            _db.Entry(editedProject).State = EntityState.Modified;
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
