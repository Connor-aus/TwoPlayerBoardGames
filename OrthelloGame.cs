using System;
using static System.Console;
using System.IO;


namespace IFN563___BoardGame
{
    class OrthelloGame : Game
    {
        public override string GameName { get; } = "Orthello";
        public override int BoardSize { get; } = 8;
        public override int NumberPlayers { get; } = 2;
        public override char[] Pieces { get; } = { 'W', 'B' }; //player who goes first is assingned char in position [0] 
        public override char Blank { get; } = ' ';  // set to any position void of a player piece
        public override string HelpFile { get; }


        public OrthelloGame()
        {
        }

        public override bool CheckValidMoves()
        {
            throw new NotImplementedException();
        }

        public override bool ValidMove(int row, int col, char symbol)
        {
            throw new NotImplementedException();
        }

        public override bool CheckWin(char s1)
        {
            throw new NotImplementedException();
        }
    }
}
