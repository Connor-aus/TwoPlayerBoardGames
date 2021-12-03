using System;
using static System.Console;
using System.IO;


namespace IFN563___BoardGame
{
    class GomokuGame : Game
    {
        public override string GameName { get; } = "Gomoku";
        public override int BoardSize { get; } = 12; // smaller board used for an easier interface
        public override int NumberPlayers { get; } = 2;
        public override char[] Pieces { get; } = { 'B', 'W' };  // player who goes first is assingned char in position [0]
        public override char Blank { get; } = ' ';  // set to any position void of a player piece

        public override string HelpFile { get; } = Directory.GetCurrentDirectory() + "/documents/GomokuHelp.txt";


        public override bool CheckValidMoves()
        {
            // check for Blank symbols within each position (row, col)
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                        if (gameBoard.BoardLayout[y, x] == Blank)
                        {
                            return false;
                        }
                }
            }

            // no valid moves available - game end
            return true;
        }




        public override bool ValidMove(int row, int col, char symbol)
        {
            // check if referenced position is Blank
            if (row <= BoardSize - 1 && row >= 0 && col <= BoardSize - 1 && col >= 0 && gameBoard.BoardLayout[row, col] == Blank)
            {
                return true;
            }
            else
            {
                WriteLine("     Invalid selection. Choose again >>");
                return false;
            }
        }





        public override bool CheckWin(char s)
        {
            // check horizontal win condition
            for (int x = 0; x < BoardSize - 4; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    if (gameBoard.BoardLayout[x, y] == s && gameBoard.BoardLayout[x + 1, y] == s && gameBoard.BoardLayout[x + 2, y] == s && gameBoard.BoardLayout[x + 3, y] == s && gameBoard.BoardLayout[x + 4, y] == s)
                    {
                        return true;
                    }
                }
            }

            // check vertical win condition
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize - 4; y++)
                {
                    if (gameBoard.BoardLayout[x, y] == s && gameBoard.BoardLayout[x, y + 1] == s && gameBoard.BoardLayout[x, y + 2] == s && gameBoard.BoardLayout[x, y + 3] == s && gameBoard.BoardLayout[x, y + 4] == s)
                    {
                        return true;
                    }
                }
            }

            // check positive diagonal win condition
            for (int x = 0; x < BoardSize - 4; x++)
            {
                for (int y = 0; y < BoardSize - 4; y++)
                {
                    if (gameBoard.BoardLayout[x, y] == s && gameBoard.BoardLayout[x + 1, y + 1] == s && gameBoard.BoardLayout[x + 2, y + 2] == s && gameBoard.BoardLayout[x + 3, y + 3] == s && gameBoard.BoardLayout[x + 4, y + 4] == s)
                    {
                        return true;
                    }
                }

            }

            // check negative diagonal win condition
            for (int x = 4; x < BoardSize; x++)
            {
                for (int y = 4; y < BoardSize - 4; y++)
                {
                    if (gameBoard.BoardLayout[x, y] == s && gameBoard.BoardLayout[x - 1, y - 1] == s && gameBoard.BoardLayout[x - 2, y - 2] == s && gameBoard.BoardLayout[x - 3, y - 3] == s && gameBoard.BoardLayout[x - 4, y - 4] == s)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GomokuGame()
        {
        }
    }
}
