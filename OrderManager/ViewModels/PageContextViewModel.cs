using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ViewModels
{
    class PageContextViewModel : INotifyPropertyChanged
    {
        private Uri _pageUri;   
        public Uri PageUri
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
