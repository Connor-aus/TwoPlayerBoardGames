using System;
using static System.Console;
namespace IFN563___BoardGame
{
    class BoardGameArena
    {     
        static void Main(string[] args)
        {
            GameController controller = new GameController();

            WriteLine("------------Welcome to the board game arena------------\n");
            WriteLine("Would you like to start a new game or load a saved game?");
            WriteLine(" 1. New game");
            WriteLine(" 2. Load game");
            bool validInput = false;

            while (!validInput)
            {
                string input = ReadLine();

                if (input == "2" || input == "2.")
                {
                    // check for valid save file
                    bool saveExist = controller.CheckSaves();
                    if (saveExist)
                    {
                        // initialisation uses save file data rather than user input
                        controller.Load = true;
                    }
                    else
                    {
                        WriteLine(" Sorry, there is no valid save file.");
                        WriteLine(" A new game will begin.");
                    }
                }

                // load game or create new game 
                if (input == "1" || input == "2" || input == "1." || input == "2.")
                {
                    controller.InitializeGame();
                    WriteLine();

                    controller.PlayerCreation();
                    WriteLine();

                    controller.SetPlayer1();

                    validInput = true;

                    if (controller.Load == true)
                    {
                        controller.LoadMoves();
                    }
                }
                else
                {
                    WriteLine("\n Invalid selection. Please make a valid selsction >>");
                }
            }

            Console.Clear();

            controller.Play();

            ReadKey();
        }
    }
}
