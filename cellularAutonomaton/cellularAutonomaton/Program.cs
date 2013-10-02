using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellularAutonomaton
{
    class Program
    {
        struct patVal
        {
            public String pat;
            public int val;
            public bool repX;
        };

        static patVal[] patt = new patVal[8];
        static void init()
        {
            String[] list = {"...", "..x", ".x.",
                           ".xx", "x..", "x.x",
                           "xx.", "xxx" };
            int pow = 1;
            for (int i = 0; i < 8; i++)
            {
                patt[i].pat = list[i];
                patt[i].val = pow;
                pow = pow << 1;
            }
            setFalse();

        }
        static void setFalse()
        {
            for (int i = 0; i < patt.Length; i++)
            {
                patt[i].repX = false;
            }
        }
        static void Main(string[] args)
        {
            init();
            /*foreach (patVal p in patt)
            {
                Console.WriteLine(p.pat + " " + p.val);
            }
            foreach (int i in getPattern(255))
            {
                Console.WriteLine(i);
            }*/
            Console.WriteLine(cellular_automaton(".x.x.x.x.", 17, 2));
            Console.WriteLine(cellular_automaton(".x.x.x.x.", 249, 3));
            Console.WriteLine(cellular_automaton("...x....", 125, 1));
            Console.WriteLine(cellular_automaton("...x....", 125, 2));
            Console.WriteLine(cellular_automaton("...x....", 125, 3));
            Console.WriteLine(cellular_automaton("...x....", 125, 4));
            Console.WriteLine(cellular_automaton("...x....", 125, 5));
            Console.WriteLine(cellular_automaton("...x....", 125, 6));
            Console.WriteLine(cellular_automaton("...x....", 125, 7));
            Console.WriteLine(cellular_automaton("...x....", 125, 8));
            Console.WriteLine(cellular_automaton("...x....", 125, 9));
            Console.WriteLine(cellular_automaton("...x....", 125, 10));
            Console.WriteLine(cellular_automaton("...........x...........", 142, 10));
            Console.ReadLine();
        }
        static string cellular_automaton(String current, int pattern, int generation)
        {
            setFalse();
            int[] patternNums = getPattern(pattern);
            int index = 0;
            foreach (int p in patternNums)
            {

                for (int i = 0; i < patt.Length; i++)
                {
                    if (p == patt[i].val)
                        patt[i].repX = true;
                }
                index++;
            }
            return cellular_automaton(current, generation);
        }
        static string cellular_automaton(String current, int generation)
        {

            if (generation < 1)
                return current;

            //String[] currentRepX = new String[patternNums.Length];

            String final = current.ElementAt(0).ToString();
            for (int i = 1; i < current.Length - 1; i++)
            {
                String temp = "";
                temp = current.Substring((i - 1), 3);
                foreach (patVal p in patt)
                {
                    if (p.pat == temp)
                    {

                        if (p.repX)
                            final += "x";
                        else
                            final += ".";
                    }
                }
            }

            final += current.ElementAt(current.Length - 1).ToString();
            //Console.WriteLine(final);
            return cellular_automaton(final, --generation);
        }
        static int[] getPattern(int i)
        {
            int[] num = new int[8];
            int index = 0;
            int n = 1;
            while (i > 0)
            {

                while (n <= i)
                    n = n << 1;
                n = n >> 1;
                num[index] = n;
                index++;
                i -= n;
                n = 1;
            }
            num = prune(num);
            return num;
        }
        static int[] prune(int[] arr)
        {
            int finIndex = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                    finIndex = i;
            }
            int[] finArr = new int[++finIndex];
            for (int i = 0; i < finIndex; i++)
            {
                finArr[i] = arr[i];
            }
            return finArr;
        }
    }
}
