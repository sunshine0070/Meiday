using System;
using System.Windows.Input;
namespace Meiday
{
    class Append : ICommand 
    { 
        private LoginViewModel c; 
        public event EventHandler CanExecuteChanged; 
        public Append(LoginViewModel c) 
        { 
            this.c = c; 
        } 
        public bool CanExecute(object parameter) 
        { 
            return true;
        } 
        public void Execute(object parameter) 
        { 
            c.InputString += parameter; 
        } 
    }
    class BackSpace : ICommand 
    { 
        private LoginViewModel c; 
        public BackSpace(LoginViewModel c) 
        { 
            this.c = c; 
        } 
        public event EventHandler CanExecuteChanged 
        { 
            add 
            { 
                CommandManager.RequerySuggested += value; 
            } 
            remove 
            { 
                CommandManager.RequerySuggested -= value; 
            } 
        } 
        public bool CanExecute(object parameter) 
        { 
            return c.DisplayText.Length > 0; 
        } 
        public void Execute(object parameter) 
        { 
            int length = c.InputString.Length - 1; 
            if (0 < length) 
            { 
                c.InputString = c.InputString.Substring(0, length); 
            } 
            else 
            { 
                c.InputString = c.DisplayText = string.Empty; 
            } 
        }
    }
}