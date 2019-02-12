using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.Models
{
    public class AsyncDelegateCommand : ICommand
    {
        private readonly Action<object> _actionWithArgu;
        private readonly Action _action;
        private readonly Predicate<object> _canExecute;
        private readonly Func<object, Task> _asyncExecute;

        private readonly bool IsAsync = false;
        private readonly bool IsHaveArgu = true;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }



        public AsyncDelegateCommand(Action action)
        {
            _action = action;
            IsAsync = false;
            IsHaveArgu = false;
        }
        public AsyncDelegateCommand(Action<object> action)
        {
            _actionWithArgu = action;
            IsAsync = false;
            IsHaveArgu = true;
        }

        public AsyncDelegateCommand(Func<object, Task> execute) : this(execute, null)
        {
            IsAsync = true;
        }
        public AsyncDelegateCommand(Func<object, Task> asyncExecute, Predicate<object> canExecute)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
            IsAsync = true;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }
        public void Execute() => Execute(null);

        public async void Execute(object parameter)
        {
            if (IsAsync)
            {
                await ExecuteAsync(parameter);
            }
            else
            {
                if (IsHaveArgu) _actionWithArgu(parameter);
                else _action();


            }

        }

        public virtual async Task ExecuteAsync(object parameter)
        {
            await _asyncExecute(parameter);
        }
    }
}
