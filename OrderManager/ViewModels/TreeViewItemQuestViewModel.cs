﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OrderManager.ViewModels
{
    class TreeViewItemQuestViewModel : TreeViewItemViewModel<TreeViewItemQuestViewModel>, INotifyPropertyChanged
    {
        private string _pageQuestPath = ".\\Pages\\QuestInformationPage";
        public TreeViewItemQuestViewModel()
        {
            Context.PageUri = new Uri(_pageQuestPath);
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
