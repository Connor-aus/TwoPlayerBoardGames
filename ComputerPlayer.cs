using System;
using IFN563___BoardGame.Command_Pattern;


namespace IFN563___BoardGame
{
    class ComputerPlayer : Player
    {
        public static string Type { get; } = "Computer";
        public override string Name { get; set; }
        public override char Symbol { get; set; }

        public override void MakeMove(Game gameInst, MoveHistory moveHistory)
        {

            bool validMove = false;

            while (!validMove)
            {
                // instantiate randam number generator
                Random random = new Random();

                // select a random board location within the boundaries of the board size
                int row = random.Next(0, (gameInst.BoardSize - 1));  
                int col = random.Next(0, (gameInst.BoardSize - 1));

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
        public ComputerPlayer()
        {
            this.Name = Type;
        }
    }
}
