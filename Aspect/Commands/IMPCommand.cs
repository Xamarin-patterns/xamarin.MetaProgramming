using System;
using System.Windows.Input;
using Xamarin.MetaProgramming.Commands.Contracts;

namespace Xamarin.MetaProgramming.Commands
{
    public interface IMPCommand : ICommand
    {
        void AddContract(params BaseContract[] contracts);
        void ChangeCanExecute();
    }

}