using System;
using System.Threading.Tasks;
using Xamarin.MetaProgramming.Commands.Contracts;

namespace Xamarin.MetaProgramming.Commands
{
    public class Command<T> : IMPCommand
    {
        private readonly IMPCommand _impCommandImplementation;

        public Command(Action<T> execute, Predicate<T> canExecute)
        {
            _impCommandImplementation = new RelayCommand<T>(execute, canExecute);
        }
        public Command(Action<T> execute)
        {
            _impCommandImplementation = new RelayCommand<T>(execute);
        }
        public Command(Func<T,Task> execute, Predicate<T> canExecute)
        {
            _impCommandImplementation = new CommandAsync<T>(execute, canExecute);
        }
        public Command(Func<T, Task> execute)
        {
            _impCommandImplementation = new CommandAsync<T>(execute);
        }
        public bool CanExecute(object parameter)
        {
            return _impCommandImplementation.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _impCommandImplementation.Execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => _impCommandImplementation.CanExecuteChanged += value;
            remove => _impCommandImplementation.CanExecuteChanged -= value;
        }

        public void AddContract(params BaseContract[] contracts)
        {
            _impCommandImplementation.AddContract(contracts);
        }

        public void ChangeCanExecute()
        {
            _impCommandImplementation.ChangeCanExecute();
        }
    }

    public class Command :IMPCommand
    {
        private readonly IMPCommand _impCommandImplementation;

        public Command(Action execute, Predicate<object> canExecute)
        {
            _impCommandImplementation = new RelayCommand(execute,canExecute);
        }
        public Command(Task execute, Predicate<object> canExecute)
        {
            _impCommandImplementation = new CommandAsync(execute, canExecute);
        }
        public bool CanExecute(object parameter)
        {
            return _impCommandImplementation.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _impCommandImplementation.Execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => _impCommandImplementation.CanExecuteChanged += value;
            remove => _impCommandImplementation.CanExecuteChanged -= value;
        }

        public void AddContract(params BaseContract[] contracts)
        {
            _impCommandImplementation.AddContract(contracts);
        }

        public void ChangeCanExecute()
        {
            _impCommandImplementation.ChangeCanExecute();
        }
    }
}