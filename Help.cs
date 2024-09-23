using ConsoleTables;
namespace RockPaperScissors
{
    internal class Help
    {
        public void PrintHelpTable(string[] moves)
        {
            string win = "Win", lose = "Lose", draw = "Draw";
            if (moves.Length == 0 || moves.Length % 2 == 0) Console.WriteLine("Invalid moves list");
            var helpTable = new ConsoleTable("v PC/User >");
            for(int i = 0; i < moves.Length; i++)
            {
                helpTable.AddColumn([moves[i]]);
            }
            for (int i = 0; i < moves.Length; i++)
            {
                string[] row = new string[moves.Length + 1];
                row[0] = moves[i];
                int rowPointer = 1;
                int[] winningMovesIndexes = GetWinningMovesIndexes(moves, i);
                for(int j = 0; j < moves.Length; j++)
                {
                    if (j == i)
                    {
                        row[rowPointer++] = draw;
                        continue;
                    }
                    if (winningMovesIndexes.Contains(j))
                    {
                        row[rowPointer++] = win;
                        continue;
                    }
                    else
                        row[rowPointer++] = lose;                    
                }
                helpTable.AddRow(row);
            }
            helpTable.Write();  
        }

        int[] GetWinningMovesIndexes(string[] moves, int computerMoveIndex)
        {
            int[] winningIndexes = new int[moves.Length / 2];
            for (int i = 0; i < winningIndexes.Length; i++)
            {
                int sourceIndex = (computerMoveIndex + i + 1) % moves.Length;
                winningIndexes[i] = sourceIndex;
            }
            return winningIndexes;
        }
    }
}
