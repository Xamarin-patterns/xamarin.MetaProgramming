using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.MetaProgramming.Commands.Contracts;

namespace Xamarin.MetaProgramming.Commands
{
    public class CommandAsync<T> : IMPCommand
    {
        
        private readonly Func<T,Task> _executeTask;
        private readonly Predicate<T> _canExecute;
        private bool _looked;
        private List<BaseContract> _contracts;

        public CommandAsync(Func<T, Task> executeTask) : this(executeTask, o => true)
        {
        }

        public CommandAsync(Func<T, Task> executeTask, Predicate<T> canExecute)
        {
            _contracts=new List<BaseContract>();
            _executeTask = executeTask;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var contractNotSatisfied = _contracts.Any(contract => !contract.IsSatisfied(parameter));

            return !contractNotSatisfied && !_looked && _canExecute.Invoke((T) parameter);
        }

        public async void Execute(object parameter)
        {
            
            try
            {
                _looked = true;
                CanExecuteChanged?.Invoke(this, new CommandExecuteChangedArgs("Command Looked for async execution"));
                await _executeTask.Invoke((T) parameter);

            }
            finally
            {
                _looked = false;
                CanExecuteChanged?.Invoke(this, new CommandExecuteChangedArgs("Command Unlooked for async execution terminated"));
            }

        }

        public event EventHandler CanExecuteChanged;
        public void AddContract(params BaseContract[] contracts)
        {
            _contracts.AddRange(contracts);
            contracts.ForEach(contract => contract.SatisfactionChanged += (s, e) =>
            {
                ChangeCanExecute();
            });
        }
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        }
    }

    public class CommandAsync : IMPCommand
    {
       private readonly Task _executeTask;
       private readonly Predicate<object> _canExecute;
       private bool _looked;
       private List<BaseContract> _contracts;

       public CommandAsync(Task executeTask) :this(executeTask,o =>true )
       {
       }

        public CommandAsync(Task executeTask, Predicate<object> canExecute)
       {
           _contracts=new List<BaseContract>();
            _executeTask = executeTask;
           _canExecute = canExecute;
       }

       public bool CanExecute(object parameter)
       {
           var contractNotSatisfied = _contracts.Any(contract => !contract.IsSatisfied(parameter));

            return !contractNotSatisfied&&!_looked && _canExecute.Invoke(parameter);
       }

        public async void Execute(object parameter)
        {
            try
            {
                _looked = true;
                CanExecuteChanged?.Invoke(this,new CommandExecuteChangedArgs("Command Looked for async execution"));
                await _executeTask;
              
            }
            finally
            {
                _looked = false;
                CanExecuteChanged?.Invoke(this, new CommandExecuteChangedArgs("Command Unlooked for async execution terminated"));
            }

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
}
