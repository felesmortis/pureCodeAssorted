using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace radix_sort
{
    class Program
    {
        
        struct KVP
        {
            int key;
            int value;

            public int Key
            {
                get
                {
                    return key;
                }
                set
                {
                    if (key >= 0)
                        key = value;
                    else
                        throw new Exception("Invalid key value");
                }
            }

            public int Value
            {
                get
                {
                    return value;
                }
                set
                {
                    this.value = value;
                }
            }
        }
        static void Main(string[] args)
        {
            int[] array = new int[1000000];
            Random rand = new Random();
            
            StreamWriter w = new StreamWriter("radixsort.txt");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(1000000000);
            }
            //printarr(array);
            foreach (int i in array)
            {
                w.WriteLine(i.ToString());
            }
            array = Sort(array);
            //Console.WriteLine("_________________________________");
            w.WriteLine("_________________________________");
            //printarr(array);
            foreach (int i in array)
            {
                w.WriteLine(i.ToString());
            }
            //Console.ReadLine();
            w.Close();
            
        }
        /*static void writearr(int[] array)
        {
            foreach (int i in array)
            {
                w.WriteLine(i.ToString());
            }
        }*/
        static void printarr(int[] array)
        {
            foreach (int i in array)
            {
                Console.WriteLine(i.ToString() + ", ");
            }

        }
        public static int[] Sort(int[] array)
        {
            return RadixSort(array, 1);
        }
        static int[] RadixSort(int[] array, int digit)
        {
            bool Empty = true;
            KVP[] digits = new KVP[array.Length];//array that holds the digits;
            int[] SortedArray = new int[array.Length];//Hold the sorted array
            for (int i = 0; i < array.Length; i++)
            {
                digits[i] = new KVP();
                digits[i].Key = i;
                digits[i].Value = (array[i] / digit) % 10;
                if (array[i] / digit != 0)
                    Empty = false;
            }

            if (Empty)
                return array;

            KVP[] SortedDigits = CountingSort(digits);
            for (int i = 0; i < SortedArray.Length; i++)
                SortedArray[i] = array[SortedDigits[i].Key];
            return RadixSort(SortedArray, digit * 10);
        }
        static KVP[] CountingSort(KVP[] ArrayA)
        {
            int[] ArrayB = new int[MaxValue(ArrayA) + 1];
            KVP[] ArrayC = new KVP[ArrayA.Length];

            for (int i = 0; i < ArrayB.Length; i++)
                ArrayB[i] = 0;

            for (int i = 0; i < ArrayA.Length; i++)
                ArrayB[ArrayA[i].Value]++;

            for (int i = 1; i < ArrayB.Length; i++)
                ArrayB[i] += ArrayB[i - 1];

            for (int i = ArrayA.Length - 1; i >= 0; i--)
            {
                int value = ArrayA[i].Value;
                int index = ArrayB[value];
                ArrayB[value]--;
                ArrayC[index - 1] = new KVP();
                ArrayC[index - 1].Key = i;
                ArrayC[index - 1].Value = value;
            }
            return ArrayC;
        }

        static int MaxValue(KVP[] arr)
        {
            int Max = arr[0].Value;
            for (int i = 1; i < arr.Length; i++)
                if (arr[i].Value > Max)
                    Max = arr[i].Value;
            return Max;
        }
    }
}
