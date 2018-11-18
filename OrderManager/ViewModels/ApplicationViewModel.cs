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

        private TreeViewItemCustomerViewModel _treeViewItemCustomerViewModel;
        private TreeViewItemProjectViewModel _treeViewItemProjectViewModel;
        private TreeViewItemExecutorViewModel _treeViewItemExecutorViewModel;
        private TreeViewItemQuestViewModel _treeViewItemQuestViewModel;

        private string _pageCustomerPath = ".\\Pages\\CustomerInformationPage";
        private string _pageProjectPath = ".\\Pages\\ProjectInformationPage";
        private string _pageExecutorPath = ".\\Pages\\ExecutorInformationPage";
        private string _pageQuestPath = ".\\Pages\\QuestInformationPage";

        public ObservableCollection<Projects> ProjectsCollection
        {
            get { return _projectsCollection; }
            set
            {
                _projectsCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TreeViewItemCustomerViewModel> ProjectsModel { get; set; } =
            new ObservableCollection<TreeViewItemCustomerViewModel>();

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
            _treeViewItemCustomerViewModel.Context.UriPagePAth = _pageCustomerPath;
            //_treeViewItemCustomerViewModel.Context.UriPagePAth = _pageCustomerPath;
            //_treeViewItemCustomerViewModel.Context.UriPagePAth = _pageCustomerPath;
            //_treeViewItemCustomerViewModel.Context.UriPagePAth = _pageCustomerPath;
        }

        private TreeViewItemCustomerViewModel GetProject(Projects project)
        {
            string CustomerName = _db.Customers.FirstOrDefault(y => y.IdProject == project.Id).FullName;
            var selectedQuest = from quest in _db.Quests
                                where quest.IdProject == project.Id
                                select quest;
            ObservableCollection<Quests> Quests = new ObservableCollection<Quests>(selectedQuest.ToList());
            ObservableCollection<TreeViewItemCustomerViewModel> questsModel = new ObservableCollection<TreeViewItemCustomerViewModel>();

            foreach (var item in Quests)
            {
                questsModel.Add(new TreeViewItemCustomerViewModel
                {
                    Title = item.Description,
                    TreeViewItemViewModels = new ObservableCollection<TreeViewItemCustomerViewModel>
                    {
                        new TreeViewItemCustomerViewModel
                        {
                            Title = _db.Executors.FirstOrDefault(x => x.Id == item.IdExecutor).FullName
                        }
                    }
                });
            }

            ObservableCollection<TreeViewItemCustomerViewModel> coll = new ObservableCollection<TreeViewItemCustomerViewModel>
            {
                new TreeViewItemCustomerViewModel
                {
                    Title = CustomerName
                },
                new TreeViewItemCustomerViewModel
                {
                    Title = "Quests",
                    TreeViewItemViewModels = questsModel
                }
            };

            TreeViewItemCustomerViewModel ret = new TreeViewItemCustomerViewModel();
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
