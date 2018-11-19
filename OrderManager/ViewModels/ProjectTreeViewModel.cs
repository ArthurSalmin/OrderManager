using OrderManager.Commands;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrderManager.ViewModels
{
    class ProjectTreeViewModel : INotifyPropertyChanged
    {
        private ManagerContext _db;

        private RelayCommand _addProject;
        private RelayCommand _deleteProject;
        private RelayCommand _addQuest;
        private RelayCommand _deleteQuest;
        
        private TreeViewItemProjectViewModel _treeViewItemProjectViewModel;
        private TreeViewItemQuestViewModel _treeViewItemQuestViewModel;

        public ProjectTreeViewModel()
        {
            _db = new ManagerContext();
            var projects = _db.Projects.Select(GetProject).ToList();
            foreach (var item in projects)
            {
                ProjectsModel.Add(item);
            }

            _treeViewItemProjectViewModel = new TreeViewItemProjectViewModel();
            _treeViewItemQuestViewModel = new TreeViewItemQuestViewModel();
        }

        public ObservableCollection<TreeViewItemProjectViewModel> ProjectsModel { get; set; } =
            new ObservableCollection<TreeViewItemProjectViewModel>();
        
        public RelayCommand AddProject
        {
            get
            {
                return _addProject ??
                    (_addProject = new RelayCommand((o) =>
                    {
                        Projects newProject = new Projects();
                        _db.Projects.Add(newProject);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteProject
        {
            get
            {
                return _deleteProject ??
                    (_deleteProject = new RelayCommand((selectedItem) => 
                    {
                        if (selectedItem == null) return;
                        Projects deletedProject = selectedItem as Projects;
                        _db.Projects.Remove(deletedProject);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand AddQuest
        {
            get
            {
                return _addQuest ??
                    (_addQuest = new RelayCommand((o) =>
                    {
                        Quests newQuest = new Quests();
                        _db.Quests.Add(newQuest);
                        _db.SaveChanges();
                    }));
            }
        }

        public RelayCommand DeleteQuest
        {
            get
            {
                return _deleteQuest ??
                    (_deleteQuest = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Quests deletedQuest = selectedItem as Quests;
                        _db.Quests.Remove(deletedQuest);
                        _db.SaveChanges();
                    }));
            }
        }

        private TreeViewItemProjectViewModel GetProject(Projects project)
        {
            string responsibleExecutorName = _db.Executors.FirstOrDefault(y => y.Id == project.IdResponsibleExecutor).FullName;
            string customerName = _db.Customers.FirstOrDefault(y => y.IdProject == project.Id).FullName;
            var selectedQuest = from quest in _db.Quests
                                where quest.IdProject == project.Id
                                select quest;
            ObservableCollection<Quests> Quests = new ObservableCollection<Quests>(selectedQuest.ToList());
            ObservableCollection<TreeViewItemProjectViewModel> questsModel = new ObservableCollection<TreeViewItemProjectViewModel>();

            foreach (var item in Quests)
            {
                questsModel.Add(new TreeViewItemProjectViewModel
                {
                    Title = item.Description,
                    TreeViewItemViewModels = new ObservableCollection<TreeViewItemProjectViewModel>
                    {
                        new TreeViewItemProjectViewModel
                        {
                            Title = _db.Executors.FirstOrDefault(x => x.Id == item.IdExecutor).FullName
                        }
                    }
                });
            }

            ObservableCollection<TreeViewItemProjectViewModel> coll = new ObservableCollection<TreeViewItemProjectViewModel>
            {
                new TreeViewItemProjectViewModel
                {
                    Title = customerName
                },
                new TreeViewItemProjectViewModel
                {
                    Title = responsibleExecutorName
                },
                new TreeViewItemProjectViewModel
                {
                    Title = "Quests",
                    TreeViewItemViewModels = questsModel
                }
            };

            TreeViewItemProjectViewModel ret = new TreeViewItemProjectViewModel();
            ret.Title = project.Name;
            ret.TreeViewItemViewModels = coll;
            return ret;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
