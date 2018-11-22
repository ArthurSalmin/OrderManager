using OrderManager.Commands;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ViewModels.EntitiesViewModels
{
    class QuestViewModel : PageContextViewModel, INotifyPropertyChanged
    {
        private ManagerContext _db;

        private string _description;
        private string _timeOfImplementation;
        private string _executor;
        private string _status;

        private RelayCommand _getQuest;
        public override Uri PageUri
        {
            get
            {
                return _pageUri;
            }
            set
            {
                _pageUri = value;
                OnPropertyChanged("PageUri");
            }
        }

        public QuestViewModel()
        {
            _db = new ManagerContext();
        }

        public RelayCommand GetQuest
        {
            get
            {
                return _getQuest ??
                    (_getQuest = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        Quests quest = selectedItem as Quests;
                        _description = quest.Description;
                        _timeOfImplementation = quest.TimeOfImplementation;
                        _executor = _db.Executors.FirstOrDefault(x => x.Id == quest.Id).FullName;
                        _status = quest.Status;
                    }));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string TimeOfImplementation
        {
            get { return _timeOfImplementation; }
            set
            {
                _timeOfImplementation = value;
                OnPropertyChanged("TimeOfImplamentation");
            }
        }

        public string Executor
        {
            get { return _executor; }
            set
            {
                _executor = value;
                OnPropertyChanged("Executor");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
