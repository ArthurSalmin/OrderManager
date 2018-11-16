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

namespace OrderManager.ViewModels
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private ManagerContext _db;
        private ProjectBusiness _pb;
        private Projects _selectedProject;
        private ObservableCollection<Projects> _projectsCollection;
        private ObservableCollection<TreeViewItemViewModel> _treeViewItems;

        public ObservableCollection<Projects> ProjectsCollection
        {
            get { return _projectsCollection; }
            set
            {
                _projectsCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TreeViewItemViewModel> ProjectsModel { get; set; } = new ObservableCollection<TreeViewItemViewModel>();

        //public List<string> Projects
        //{
        //    get { return ProjectsCollection.Select(x => x.Name).ToList(); }
        //}

        public Projects SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CurrentProject
        {
            get
            {
                return new RelayCommand(GetCurrentProject);
            }
        }

        private void GetCurrentProject()
        {
            _pb.GetProject(SelectedProject);
        }

        public ApplicationViewModel()
        {
            _db = new ManagerContext();
            var projects = _db.Projects.Select(GetProject).ToList();
            foreach (var item in projects)
            {
                ProjectsModel.Add(item);
            }

        }

        private TreeViewItemViewModel GetProject(Projects project)
        {
            string CustomerName = _db.Customers.FirstOrDefault(y => y.IdProject == project.Id).FullName;
            var selectedQuest = from quest in _db.Quests
                             where quest.IdProject == project.Id
                             select quest;
            ObservableCollection<Quests> Quests = new ObservableCollection<Quests>(selectedQuest.ToList());
            ObservableCollection<TreeViewItemViewModel> questsModel = new ObservableCollection<TreeViewItemViewModel>();

            foreach (var item in Quests)
            {
                questsModel.Add(new TreeViewItemViewModel
                {
                    Title = item.Description,
                    TreeViewItemViewModels = new ObservableCollection<TreeViewItemViewModel>
                    {
                        new TreeViewItemViewModel
                        {
                            Title = _db.Executors.FirstOrDefault(x => x.Id == item.IdExecutor).FullName
                        }
                    }
                });
            }

            ObservableCollection<TreeViewItemViewModel> coll = new ObservableCollection<TreeViewItemViewModel>
            {
                new TreeViewItemViewModel
                {
                    Title = CustomerName
                },
                new TreeViewItemViewModel
                {
                    Title = "Quests",
                    TreeViewItemViewModels = questsModel
                }
            };

            TreeViewItemViewModel ret = new TreeViewItemViewModel();
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
