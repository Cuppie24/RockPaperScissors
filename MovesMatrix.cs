using ConsoleTables;
namespace RockPaperScissors
{
    class MovesMatrix
    {
        private string[] _moves;
        public int[,] Matrix;
        public int[,] GenerateMatrix()
        {
            int[,] matrix = new int[_moves.Length, _moves.Length];
            for(int i = 0; i < _moves.Length; i++)
            {
                int[] winningIndexes = GetWinningIndexes(i);
                for(int j = 0; j < _moves.Length; j++)
                {
                    if(i == j)
                        matrix[i, j] = 0;
                    else if (winningIndexes.Contains(j))
                        matrix[i, j] = 1;
                    else
                        matrix[i, j] = -1;
                }
            }
            return matrix;
        }
        public int[] GetWinningIndexes(int playerMoveIndex)
        {
            int[] winningIndexes = new int[_moves.Length / 2];
            for(int i = 0; i < winningIndexes.Length; i++)
            {
                int pointer = playerMoveIndex - i - 1;
                if (pointer < 0) pointer = _moves.Length - Math.Abs(pointer);
                winningIndexes[i] = pointer;
            }
            return winningIndexes;
        }
        public MovesMatrix(string[] moves)
        {
            if (moves.Length % 2 == 0) throw new Exception("The number of moves must be odd!");
            _moves = moves;
            Matrix = GenerateMatrix();
        }
    }
}
