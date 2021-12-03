using System;
using static System.Console;

namespace IFN563___BoardGame
{
    public class Board
    {
        private int Size { get; }
        public char Blank { get; }  // set to any position void of a player piece
        private int x, y;


        public char[,] BoardLayout {get; set; }

        private static Board gameBoard; // initialize singleton
        public static Board GameBoard // return singleton
        {
            get
            {
                return gameBoard;
            }
            
        }

        public static Board GameBoardSet(int size, char blank) // initial set-up and return singleton
        {
            if (gameBoard == null)
            {
                gameBoard = new Board(size, blank);
            }
            return gameBoard;
        }
        
        public void DisplayBoard()
        {
            Console.Clear();

            // display coloumn numbers
            Write("\n      ");
            for (int i = 1; i < Size + 1; i++)
            {
                if (i < 10)
                {
                    Write(i + "   ");
                }
                else
                {
                    Write(i + "  ");
                }
            }
            WriteLine();

            // display rows
            for (x = 0; x < Size; x++)
            {
                if (x < 9)
                {
                    Write("  " + (x + 1) + " | ");
                }
                else
                {
                    Write(" " + (x + 1) + " | ");
                }
                
                // display columns
                for (y = 0; y < Size; y++)
                {
                    Write(BoardLayout[x, y]);
                    Write(" | ");
                }
                Write("\n    ");

                // display row divider
                for (int j = 0; j < Size; j++)
                {
                    if (j < Size - 1)
                    {
                        Write("----");
                    }
                    else
                    {
                        Write("-----\n");
                    }
                }
            }
            WriteLine();
        }

        
        private Board (int size, char blank)
        {
            this.Size = size;
            this.Blank = blank;
            this.BoardLayout = new char[size, size];

            // initialise board layout
            for (x = 0; x < Size; x++)
            {
                for (y = 0; y < Size; y++)
                    this.BoardLayout[x, y] = Blank;
            }
        }
    }
}
