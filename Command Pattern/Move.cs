using System;

namespace IFN563___BoardGame
{
    public class Move : ICommand
    {
        private Board board;
        public char Symbol { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Move()
        {
            // get Board singleton
            this.board = Board.GameBoard;
        }

        public void Execute() // execute move
        {
            board.BoardLayout[Row, Col] = Symbol;
        } 


        public void Undo() // undo move
        {
            board.BoardLayout[Row, Col] = board.Blank;
        }
    }
}
