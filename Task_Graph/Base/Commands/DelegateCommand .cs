using System;

namespace Task_Graph.Base.Commands
{
    public class DelegateCommand : IDelegateCommand
    {


        Action<object> execute;
        Func<object, bool> canExecute;


        public event EventHandler? CanExecuteChanged;


        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = AlwaysCanExecute;
        }

        public bool CanExecute(object parameter)
        {

            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);

        }
        public void RaiseCanExecuteChange()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
        public bool AlwaysCanExecute(object parameter)
        {
            return true;
        }
    }
}
