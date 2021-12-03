using System;
using static System.Console;
using IFN563___BoardGame.Command_Pattern;


namespace IFN563___BoardGame
{
    class HumanPlayer : Player
    {
        public override string Name { get; set; }
        public override char Symbol { get; set; }

        public override void MakeMove(Game gameInst, MoveHistory moveHistory)
        {
            int row = 0;
            int col = 0;
            bool validMove = false;


            while (!validMove)
            {
                bool validInt = false;

                // request row selection
                WriteLine("select a row >> ");       
                while (!validInt)
                {
                    validInt = int.TryParse(ReadLine(), out row);

                    // compensate for +1 to row numbering
                    row = row - 1;

                    // checking validity of entry against board size
                    if (!validInt || row >= gameInst.BoardSize || row < 0)
                    {
                        WriteLine("     Invalid Entry. Please enter a valid row number >> ");
                        validInt = false;
                    }
                }

                validInt = false;

                // request column selection
                WriteLine("select a column >> ");
                while (!validInt)
                {
                    validInt = int.TryParse(ReadLine(), out col);

                    // compensate for +1 to column numbering
                    col = col - 1;

                    // checking validity of entry against board size
                    if (!validInt || col >= gameInst.BoardSize || row < 0)
                    {
                        WriteLine("     Invalid Entry. Please enter a valid coloumn number >> ");
                        validInt = false;
                    }
                }

                // check move validitiy
                validMove = gameInst.ValidMove(row, col, Symbol);

                // if valid, create move and pass to move history
                if (validMove)
                {
                    Move move = new Move();
                    move.Symbol = Symbol;
                    move.Row = row;
                    move.Col = col;
                    moveHistory.AddCommand(move);
                }
            }
        }

        public HumanPlayer(string name)
        {
            this.Name = name;
        }
    }
}
