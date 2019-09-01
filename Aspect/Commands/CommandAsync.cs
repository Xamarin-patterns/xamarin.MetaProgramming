using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.MetaProgramming.Commands
{
    public class CommandAsync<T> : IMPCommand
    {
        
        private readonly Func<T,Task> _executeTask;
        private readonly Predicate<object> _canExecute;
        private bool _looked;
        public CommandAsync(Func<T, Task> executeTask) : this(executeTask, o => true)
        {
        }

        public CommandAsync(Func<T, Task> executeTask, Predicate<object> canExecute)
        {
            _executeTask = executeTask;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !_looked && _canExecute.Invoke(parameter);
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
       public CommandAsync(Task executeTask) :this(executeTask,o =>true )
       {
       }

        public CommandAsync(Task executeTask, Predicate<object> canExecute)
       {
           _executeTask = executeTask;
           _canExecute = canExecute;
       }

       public bool CanExecute(object parameter)
       {
           return !_looked && _canExecute.Invoke(parameter);
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
        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
