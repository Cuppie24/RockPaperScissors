using System.Text;
namespace RockPaperScissors
{
    class Program
    {       
        public static void Main(string[] args)
        {
            if (args.Length % 2 == 0 || args.Length == 0) throw new Exception("There is no entry arguments!");
            int keyLengthInBits = 256;
            Cryptography cryptography = new Cryptography();
            Help help = new Help();
            while (true)
            {
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
                    case 0: help.PrintHelpTable(args);    
                        shouldContinue = true;
                        break;
                    case int i when i == args.Length + 1: shouldExit = true;
                        break;
                }
                if (shouldExit) break;
                if (shouldContinue) continue;

                Console.WriteLine($"Computer's move: {args[computerMove]}");
                int result = DetermineWinner(args, computerMove, playerInput - 1);
                switch (result)
                {
                    case 0:
                        Console.WriteLine("You lose!");
                        break;
                    case 1:
                        Console.WriteLine("You win!");
                        break;
                    case 2: Console.WriteLine("Draw!");
                        break;
                }
                Console.WriteLine($"Key in Hex: {ByteArrayToHex(key)}\r\n" +
                    $"Service for finding HMAC: https://www.liavaag.org/English/SHA-Generator/HMAC/\r\n");
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

        static int DetermineWinner(string[] moves, int computerMoveIndex, int playerMoveIndex)
        {
            if (computerMoveIndex == playerMoveIndex) return 2;

            //first solution (too slow)
            int[] winningIndexes = new int[moves.Length / 2];
            for (int i = 0; i < winningIndexes.Length; i++)
            {
                int sourceIndex = (computerMoveIndex + i + 1) % moves.Length;
                winningIndexes[i] = sourceIndex;
            }
            if (winningIndexes.Contains(playerMoveIndex)) return 1; else return 0;


            ////second solution
            //int numberOfWinningMoves = moves.Length / 2;
            //if (computerMoveIndex + numberOfWinningMoves > moves.Length - 1)
            //{
            //    int remainder = (computerMoveIndex + numberOfWinningMoves) - (moves.Length - 1);
            //    if (playerMoveIndex > computerMoveIndex && playerMoveIndex < moves.Length) return 1;
            //    else if (playerMoveIndex <= remainder - 1 && playerMoveIndex >= 0) return 1;
            //    else return 0;
            //}
            //else
            //{
            //    if (playerMoveIndex > computerMoveIndex && playerMoveIndex < computerMoveIndex + numberOfWinningMoves) return 1; else return 0;
            //}
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
