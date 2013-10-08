using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringExtention;

namespace cellularAutonomaton
{
    class Program
    {
        static mainProps props = new mainProps();
        struct patVal
        {
            public String pat;
            public int val;
            public bool repX;
        };
        
        static patVal[] patt = new patVal[props.NUMPATTERNS];
        static void init()
        {
            patt = new patVal[props.NUMPATTERNS];
            //int i = 1;
            String[] list = new String[props.NUMPATTERNS];
            for (int i = 0; i < props.NUMPATTERNS; i++)
            {
                String temp = Convert.ToString(i, 2).Replace('0', '.').Replace('1', 'x');
                int n = temp.Length;

                if (n < props.SIZEPATTERNS)
                {
                    for (int l = n; l < props.SIZEPATTERNS; l++)
                        temp = '.' + temp;
                }
                list[i] = temp;
                //Console.WriteLine(temp);
            }
            int pow = 1;
            for (int i = 0; i < props.NUMPATTERNS; i++)
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
            //Console.WriteLine((9+(-1) % (8)));
            //Console.ReadLine();
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
            user();
        }
        static void user()
        {
            Console.WriteLine("Would you like to enter your own pattern?(Y/N)");
            while (Console.ReadLine().ToUpper() == "Y")
            {
                Console.WriteLine("Would you like to specify your own pattern?(Y/N)");
                bool valid = false;
                String patYN = Console.ReadLine().ToUpper();
                while (patYN == "Y" && !valid)
                {
                    valid = true;
                    int num = -1;
                    int val = 0;
                    do
                    {
                        Console.WriteLine("Would you like to specify the Minimum Number of Patterns(0), the Greatest Pattern number(1), or the Length of the patterns(2)?");
                        
                    } while (!int.TryParse(Console.ReadLine(), out num) && num >= 0 && num < 3);
                    do
                    {
                        Console.WriteLine("Number?");

                    } while (!int.TryParse(Console.ReadLine(), out val) && val > 0);
                    int size;
                    int n;
                    /*
                    TODO: Add a class to store numpatterns, sizepatterns and maxpatsize
                    so they can dynamically change with properties when the others change.
                    */
                    switch (num)
                    {
                        case 0:
                            try
                            {
                                props.NUMPATTERNS = val;
                            }
                            catch (Exception ex)
                            {
                                valid = false;
                            }
                            break;
                        case 1:
                            try
                            {
                                props.MAXPATSIZE = val;
                            }
                            catch (Exception ex)
                            {
                                valid = false;
                            }
                            break;
                        case 2:
                            try
                            {
                                props.SIZEPATTERNS = val;
                            }
                            catch (Exception ex)
                            {
                                valid = false;
                            }
                            break;
                        default:
                            Console.WriteLine("Clean up your code, it didn't work");
                            break;

                    }
                    if (!valid)
                    {
                        Console.WriteLine("Would you like to specify your own pattern?(Y/N)");
                        patYN = Console.ReadLine().ToUpper();
                    }
                }
                String str = "";
                long pattern = 0;
                int generation = 0;
                bool flag = true;
                do
                {
                    Console.WriteLine("Please enter a string of '.' and 'x'.");
                    str = Console.ReadLine();
                } while (!(str.Contains('.') || str.Contains('x')));
                do
                {
                    flag = true;
                    Console.WriteLine("Please enter a pattern (whole positive)number less than " + props.MAXPATSIZE + ".");
                    if(!long.TryParse(Console.ReadLine(), out pattern))
                        flag = false;
                    if (flag && (pattern < 0 || pattern > props.MAXPATSIZE))
                        flag = false;
                }while(!flag);
                do
                {
                    Console.WriteLine("Please enter the generations.(whole positive integer)");
                    

                }while(!int.TryParse(Console.ReadLine(), out generation) || generation < 0);
                init();
                Console.WriteLine(cellular_automaton(str, pattern, generation));
                Console.WriteLine("Again?(Y/N)");
            }
        }
        static string cellular_automaton(String current, long pattern, int generation)
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

            String final = "";
            int wings = props.SIZEPATTERNS / 2;
            for (int i = 0; i < current.Length; i++)
            {
                string temp = current.SubstringLoop(i - wings, props.SIZEPATTERNS);
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
        static int[] getPattern(long i)
        {
            int[] num = new int[props.NUMPATTERNS];
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


    //*******************************************************************************
    class mainProps
    {
        private int numPatterns;
        private int sizePatterns;
        private long maxPatSize;
        public mainProps()
        {
            numPatterns = 8;
            sizePatterns = 3;
            maxPatSize = 256;
        }
        static int pow2Greater(int num)
        {
            int i = 1;
            while (i < num)
            {
                i = i << 1;
            }
            return i;
        }
        public int NUMPATTERNS
        {
            get
            {
                return numPatterns;
            }
            set
            {
                int val = pow2Greater(value);
                int size = Convert.ToString(NUMPATTERNS - 1, 2).Length;
                if (size % 2 == 1)
                {
                    numPatterns = val;
                    sizePatterns = size;
                    maxPatSize = (long)Math.Pow(2, NUMPATTERNS);
                }
                else throw new Exception("Number of patterns does not correspond to an odd integer value");
            }
        }
        public int SIZEPATTERNS
        {
            get
            {
                return sizePatterns;
            }
            set
            {
                if (value % 2 == 1)
                {
                    sizePatterns = value;
                    numPatterns = (int)Math.Pow(2, value);
                    maxPatSize = (long)Math.Pow(2, numPatterns);
                }
                else throw new Exception();
                
            }
        }
        public long MAXPATSIZE
        {
            get
            {
                return maxPatSize;
            }
            set
            {
                double d = Math.Sqrt(value);
                int n = (int)d;
                if (d == n)
                {
                    int size = Convert.ToString(n - 1, 2).Length;
                    if (size % 2 == 1)
                    {
                        numPatterns = n;
                        sizePatterns = size;
                        maxPatSize = value;
                    }
                    else throw new Exception();

                }
                else throw new Exception();
                
            }
        }
    }
}
namespace StringExtention
{
    public static class StrLoop
    {
        public static string SubstringLoop(this String ring, int startIndex)
        {
            return SubstringLoop(ring, startIndex, ring.Length - startIndex);
        }
        public static string SubstringLoop(this String ring, int startIndex, int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += ring.ElementAt(getIndex(ring, startIndex + i));
            }
            return result;
        }
        private static int getIndex(this String ring, int i)
        {
            if (i < 0)
            {
                return ring.Length - (Math.Abs(i) % ring.Length);
            }
            return ((i) % (ring.Length));
        }
    }
}
