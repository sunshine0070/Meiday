using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Meiday.ViewModel

{
    internal class RelayCommand<T> : ICommand
    {
        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;
        private Action<object, Uri> checkExecuted;
        private Func<object, bool> canExecuted;

        public RelayCommand(Action <T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;

        }

        public RelayCommand(Action<object, Uri> checkExecuted, Func<object, bool> canExecuted)
        {
            this.checkExecuted = checkExecuted;
            this.canExecuted = canExecuted;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged 
        { 
            add { CommandManager.RequerySuggested += value; } 
            remove { CommandManager.RequerySuggested -= value; } 
        }


        public void Execute(object parameter)
        {
            
            _execute((T)parameter);
        }

    }
}
