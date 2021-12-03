using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563___BoardGame
{
    public interface ICommand
    {
        public void Execute();

        public void Undo();
    }
}
