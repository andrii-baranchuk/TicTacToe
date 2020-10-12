using System.Collections.Generic;

namespace Assets.Scripts.GameMechanic.Extensions
{
    public static class ArrayExtension
    {
        public static (List<T> Elements, List<(int, int)> Indexes) GetRow<T>(this T[,] array, int rowIndex)
        {
            var elements = new List<T>();
            var indexes = new List<(int, int)>();

            for (int columnIndex = 0; columnIndex < array.GetLength(1); columnIndex++)
            {
                elements.Add(array[rowIndex, columnIndex]);
                indexes.Add((rowIndex, columnIndex));
            }

            return (elements, indexes);
        }

        public static (List<T> Elements, List<(int, int)> Indexes) GetColumn<T>(this T[,] array, int columnIndex)
        {
            var elements = new List<T>();
            var indexes = new List<(int, int)>();

            for (int rowIndex = 0; rowIndex < array.GetLength(0); rowIndex++)
            {
                elements.Add(array[rowIndex, columnIndex]);
                indexes.Add((rowIndex, columnIndex));
            }

            return (elements, indexes);
        }

        public static (List<T> Elements, List<(int, int)> Indexes) GetMainDiagonal<T>(this T[,] array)
        {
            var elements = new List<T>();
            var indexes = new List<(int, int)>();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                elements.Add(array[i, i]);
                indexes.Add((i, i));
            }

            return (elements, indexes);
        }

        public static (List<T> Elements, List<(int, int)> Indexes) GetReverseDiagonal<T>(this T[,] array)
        {
            var elements = new List<T>();
            var indexes = new List<(int, int)>();

            int arrayLength = array.GetLength(0);
            for (int i = 0; i < arrayLength; i++)
            {
                elements.Add(array[i, arrayLength - i - 1]);
                indexes.Add((i, arrayLength - i - 1));
            }

            return (elements, indexes);
        }
    }
}
