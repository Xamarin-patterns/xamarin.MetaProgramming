using System.ComponentModel;

namespace Xamarin.MetaProgramming
{
    public interface INotifyPropertyChangedImplementor : INotifyPropertyChanged
    {
        void RaisePropertyChanged(string propertyName);
    }
}