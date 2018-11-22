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
    class TreeViewItemQuestViewModel : TreeViewItemViewModel<TreeViewItemQuestViewModel>, INotifyPropertyChanged
    {
        private RelayCommand _editQuest;
        private ManagerContext _db;
        private string _pageQuestPath = "/Pages/QuestInformationPage.xaml";
        public TreeViewItemQuestViewModel()
        {
            _db = new ManagerContext();
            Context = new QuestViewModel();
            Context.PageUri = new Uri(_pageQuestPath, UriKind.RelativeOrAbsolute);
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
        public override ObservableCollection<TreeViewItemQuestViewModel> TreeViewItemViewModels
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

        public RelayCommand EditQuest
        {
            get
            {
                return _editQuest ?? 
                    (_editQuest = new RelayCommand((selectedItem) => 
                    {
                        if (selectedItem == null) return;
                        Quests editedQuest = selectedItem as Quests;
                        editedQuest = _db.Quests.Find(editedQuest.Id);
                        if (editedQuest != null)
                        {
                            _db.Entry(editedQuest).State = EntityState.Modified;
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
