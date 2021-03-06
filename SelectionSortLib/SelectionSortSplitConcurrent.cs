﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SelectionSortLib
{
    public class SelectionSortSplitConcurrent : ISelectionSort
    {
        List<int> numbers;
        List<int> half1;
        List<int> half2;
        readonly List<int> unsortedNumbers;
        readonly int sizeOfArray;
        List<List<int>> halfs;

        public SelectionSortSplitConcurrent(List<int> numbers)
        {
            this.unsortedNumbers = numbers.ToList();
            this.numbers = numbers.ToList();
            sizeOfArray = numbers.Count;
        }

        private void SplitInHalf()
        {
            half1 = new List<int>();
            half2 = new List<int>();

            halfs = new List<List<int>>();

            for (int i = 0; i < (numbers.Count / 2); i++)
            {
                half1.Add(numbers[i]);
            }
            for (int i = numbers.Count / 2; i < numbers.Count; i++)
            {
                half2.Add(numbers[i]);
            }
        }

        public List<int> GetSortedArray()
        {
            return numbers;
        }

        static object lockObj = new object();
        public double Sort()
        {
            double sumOfTime = 0;
            for (int x = 0; x < Functions.NUMBER_OF_LOOPS; x++)
            {
                numbers = unsortedNumbers.ToList(); // Restoring the array
                SplitInHalf();
                Stopwatch sw = new Stopwatch();
                
                sw.Start(); // Start the timer

                var task1 = Task.Run(() => SortPart1());
                var task2 = Task.Run(() => SortPart2());
                Task.WaitAll(task1, task2);
                

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
            int index;

            foreach (List<int> sortedPart in halfs) // read first elements of both halfs
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
                index = 0;
                for (int i = 0; i < numbers.Count; i++) // Check which element is minimum
                {
                    if (numbers[i] <= min)
                    {
                        min = numbers[i];
                        index = i;
                    }
                }

                result.Add(min);                // Add to the result array

                if (halfs[index].Count > 0)     // if in this half is something more
                {
                    numbers[index] = halfs[index].First(); // replace the number with the next from this half
                    halfs[index].RemoveAt(0);   // and remove it from the half
                }
                else                            // if there is nothing more in the half
                {
                    numbers.RemoveAt(index);    // remove the number from temp array
                    halfs.RemoveAt(index);      // and remove this half
                }

            } while (numbers.Count > 0);

            numbers = result;
        }

        private void SortPart1()
        {
            int i, j, indexOfMinValue;

            #region Sorting procedure for first half
            for (i = 0; i < half1.Count; i++)
            {
                indexOfMinValue = i;
                for (j = i + 1; j < half1.Count; j++)
                    if (half1[j] < half1[indexOfMinValue])
                        indexOfMinValue = j;
                Functions.Swap(half1, indexOfMinValue, i);
                Debug("Sort part 1");
            }
            #endregion

            halfs.Add(half1);
        }
        private void SortPart2()
        {
            int i, j, indexOfMinValue;
            
            #region Sorting procedure for second half
            for (i = 0; i < half2.Count; i++)
            {
                indexOfMinValue = i;
                for (j = i + 1; j < half2.Count; j++)
                    if (half2[j] < half2[indexOfMinValue])
                        indexOfMinValue = j;
                Functions.Swap(half2, indexOfMinValue, i);
                Debug("Sort part 2");
            }
            #endregion
            
            halfs.Add(half2);
        }
        static void Debug(string fromFn)
        {
            lock (lockObj)
            {
                //Console.WriteLine("Called from " + fromFn);
            }
        }
    }
}
