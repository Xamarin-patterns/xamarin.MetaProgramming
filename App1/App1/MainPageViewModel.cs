using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.MetaProgramming;

namespace App1
{
    [ViewModel(typeof(MainPageViewModelConfiguration))]
    public class MainPageViewModel
    {
        [DefaultValue("xx")]
        public string Name { get; set; } 
        public string WelcomeMsg => $"Hello {Name}";
        public ICommand Command { get; }

        public MainPageViewModel()
        {
            Command = new Command(() => { }, () => Name.Length > 3);
        }

    }

    [Serializable]
    public class MainPageViewModelConfiguration : ConfigurationProvider<MainPageViewModel>
   {
       public MainPageViewModelConfiguration()
       {
           this.NotifyPropertyChange(model =>model.Name );
           this.NotifyPropertyChange(model=>model.WelcomeMsg).DependOn(model =>model.Name );
           this.NotifyPropertyChange(model => model.Command).DependOn(model => model.Name);
       }
    }
}
