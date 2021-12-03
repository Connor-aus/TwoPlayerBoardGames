using System;
using System.IO;


namespace IFN563___BoardGame
{
    abstract class Game
    {
        public abstract string GameName { get; }
        public abstract int BoardSize { get; }
        public abstract int NumberPlayers { get; }
        public abstract char[] Pieces { get; }  // player who goes first is assignned char from index [0] 
        public abstract char Blank { get; }  // set to any position void of a player piece
        public abstract string HelpFile { get; } // help file location


        protected Board gameBoard;

        public void CreateEquipment()
        {
            gameBoard = Board.GameBoardSet(BoardSize, Blank); // singleton instance of game board
            Piece[] piece = new Piece[NumberPlayers]; // array of objects for the peices
            for (int i = 0; i < NumberPlayers; ++i)
            {
                piece[i] = new Piece(Pieces[i]);
            }
        }


        public abstract bool CheckValidMoves();
        


        public abstract bool ValidMove(int row, int col, char symbol);
        


        public abstract bool CheckWin(char s1);



        public Game()
        {
        }
    }
}
