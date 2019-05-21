using System;
using System.Collections.Generic;
using System.Text;


namespace SelectionSortLib
{
    public static class Functions
    {
        public const int NUMBER_OF_LOOPS = 1;
        const int TABLE_SIZE = 100; // Random table size
        const int MIN_VALUE = 1;  // Min value in table 
        const int MAX_VALUE = 100;  // Max value in table
        public static List<int> GetArray()
        {
            Random rnd = new Random(1);
            List<int> numbers = new List<int>();

            for (int i = 0; i < TABLE_SIZE; i++)
            {
                numbers.Add(rnd.Next(MIN_VALUE, MAX_VALUE+1));
            }

            return numbers;
        }

        public static void Swap(List<int> list, int indexA, int indexB)
        {
            int temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
    }
}
