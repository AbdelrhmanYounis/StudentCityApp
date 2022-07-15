using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCity.BL.Services.Utilities
{
    class SortingClass
    {
        void heapify(double[,] arr, int n, int i)
        {
            int largest = i; // Initialize largest as root 
            int l = 2 * i + 1; // left = 2*i + 1 
            int r = 2 * i + 2; // right = 2*i + 2 

            // If left child is larger than root 
            if (l < n && arr[l,1] > arr[largest,1])
                largest = l;

            // If right child is larger than largest so far 
            if (r < n && arr[r,1] > arr[largest,1])
                largest = r;

            // If largest is not root 
            if (largest != i)
            {
                double temp_val = arr[i, 1], temp_id = arr[i, 0];

                arr[i, 0] = arr[largest, 0];
                arr[i, 1] = arr[largest, 1];

                arr[largest, 0] = temp_id;
                arr[largest, 1] = temp_val;

                // Recursively heapify the affected sub-tree 
                heapify(arr, n, largest);
            }
        }
        /*******************************************************************************************/
        public double[,] sort(double[,] arr)
        {
            int n = arr.Length/2;

            // Build heap (rearrange array) 
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(arr, n, i);

            // One by one extract an element from heap 
            for (int i = n - 1; i >= 0; i--)
            {

                // Move current root to end 
                double temp_val = arr[0, 1], temp_id = arr[0, 0];

                arr[0, 0] = arr[i, 0];
                arr[0, 1] = arr[i, 1];

                arr[i, 0] = temp_id;
                arr[i, 1] = temp_val;

                // call max heapify on the reduced heap 
                heapify(arr, i, 0);
            }
            return arr;
        }
        /*******************************************************************************************/

        public double[,] quickSort(double[,] arr, int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                quickSort(arr, low, pi - 1);
                return quickSort(arr, pi + 1, high);

            }
            else
                return arr;
        }

        /*******************************************************************************************/

        static int partition(double[,] arr, int low, int high)
        {
            double pivot = arr[high, 1];

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than or equal to pivot 
                if (arr[j, 1] >= pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    double temp_val = arr[i, 1], temp_id = arr[i, 0];

                    arr[i, 0] = arr[j, 0];
                    arr[i, 1] = arr[j, 1];

                    arr[j, 0] = temp_id;
                    arr[j, 1] = temp_val;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            double temp1 = arr[i + 1, 0], temp2 = arr[i + 1, 1];
            arr[i + 1, 0] = arr[high, 0];
            arr[i + 1, 1] = arr[high, 1];
            arr[high, 0] = temp1;
            arr[high, 1] = temp2;

            return i + 1;
        }

        /*******************************************************************************************/

        static void Shift(double[,] quikArr, int quikArr_length, int index1, int index2)
        {
            //main objective ==> but index1 before index2 and shift the array
            double id = quikArr[index2, 0], counter = quikArr[index2, 1], current_id, current_counter;
            quikArr[index2, 0] = quikArr[index1, 0];
            quikArr[index2, 1] = quikArr[index1, 1];


            for (int begin = (index2) + 1; begin < quikArr_length - 1; begin++)
            {
                current_id = quikArr[begin, 0];
                current_counter = quikArr[begin, 1];

                quikArr[begin, 0] = id;
                quikArr[begin, 1] = counter;

                id = current_id;
                counter = current_counter;
            }
            quikArr[quikArr_length - 1, 0] = id;
            quikArr[quikArr_length - 1, 1] = counter;
        }

        /*******************************************************************************************/

        public double[,] updateSort(double[,] quikArr, int quikArr_length, int begin, int end)
        {
            if (quikArr[begin, 1] > quikArr[begin - 1, 1])
                for (int i = begin; i < end; i++)
                {
                    if (quikArr[i, 1] > quikArr[0, 1])
                        Shift(quikArr, quikArr_length, i, 0);

                    else if (quikArr[i, 1] > quikArr[(quikArr_length / 2), 1])
                        for (int x = (quikArr_length / 2) - 1; x >= 0; x--)
                        {
                            if (quikArr[i, 1] <= quikArr[x, 1])
                            {
                                Shift(quikArr, quikArr_length, i, x + 1);
                                break;
                            }
                        }
                    else
                        for (int x = (quikArr_length / 2); x <= quikArr_length; x++)
                        {
                            if (quikArr[i, 1] >= quikArr[x, 1])
                            {
                                Shift(quikArr, quikArr_length, i, x);
                                break;
                            }
                        }
                }
            return quikArr;
        }

    }
}
