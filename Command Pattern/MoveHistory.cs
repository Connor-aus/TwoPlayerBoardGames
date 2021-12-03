using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace IFN563___BoardGame.Command_Pattern
{
    public class MoveHistory
    {
        public List<Move> CommandList { get; set; } = new List<Move>(); // move history or list of moves
        public int Index { get; set; } // current position within move history - referenced within game controller

        

        public void AddCommand(Move command) // add move to list of moves and execute
        {
            if (Index < CommandList.Count)
            {
                CommandList.RemoveRange(Index, CommandList.Count - Index);
            }

            CommandList.Add(command);
            command.Execute();
            Index++;
        }


        public void UndoCommand() // undo move and update move index
        {
            if (Index == 0)
            {
                return;
            }
            if (Index > 0)
            {
                CommandList[Index - 1].Undo();
                Index--;
            }
        }


        public void RedoCommand() // redo move and update move index
        {
            if (Index >= CommandList.Count)
            {
                return;
            }
            if (Index < CommandList.Count)
            {
                Index++;
                CommandList[Index - 1].Execute();
            }
        }

        public void SaveMoveHistory(string saveMoves)
        {
            Stream stream = File.OpenWrite(saveMoves);

            XmlSerializer xmlSer = new XmlSerializer(typeof(List<Move>));

            xmlSer.Serialize(stream, CommandList);

            stream.Close();
        }

        public void LoadMoveHistory(string savedMoves)
        {
            XmlSerializer xmlDeSer = new XmlSerializer(typeof(List<Move>));

            using (FileStream stream = File.Open(savedMoves, FileMode.Open))
            {
                CommandList = (List<Move>)xmlDeSer.Deserialize(stream);

                stream.Close();
            }

        }
    }
}
