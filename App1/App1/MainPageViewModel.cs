using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.MetaProgramming;
using Xamarin.MetaProgramming.Commands;

namespace App1
{
    [ViewModel(typeof(MainPageViewModelConfiguration))]
    public class MainPageViewModel
    {
        [DefaultValue("xx")]
        public string Name { get; set; } 
        public string WelcomeMsg => $"Hello {Name}";
        public IMPCommand Command { get; }
        public CommandAsync<object> AsyncCommand { get; }

        public MainPageViewModel()
        {
            Command = new RelayCommand(() => { }, (obj) => Name.Length > 3);
            AsyncCommand=new CommandAsync<object>((obj)=>Task.Delay(10000));
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
