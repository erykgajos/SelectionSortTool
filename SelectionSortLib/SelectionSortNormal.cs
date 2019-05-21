using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SelectionSortLib
{
    public class SelectionSortNormal : ISelectionSort
    {
        List<int> numbers;
        readonly List<int> unsortedNumbers;
        readonly int sizeOfArray;

        public List<int> GetSortedArray()
        {
            return numbers;
        }

        public SelectionSortNormal(List<int> numbers)
        {
            this.unsortedNumbers = numbers.ToList();
            this.numbers = numbers.ToList();
            sizeOfArray = numbers.Count;
        }

        public double Sort()
        {
            int i, j, indexOfMinValue;
            double sumOfTime = 0;

            for (int x = 0; x < Functions.NUMBER_OF_LOOPS; x++)
            {
                numbers = unsortedNumbers.ToList(); // Restoring the array
                Stopwatch sw = new Stopwatch();
                sw.Start(); // Start the timer

                #region Sorting procedure
                for (i = 0; i < sizeOfArray; i++)
                {
                    indexOfMinValue = i;
                    for (j = i + 1; j < sizeOfArray; j++)
                        if (numbers[j] < numbers[indexOfMinValue])
                            indexOfMinValue = j;
                    Functions.Swap(numbers, indexOfMinValue, i);
                }
                #endregion

                sw.Stop(); // Stop the timer
                sumOfTime += sw.Elapsed.TotalMilliseconds;
            }

            //double elapsedTime = sw.Elapsed.TotalMilliseconds;
            double elapsedTime = sumOfTime / Functions.NUMBER_OF_LOOPS; // Calculate the average
            return elapsedTime;
        }
    }

}
