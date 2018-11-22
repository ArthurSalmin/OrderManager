using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.ViewModels.EntitiesViewModels
{
    abstract class PageContextViewModel 
    {
        protected Uri _pageUri;   
        abstract public Uri PageUri { get; set; }
    }
}
