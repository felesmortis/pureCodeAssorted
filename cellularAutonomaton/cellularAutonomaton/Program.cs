using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellularAutonomaton
{
    class Program
    {
        static int NUMPATTERNS = 8;
        static int SIZEPATTERNS = Convert.ToString(NUMPATTERNS-1, 2).Length;
        struct patVal
        {
            public String pat;
            public int val;
            public bool repX;
        };
        
        static patVal[] patt = new patVal[NUMPATTERNS];
        static void init()
        {
            //int i = 1;
            String[] list = new String[NUMPATTERNS];
            for (int i = 0; i < NUMPATTERNS; i++)
            {
                String temp = Convert.ToString(i, 2).Replace('0', '.').Replace('1', 'x');
                int n = temp.Length;

                if (n < SIZEPATTERNS)
                {
                    for (int l = n; l < SIZEPATTERNS; l++)
                        temp = '.' + temp;
                }
                list[i] = temp;
                Console.WriteLine(temp);
            }
            int pow = 1;
            for (int i = 0; i < NUMPATTERNS; i++)
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
            //Expected - xxxxxxx.. Actual xxxxxxx..
            Console.WriteLine(cellular_automaton(".x.x.x.x.", 249, 3));
            //Expected - .x..x.x.x Actual .x..x.x.x
            Console.WriteLine(cellular_automaton("...x....", 125, 1));
            //Expected - xx.xxxxx Actual xx.xxxxx
            Console.WriteLine(cellular_automaton("...x....", 125, 2));
            //Expected - .xxx.... Actual .xxx....
            Console.WriteLine(cellular_automaton("...x....", 125, 3));
            //Expected - .x.xxxxx Actual .x.xxxxx
            Console.WriteLine(cellular_automaton("...x....", 125, 4));
            //Expected - xxxx...x Actual xxxx...x
            Console.WriteLine(cellular_automaton("...x....", 125, 5));
            //Expected - ...xxx.x Actual ...xxx.x
            Console.WriteLine(cellular_automaton("...x....", 125, 6));
            //Expected - xx.x.xxx Actual xx.x.xxx
            Console.WriteLine(cellular_automaton("...x....", 125, 7));
            //Expected - .xxxxx.. Actual .xxxxx..
            Console.WriteLine(cellular_automaton("...x....", 125, 8));
            //Expected - .x...xxx Actual .x...xxx
            Console.WriteLine(cellular_automaton("...x....", 125, 9));
            //Expected - xxxx.x.x Actual xxxx.x.x
            Console.WriteLine(cellular_automaton("...x....", 125, 10));
            //Expected - ...xxxxx Actual ...xxxxx
            Console.WriteLine(cellular_automaton("...........x...........", 142, 10));
            Console.WriteLine(cellular_automaton("xx.x..x.xx..x....x.xxx.xxxx", 127, 5));
            Console.ReadLine();
        }
        static string cellular_automaton(String current, int pattern, int generation)
        {
            setFalse();
            int[] patternNums = getPattern(pattern);
            int index = 0;
            current = current.Replace('.', '_');
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

            String final = "";
            for (int i = 0; i < current.Length; i++)
            {
                String temp = "";
                try
                {
                    temp = current.Substring((i - 1), 3);
                }
                catch (Exception)
                {
                    if (i == 0)
                        temp = current.Last() + current.Substring(i, 2);
                    else
                        temp = current.Substring(i-1, 2) + current.First();
                }
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
            
            //final += current.ElementAt(current.Length-1).ToString();
            //Console.WriteLine(final);
            return cellular_automaton(final, --generation);
        }
        static int[] getPattern(int i)
        {
            int[] num = new int[NUMPATTERNS];
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
