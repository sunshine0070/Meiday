using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Meiday.ViewModel
{
    internal class DelegateCommand : ICommand
    {
        private readonly Func<bool> canExecute; private readonly Action execute;

        public DelegateCommand(Action execute) : this(execute, null) { }
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        //요약 
        //can execute, event, handler

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object o)
        {
            if(this.canExecute != null) //null이 아니면 true 사용가능 
            {
                return true;
            }
            return this.canExecute();
        }

        public void Execute(object o)
        {
            this.execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if(this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

    }
}
