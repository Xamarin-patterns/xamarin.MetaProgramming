using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.MetaProgramming.Commands.Contracts;

namespace Xamarin.MetaProgramming.Commands
{
    public class RelayCommand<T> : IMPCommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;
        private List<BaseContract> _contracts;

        public RelayCommand(Action<T> execute):this(execute,obj => true)
        {
            

        }
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
            _contracts = new List<BaseContract>();

        }

        public bool CanExecute(object parameter)
        {
            var contractNotSatisfied = _contracts.Any(contract => !contract.IsSatisfied(parameter));
            return !contractNotSatisfied && _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
        public void AddContract(params BaseContract[] contracts)
        {
            _contracts.AddRange(contracts);
            contracts.ForEach(contract => contract.SatisfactionChanged += (s, e) => ChangeCanExecute());
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand : IMPCommand
    {
        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;
        private List<BaseContract> _contracts;

        public RelayCommand(Action execute, Predicate<object> canExecute)
        {
            _contracts=new List<BaseContract>();
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var contractNotSatisfied = _contracts.Any(contract => !contract.IsSatisfied(parameter));
            return !contractNotSatisfied && _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;
        public void AddContract(params BaseContract[] contracts)
        {
            _contracts.AddRange(contracts);
            contracts.ForEach(contract=>contract.SatisfactionChanged+=(s,e)=>ChangeCanExecute());
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}