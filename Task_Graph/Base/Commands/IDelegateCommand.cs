using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Task_Graph.Base.Commands
{
    public interface IDelegateCommand : ICommand
    {
        public void RaiseCanExecuteChange();

    }
}
