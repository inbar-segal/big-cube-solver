using System;
using System.Collections.Generic;
using System.Text;

namespace BigCubeSolver1025.Utils
{
    public class Funcs
    {
        public static T[,] RotateMatrix<T>(T[,] matrix)
        {
            T[,] newMatrix = new T[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[matrix.GetLength(0) - j - 1, i];
                }
            }

            return newMatrix;
            //TODO understand code
        }
    }
}
