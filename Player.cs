using System;
using IFN563___BoardGame.Command_Pattern;


namespace IFN563___BoardGame
{
    abstract class Player
    {
        public abstract string Name { get; set; }
        public abstract char Symbol { get; set; }

        public abstract void MakeMove(Game instGame, MoveHistory moveHistory);  // making a move (human or AI)
    }
}
