using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureGradedAssignment
{
    internal class Algos
    { 
        public void MergeSort(int[] array)
        {
            if (array.Length <= 1)
            {
             //   Console.WriteLine($"Base case reached: Array {string.Join(", ", array)} is already sorted.");
                return;
            }

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            for (int i = 0; i < mid; i++)
            {
                left[i] = array[i];
            }

            //  Console.WriteLine($"Splitting array: {string.Join(", ", array)}");
            for (int i = mid; i < array.Length; i++)
            {
                right[i - mid] = array[i];
            }

          //  Console.WriteLine($"Left subarray: {string.Join(", ", left)}");
           //   Console.WriteLine($"Right subarray: {string.Join(", ", right)}");
            MergeSort(left);
            MergeSort(right);
          //    Console.WriteLine($"Merging arrays: Left: {string.Join(", ", left)} | Right: {string.Join(", ", right)}");
            Merge(array, left, right);
           //  Console.WriteLine($"Merged array: {string.Join(", ", array)}");
        }
        private void Merge(int[] array, int[] left, int[] right)
        {
            int k = 0, j = 0, i = 0; // these ints help traverse k = arary, j = right, k = left

            while (i < left.Length && j < right.Length) //  until the arrays reach an end
            {
                if (left[i] <= right[j]) //  if left smaller than right, i goes in the mergeged array first // else j goes first -- thus it is sorted
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            while (i < left.Length)    //Both while loops are used at the end of the sorting arangements, they put back the values into the main array
            {
                array[k++] = left[i++];
            }

            while (j < right.Length)
            {
                array[k++] = right[j++];
            }
            // Console.WriteLine($"After merging: {string.Join(", ", array)}");
        }  
        public void QuickSort(int[] array)             
        {
            QuickSortRecursive(array, 0, array.Length - 1);       //The function now can determine the length of the array internally. 0 = low, array.Length - 1 = high
        }
        public void QuickSortRecursive(int[] array, int low, int high)  
        static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];                  //Pivot is the farthest to right number in the array
            int i = low - 1;                  // Intex where all the numbers lesser than the pivot should go

            for (int j = low; j < high; j++)           // Going trough the array until the low(j) reatches the pibot(high)
            {                       
                if (array[j] <= pivot)           // if j is equal or smallare than the pivot, "i" will go to next index and then swap with j 
                {
                    i++;
                    int tempVal1 = array[i];
                    array[i] = array[j];
                    array[j] = tempVal1;
                }
            }
            int tempVal2 = array[i + 1];    // Swap array[i + 1] and array[high] to place the pivot in its correct position (last inxdex in the current array)
            array[i + 1] = array[high];
            array[high] = tempVal2;

            return i + 1;    // Returns the index where the pivot is now located, which will be used in the recursive calls to QuickSort to sort the two sub-arrays.
        }
        public void SelectionSort(int[] array)
        {
            int tempVar = 0;
            for (int x = 0; x < array.Length; x++)  // Goes trough every index in the array
            {
                int lowestValueIndex = x; // The minimum value of the unsorted array

                for (int y = x + 1; y < array.Length; y++)          //Starts at index x + 1 until the end of the array 
                {
                    if (array[y] < array[lowestValueIndex])      // If the current element is smaller than the element at the lowestValueIndex, update lowestValueIndex
                    {
                        lowestValueIndex = y;
                    }
                }
                // If the index of the minimum value is different from the current index, swap the values
                if (array[x] != array[lowestValueIndex])
                {
                 //   Console.WriteLine("Switching indexes " + array[x] + " and " + array[lowestValueIndex] + " because " + array[lowestValueIndex] + " is the lowest value in the unsorted part of the array.");
                    tempVar = array[x];
                    array[x] = array[lowestValueIndex];
                    array[lowestValueIndex] = tempVar;
                }
            }
        }
        public void BubbleSort(int[] array)
        {
            int temporaryVar = 0;
            // Console.WriteLine("Bubble Sorting Started :");
            for (int x = 0; x < array.Length - 1; x++)
            {
                for (int y = 0; y < array.Length - 1 - x; y++)
                {
                    //    Console.WriteLine("Comparing " + array[y] + " to " + array[y + 1]);
                    if (array[y] < array[y + 1])
                        //  Console.WriteLine("Cant switch " + array[y] + " with " + array[y + 1] + " because " + array[y] + " is smaller than " + array[y + 1]);
                        if (array[y] > array[y + 1])
                        {
                            //   Console.WriteLine("Switching indexes " + array[y] + " and " + array[y + 1]);
                            temporaryVar = array[y + 1];
                            array[y + 1] = array[y];
                            array[y] = temporaryVar;
                        }
                }
            }
        }
        public void InsertionSort(int[] array)
        {
            int tempVar = 0;
            for (int x = 0; x < array.Length; x++) // each index in the array
            {
                int y = x; 
                while (y > 0 && array[y] < array[y - 1]) // 2 indexes at a time, they will swap accordingly
                {
                    tempVar = array[y];
                    array[y] = array[y - 1];
                    array[y - 1] = tempVar;
                    y--;
                }
            }
        }

    }
}

