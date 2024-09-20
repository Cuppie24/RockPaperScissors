using ConsoleTables;
using System.Text;
namespace RockPaperScissors
{
    class Program
    {       
        public static void Main(string[] args)
        {            
            int keyLengthInBits = 256;
            while (true)
            {
                int computerMove, playerInput;
                computerMove = MakeAMove(args.Length);
                Cryptography cryptography = new Cryptography();
                byte[] key = cryptography.KeyGenerate(keyLengthInBits);
                byte[] hmac = cryptography.HmacGenerate(args[computerMove], key);
                PrintMenu(args, ByteArrayToHex(hmac));
                if (Int32.TryParse(Console.ReadLine(), out int parsedPlayerMove)) playerInput = parsedPlayerMove;
                else
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
                if(playerInput < 0 || playerInput > args.Length + 1)
                {
                    Console.WriteLine("Out of range of available moves!");
                    continue;
                }
                bool exit = false;
                switch (playerInput)
                {
                    case 0: PrintHelpTable();
                        break;
                    case int i when i == args.Length + 1: exit = true;
                        break;
                }
                if (exit) break;

                Console.WriteLine($"Computer's move: {args[computerMove]}");
                int result = DetermineWinner(args, computerMove, playerInput - 1);
                switch (result)
                {
                    case 2: Console.WriteLine("Draw!");
                        break;
                    case 0: Console.WriteLine("You lose!");
                        break;
                    case 1: Console.WriteLine("You win!");
                        break;
                }
                Console.WriteLine($"Key in Hex: {BitConverter.ToString(key).Replace("-","")}\r\n" +
                    $"Service for finding HMAC: https://www.liavaag.org/English/SHA-Generator/HMAC/\r\n");
            }
        }
        static int DetermineWinner(string[] moves, int computerMoveIndex, int playerMoveIndex)
        {
            if (computerMoveIndex == playerMoveIndex) return 2;
            int[] winningIndexes = [moves.Length / 2];
            for (int i = 0; i < winningIndexes.Length; i++)
            {
                int sourceIndex = (computerMoveIndex + i) % moves.Length;
                winningIndexes[i] = sourceIndex;
            }
            if (winningIndexes.Contains(playerMoveIndex)) return 1; else return 0;
        }

        static void PrintMenu(string[] moves, string hmacHex)
        {
            StringBuilder menu = new StringBuilder();
            int lineCounter = 1;
            menu.AppendLine($"Hmac in Hex: {hmacHex}");
            menu.AppendLine("Available moves: ");
            foreach (string move in moves)
            {
                menu.AppendLine($"{lineCounter++} - {move}");                
            }
            menu.AppendLine($"{lineCounter++} - exit");
            menu.AppendLine("0 - help");
            Console.WriteLine(menu);
        }

        static void PrintHelpTable()
        {
            Console.WriteLine("On progress");
        }
        static int MakeAMove(int numOfMoves)
        {
            Random random = new Random();
            return random.Next(0, numOfMoves);
        }
        static string ByteArrayToHex(byte[] bytes) =>
            BitConverter.ToString(bytes).Replace("-", "");
    }
}
