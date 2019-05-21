using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SelectionSortLib
{
    public class SelectionSortNormalWithSplit : ISelectionSort
    {
        List<int> numbers;
        readonly List<int> unsortedNumbers;
        readonly int sizeOfArray;
        List<List<int>> sortedParts;

        public SelectionSortNormalWithSplit(List<int> numbers)
        {
            this.unsortedNumbers = numbers.ToList();
            this.numbers = numbers.ToList();
            sizeOfArray = numbers.Count;
            sortedParts = new List<List<int>>();
        }

        public List<int> GetSortedArray()
        {
            return numbers;
        }

        public double Sort()
        {
            double sumOfTime = 0;
            for (int x = 0; x < Functions.NUMBER_OF_LOOPS; x++)
            {
                numbers = unsortedNumbers.ToList(); // Restoring the array
                Stopwatch sw = new Stopwatch();

                sw.Start(); // Start the timer
                SortParts(numbers);
                JoinParts();
                sw.Stop(); // Stop the timer
                sumOfTime += sw.Elapsed.TotalMilliseconds;
            }

            double elapsedTime = sumOfTime / Functions.NUMBER_OF_LOOPS; // Calculate the average
            return elapsedTime;
        }

        private void JoinParts()
        {
            numbers = new List<int>();
            List<int> result = new List<int>();
            int index = 0;

            foreach (List<int> sortedPart in sortedParts)
            {
                if (sortedPart.Count > 0)
                {
                    numbers.Add(sortedPart.First());
                    sortedPart.RemoveAt(0);
                }
            }

            int min;
            do
            {
                min = numbers[0];
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (numbers[i] <= min)
                    {
                        min = numbers[i];
                        index = i;
                    }
                }

                result.Add(min);
                //sortedParts[index].RemoveAt(0);
                if (sortedParts[index].Count > 0)
                {
                    numbers[index] = sortedParts[index].First();
                    sortedParts[index].RemoveAt(0);
                }
                else
                {
                    numbers.RemoveAt(index);
                    sortedParts.RemoveAt(index);
                }
            } while (numbers.Count > 0);

            numbers = result;
        }

        private void SortParts(List<int> table)
        {
            if (table.Count > 2 && table.Count % 2 == 0)    // Spliting table in half if possible
            {
                List<int> PartA = new List<int>();
                List<int> PartB = new List<int>();

                for (int i = 0; i < (table.Count / 2); i++)
                {
                    PartA.Add(table[i]);
                }
                for (int i = table.Count / 2; i < table.Count; i++)
                {
                    PartB.Add(table[i]);
                }
                SortParts(PartA);
                SortParts(PartB);
            }
            else
            {
                int i, j, indexOfMinValue;

                #region Sorting procedure
                for (i = 0; i < table.Count; i++)
                {
                    indexOfMinValue = i;
                    for (j = i + 1; j < table.Count; j++)
                        if (table[j] < table[indexOfMinValue])
                            indexOfMinValue = j;
                    Functions.Swap(table, indexOfMinValue, i);
                }
                sortedParts.Add(table);
                #endregion
            }
        }
    }
}