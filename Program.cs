using Org.BouncyCastle.Asn1;
using System.Text;
namespace RockPaperScissors
{
    class Program
    {       
        public static void Main(string[] args)
        {
            if (args.Length % 2 == 0 || args.Length == 0) throw new Exception("There is no entry arguments and the number of arguments must be odd!");
            int keyLengthInBits = 256;
            Cryptography cryptography = new Cryptography();            
            MovesMatrix movesMatrix = new MovesMatrix(args);
            Help help = new Help(args, movesMatrix.Matrix);

            while (true)
            {
                string divider = "\r\n----------------------------------------------------------------------------------------------------\r\n";
                bool shouldExit = false, shouldContinue = false;
                int computerMove, playerInput;
                computerMove = MakeAMove(args.Length);                
                byte[] key = cryptography.GenerateKey(keyLengthInBits);
                byte[] hmac = cryptography.GenerateHMAC(args[computerMove], key);

                PrintMenu(args, ByteArrayToHex(hmac));
                if (Int32.TryParse(Console.ReadLine(), out int parsedPlayerMove)) playerInput = parsedPlayerMove;
                else
                {
                    Console.WriteLine("Incorrect input!");
                    continue;
                }
                if(playerInput < 0 || playerInput > args.Length + 1)
                {
                    Console.WriteLine("There is no such choise!");
                    continue;
                }           
                
                switch (playerInput)
                {
                    case 0: help.PrintHelpMenu();    
                        shouldContinue = true;
                        break;
                    case int i when i == args.Length + 1: shouldExit = true;
                        break;
                }
                if (shouldExit) break;
                if (shouldContinue) continue;

                Console.WriteLine($"Computer's move: {args[computerMove]}");
                int result = movesMatrix.Matrix[playerInput - 1, computerMove];
                switch (result)
                {
                    case 0:
                        Console.WriteLine("Draw!");
                        break;
                    case 1:
                        Console.WriteLine("You win!");
                        break;
                    case -1: Console.WriteLine("You lose!");
                        break;
                }
                Console.WriteLine($"Key in Hex: {ByteArrayToHex(key)}\r\n" +
                    $"Service for finding HMAC: https://www.liavaag.org/English/SHA-Generator/HMAC/\r\nEncryption method SHA-256{divider}");
            }
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

        static int MakeAMove(int numOfMoves)
        {
            Random random = new Random();
            return random.Next(0, numOfMoves);
        }
        static string ByteArrayToHex(byte[] bytes) =>
            BitConverter.ToString(bytes).Replace("-", "");
    }
}
