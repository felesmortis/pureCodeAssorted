using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] curly = { '{', '}' };
            Object[] arr5 = { 8, 8, 9, 8, 10 };
            Object[] arr2 = { 3, 4, 5 };
            Object[] arr = { 1, 2, 5, arr2 };
            Object[] arr3 = { 1, 1, 2, 3, 6, 7, arr5, 2 }, arr4 = { 1, 2, 3, 5, 6, 7 };
            Console.WriteLine("\narr2: ");
            print(check(arr2.ToList<Object>()));
            Console.WriteLine("\narr: ");
            print(check(arr.ToList<Object>()));
            Console.WriteLine("\narr3: ");
            print(check(arr3.ToList<Object>()));
            Console.WriteLine("\narr4: ");
            print(check(arr4.ToList<Object>()));
            Console.WriteLine("Would you like to try your own array?(Y or N)");
            while (Console.ReadLine().ToUpper() == "Y")
            {
                bool valid = true;

                String strarr = "";
                Console.WriteLine("\n\nPlease enter an array.\nEnclose array with curly brackets. Seperate values with comma's.\nExample:{1,2,3,4,7} or {1,2,3,{7,8,9},4,5}");
                strarr = Console.ReadLine();
                strarr = strarr.Trim();

                String[] arrarr = strarr.Substring(1, strarr.LastIndexOf('}')).Split(curly);
                List<Object> lobarr = new List<Object>();
                List<Object> flobarr = new List<Object>();
                foreach (String o in arrarr)
                    lobarr.AddRange(o.Split(',').ToArray<Object>());
                try
                {
                    for (int i = 0; i < lobarr.Count; i++)
                    {
                        if ((string)lobarr[i] != "")
                            flobarr.Add(int.Parse(lobarr[i].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    valid = false;
                }
                if (valid)
                {
                    Console.WriteLine("\nYour Array's longest sequence of consecutive integers: ");
                    print(check(flobarr.ToList<Object>()));
                }
                Console.WriteLine("Would you like to try again?(Y or N)");
            }
        }
        static void print(Object[] l)
        {
            foreach (Object o in l)
                Console.WriteLine(o.ToString());
        }
        static object[] check(List<Object> l)
        {
            int index = 0;
            int[] inl = { 0, 0 };
            l = makelist(l);
            l.Sort();
            l = l.Distinct<Object>().ToList<Object>();
            while (index < l.Count)
            {
                int con = lconsecutive(l, index);
                if (con > inl[1])
                {
                    inl[0] = index;
                    inl[1] = con;

                }
                index += con;
            }

            return l.GetRange(inl[0], inl[1]).ToArray();
        }
        static List<Object> makelist(List<Object> list)
        {
            List<Object> tlist = new List<Object>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetType() != i.GetType())
                {
                    Object[] temp = (Object[])list[i];
                    tlist.AddRange(makelist(temp.ToList<Object>()));
                    list.RemoveAt(i);
                }

            }
            list.AddRange(tlist);
            return list;
        }
        static int lconsecutive(List<Object> list, int startindex)
        {
            return lconsecutive(list, startindex, 0);
        }
        static int lconsecutive(List<Object> list, int startindex, int step)
        {

            if (startindex + 2 <= list.Count)
            {
                Object x = list[startindex], x1 = list[startindex + 1];
                if ((x.GetType() == startindex.GetType() || x1.GetType() == startindex.GetType()))
                {
                    if ((int)x1 == (int)x + 1)
                    {

                        return lconsecutive(list, ++startindex, ++step);
                    }
                }
            }
            return step + 1;
        }
    }
}