using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MApp_SaveMe.Models
{
    public class MainMenuItem : BindableObject
    {
        private string _menuText;
        private Type _viewModelToLoad;

        public string MenuText
        {
            get
            {
                return _menuText;
            }
            set
            {
                _menuText = value;
                OnPropertyChanged();
            }
        }

        public Type ViewModelToLoad
        {
            get
            {
                return _viewModelToLoad;
            }
            set
            {
                _viewModelToLoad = value;
                OnPropertyChanged();
            }
        }
    }
}
