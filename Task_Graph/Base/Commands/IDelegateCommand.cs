using System.Windows.Input;

namespace Task_Graph.Base.Commands
{
    public interface IDelegateCommand : ICommand
    {
        public void RaiseCanExecuteChange();

    }
}
