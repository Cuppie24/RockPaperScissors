using ConsoleTables;
namespace RockPaperScissors
{
    internal class Help
    {
        private string[] _moves;
        private int[,] _matrix;

        public void PrintHelpMenu()
        {
            string divider = "--------------------------------------------------------------------------------------------------------";
            Console.WriteLine("1 - result matrix\r\n" +
                "2 - about HMAC");
            string playerInput = Console.ReadLine();
            switch (playerInput)
            {
                case "1":
                    PrintHelpTable();
                    break;
                case "2":
                    Console.WriteLine($"\r\n{divider}\r\nBefore making its move, the computer generates a secure cryptographic key.\r\n" +
                        "It uses this key to create an HMAC based on its move and shows it to you. At the end of the round,\r\n" +
                        "the computer announces the result and reveals the original key.\r\n\r\n" +
                        "Now you can use a third-party service to independently calculate the HMAC for the computer's move using this key.\r\n" +
                        $"This allows you to verify that the computer did not change its move after you made yours.(Use the move text as a message)\r\n{divider}\r\n");
                    break;
                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }

        }
        public void PrintHelpTable()
        {
            ConsoleTable table = new ConsoleTable("v PC/User >");
            var resultMap = new Dictionary<int, string>
            {
                { 0, "Draw" },
                { 1, "Win" },
                { -1, "Lose" }
            };

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
                    row[j + 1] = resultMap.TryGetValue(_matrix[j, i], out var result) ? result : "error";
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
