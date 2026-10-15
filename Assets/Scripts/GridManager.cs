using UnityEngine;

namespace Tetris
{
    public class GridManager : MonoBehaviour
    {
        void Start()
        {
            // 1. Declare and initialize a 22x10 2D array
            int[,] matrix = new int[22, 10];

            // 2. Fill the matrix with values (Example: Row-by-Column)
            for (int row = 0; row < 22; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    matrix[row, col] = row * col; // Assign your integer data here
                }
            }

            // 3. Read a specific value (e.g., Row 5, Column 3)
            int value = matrix[5, 3];
            Debug.Log($"Value at [5,3] is: {value}");
        }
    }
}
