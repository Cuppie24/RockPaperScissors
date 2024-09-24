using ConsoleTables;
namespace RockPaperScissors
{
    internal class Help
    {
        private string[] _moves;
        private int[,] _matrix;
        public void PrintHelpTable()
        {
            ConsoleTable table = new ConsoleTable("v PC/User >");
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                string[] column = { _moves[i] };
                table.AddColumn(column);
            }
            for (int i = 0; i < _matrix.GetLength(1); i++)
            {
                string[] row = new string[_moves.Length + 1];
                row[0] = _moves[i];
                for (int j = 0; j < _matrix.GetLength(0); j++)
                {
                    switch (_matrix[j, i])
                    {
                        case 0:
                            row[j + 1] = "Draw";
                            break;
                        case 1:
                            row[j + 1] = "Win";
                            break;
                        case -1:
                            row[j + 1] = "Lose";
                            break;
                        default:
                            row[j + 1] = "Error";
                            break;
                    }                    
                }
                table.AddRow(row);
            }
            table.Write();
        }
        public Help(string[] moves, int[,] matrix)
        {
            _moves = moves;
            _matrix = matrix;
        }
    }
}
