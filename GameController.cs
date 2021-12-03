using System;
using static System.Console;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IFN563___BoardGame.Command_Pattern;

namespace IFN563___BoardGame
{
    class GameController
    {
        private int NumHumanPlayers { get; set; }
        private int CurrentPlayerIndex { get; set; }
        public bool Load { get; set; }  = false;
        private static string SaveStatus { get; } = Directory.GetCurrentDirectory() + "/documents/SaveGameStatus.txt";
        public static string SaveMoves { get; } = Directory.GetCurrentDirectory() + "/documents/SaveGameMoves.txt";

        Game gameInst;
        Player[] player;
        Board gameBoard;
        MoveHistory moveHistory = new MoveHistory();

        public GameController()
        {
        }

        public void InitializeGame()
        {
            bool validInput = false;
            int count = 1;              // for displaying input options
            string input;

            if (!Load)      // if new game, output all games for selection
            {
                // clear current save files
                File.WriteAllText(SaveStatus, string.Empty);

                WriteLine("\nWhat game would you like to play?\n");
                WriteLine("The options are >>");

                // create list of Game subclasses
                Type parentType = typeof(Game);
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] types = assembly.GetTypes();
                IEnumerable<Type> subclasses = types.Where(t => t.IsSubclassOf(parentType));

                // output list of subclass names
                foreach (Type type in subclasses)
                {
                    Console.WriteLine(" {0}. {1}", count, type.Name);
                    count++;
                }

                input = ReadLine();
            }
            else // if loading game, load game type
            {
                // read line 1 of document
                input = StreamReader(SaveStatus, 1);
            }

            // instantiate game type
            while (!validInput)
            {
                if (input == "1" || input == "1." || input == "Gomoku")
                {
                    gameInst = new GomokuGame();
                    validInput = true;
                }
                else if (input == "2" || input == "2." || input == "Orthello")
                {
                    gameInst = new OrthelloGame();
                    validInput = true;
                    WriteLine("Sorry, this game is not available yet. Try another >>"); // GAME NOT BUILT
                }
                else
                {
                    WriteLine(" Invalid selection. Please enter a valid selection >>");
                    input = ReadLine();
                }
            }

            // instantiate game equipment
            gameInst.CreateEquipment();

            // call singleton
            gameBoard = Board.GameBoard;

            if (!Load)
            {
                // if new game, save game type to save file
                StreamWriter(SaveStatus, gameInst.GameName);
            }
        }

        

        public void PlayerCreation()
        {
            // create array of player objects
            player = new Player[gameInst.NumberPlayers];

            // if new game, declare single or multiplayer
            if (!Load)
            {
                int NoHP;

                WriteLine("Enter the number of players (1 or 2) >> ");
                bool NoPValid = int.TryParse(ReadLine(), out NoHP);

                while (!NoPValid || NoHP > gameInst.NumberPlayers || NoHP <= 0)
                {
                    WriteLine(" Invalid input. Please enter a valid selection >>");
                    NoPValid = int.TryParse(ReadLine(), out NoHP);
                }
                
                NumHumanPlayers = NoHP;

                // save number of player names needed in save file
                StreamWriter(SaveStatus, (NumHumanPlayers.ToString()));
            }
            else // return number of players from save file
            {
                // save umber of players to save file
                NumHumanPlayers = Int32.Parse(StreamReader(SaveStatus, 2));
            }

            // instantiate player objects name and type
            for (int i = 0; i < gameInst.NumberPlayers; ++i)
            {
                if (i < NumHumanPlayers)
                {
                    bool validName = false;
                    while (!validName)
                    {
                        string name;
                        
                        // if new game, enter the name of the player
                        if (!Load)
                        {
                            WriteLine("\nPlease enter the name for Player {0} >>", (i + 1));
                            name = ReadLine();
                            
                            if (!String.IsNullOrWhiteSpace(name))
                            {
                                player[i] = new HumanPlayer(name);

                                // save player name to save file
                                StreamWriter(SaveStatus, name);
                                validName = true;
                            }
                        }
                        else // load player name
                        {
                            name = StreamReader(SaveStatus, (3 + i));
                            player[i] = new HumanPlayer(name);
                            validName = true;
                        }
                    }
                }
                else
                {
                    player[i] = new ComputerPlayer();
                }
            }
        }



        public void SetPlayer1()
        {
            bool firstPlayerSet = false;
            int firstP = 0; // first player selection
            bool input = false;

            // if new game, input first player
            if (!Load)
            {
                WriteLine("Which player is going first >>");

                // list player names
                for (int i = 0; i < player.Length; ++i)
                {
                    WriteLine(" {0}. {1}", (i+1), player[i].Name);
                }

                input = int.TryParse(ReadLine(), out firstP);

                // set first player index 
                CurrentPlayerIndex = firstP - 1;
            }
            else // load first player index
            {
                CurrentPlayerIndex = Int32.Parse(StreamReader(SaveStatus, 3 + NumHumanPlayers));
                input = true;
            }
            
            // set first player
            while (!firstPlayerSet)
            {
                // check valid input
                if (input && CurrentPlayerIndex < player.Length && CurrentPlayerIndex >= 0)
                {
                    // set first player symbol
                    player[CurrentPlayerIndex].Symbol = gameInst.Pieces[0];
                    firstPlayerSet = true;

                    // number of player symbols set
                    int playersSet = 1;
                    int index = 0;

                    // set symbols to other players
                    while (playersSet < player.Length)
                    {
                        // if this player symbol has not been set
                        if (CurrentPlayerIndex != index)
                        {
                            player[index].Symbol = gameInst.Pieces[playersSet];
                            playersSet++;
                        }
                        index++;
                    }
                }
                else // invalid input, take new input and set to player index
                {
                    WriteLine(" Invalid selection. Please enter a valid selection >>");
                    input = int.TryParse(ReadLine(), out firstP);
                    CurrentPlayerIndex = firstP - 1;
                }
            }

            // if new game, save first player to save file
            if (!Load)
            {
                StreamWriter(SaveStatus, CurrentPlayerIndex.ToString());
            }
        }
        
        
        public void Play()
        {
            // game end
            bool gg = false;

            while (!gg)
            {
                // check there is a valid possible move
                gg = gameInst.CheckValidMoves();

                // if no valid moves, output winner and close
                if (gg)
                {
                    gameBoard.DisplayBoard();
                    WriteLine("\n     Game over!! (there are no valid moves)");

                    // change player index to output correct winner
                    for (int i = 0; i < player.Length; ++i)
                    {
                        if (i != CurrentPlayerIndex)
                        {
                            WriteLine("\n     Congratulations {0}, you are the winner!", player[i].Name);
                            Environment.Exit(0);
                        }
                    }
                }


                // select move, undo, redo, or ask for help
                if (!gg)
                {
                    gameBoard.DisplayBoard();

                    bool undoValid = false;
                    bool redoValid = false;

                    // constant options
                    WriteLine(" You can enter 'H' for help, 'S' to save game, or 'Q' to quit, before selecting an option.");

                    // output play options for correct player
                    WriteLine("\n It is {0}'s move ({1}). Select an option >>", player[CurrentPlayerIndex].Name, player[CurrentPlayerIndex].Symbol);

                    // move option specific to player type
                    if (player[CurrentPlayerIndex].Name == ComputerPlayer.Type)
                    {
                        WriteLine("  1. allow {0} to make its move", player[CurrentPlayerIndex].Name);
                    }
                    else
                    {
                        WriteLine("  1. make your move");
                    }

                    // check move history for undo option
                    if (moveHistory.Index > 0)
                    {
                        WriteLine("  2. undo last move");
                        undoValid = true;
                    }

                    // check move history for redo option
                    if (moveHistory.Index < moveHistory.CommandList.Count)
                    {
                        WriteLine("  3. redo undone move");
                        redoValid = true;
                    }
                    WriteLine();

                    bool input = false;


                    while (!input)
                    {
                        string select = ReadLine();

                        if (select == "1" || select == "1.") // make move
                        {
                            player[CurrentPlayerIndex].MakeMove(gameInst, moveHistory);
                            input = true;
                        }
                        else if ((select == "2" || select == "2.") && undoValid) // undo
                        {
                            moveHistory.UndoCommand();
                            input = true;
                        }
                        else if ((select == "3" || select == "3.") && redoValid) // redo
                        {
                            moveHistory.RedoCommand();
                            input = true;
                        }
                        else if (select == "H" || select == "h") // output help doc
                        {
                            StreamReader reader = new StreamReader(gameInst.HelpFile);
                            string ln = reader.ReadToEnd();
                            WriteLine(ln);
                            reader.Close();
                            WriteLine("\n   Press any key to continue >>");
                            ReadLine();
                            input = true;
                        }
                        else if (select == "S" || select == "s") // save moves history
                        {
                            moveHistory.SaveMoveHistory(SaveMoves);
                            WriteLine("   Game has been saved. Please continue with your selection >>");
                        }
                        else if (select == "Q" || select == "q") // quit game
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            WriteLine(" Invalid selection. Please enter a valid selection >>");
                        }
                    }
                }
                
                // check for win
                if (!gg)
                {
                    gg = gameInst.CheckWin(player[CurrentPlayerIndex].Symbol);
                }
                
                // if gg, output winner and close
                if (gg)
                {
                    gameBoard.DisplayBoard();
                    WriteLine("\n     Congratulations!!! {0} is the winner! ", player[CurrentPlayerIndex].Name);

                    Environment.Exit(0);
                }
                else // change to the next player 
                {
                    if ((CurrentPlayerIndex+1) < player.Length)
                    {
                        CurrentPlayerIndex++;
                    }
                    else
                    {
                        CurrentPlayerIndex = 0;
                    }
                }
            }
        }

        public bool CheckSaves() // check save file valid
        {
            // minimum lines of data for save file to be valid
            int minSaveLines = 4;

            // check for empty lines in file
            for (int i = 1; i <= minSaveLines; i++)
            {
                string saveFile = StreamReader(SaveStatus, i);

                if (String.IsNullOrWhiteSpace(saveFile))
                {
                    return false;
                }
            }
            return true;
        }


        public string StreamReader(string fileName, int line) // reading files
        {
            StreamReader reader = new StreamReader(fileName);
            
            // reads the lines that aren't required
            for (int i = 1; i < line; i++)
            {
                reader.ReadLine();
            }
            string output = reader.ReadLine();
            reader.Close();
            return output;
        }

        public void StreamWriter(string fileName, string input) // writing files
        {
            // add to file rather than overwrite file
            StreamWriter writer = new StreamWriter(fileName, true);
            writer.WriteLine(input);
            writer.Close();
        }

        internal void LoadMoves() //load move history
        {
            // load list of move objects
            moveHistory.LoadMoveHistory(SaveMoves);

            // redo until move history index is equal to the most recent move made
            while (moveHistory.Index < moveHistory.CommandList.Count)
            {
                moveHistory.RedoCommand();
            }
        }
    }
}
