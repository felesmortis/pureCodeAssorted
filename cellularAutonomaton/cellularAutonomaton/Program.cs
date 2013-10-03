using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellularAutonomaton
{
    class Program
    {
        static int NUMPATTERNS = pow2Greater(8);
        static int SIZEPATTERNS = Convert.ToString(NUMPATTERNS - 1, 2).Length;
        static long MAXPATSIZE = (int)Math.Pow(2, NUMPATTERNS);
        struct patVal
        {
            public String pat;
            public int val;
            public bool repX;
        };
        
        static patVal[] patt = new patVal[NUMPATTERNS];
        static int pow2Greater(int num)
        {
            int i = 1;
            while (i < num)
            {
                i = i << 1;
            }
            return i;
        }
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
                    switch (num)
                    {
                        case 0:
                            val = pow2Greater(val);
                            size = Convert.ToString(NUMPATTERNS-1, 2).Length;
                            if (size % 2 != 1)
                            {
                                valid = false;
                                break;
                            }
                            NUMPATTERNS = val;
                            SIZEPATTERNS = size;
                            MAXPATSIZE = (long)Math.Pow(2, NUMPATTERNS);
                            break;
                        case 1:
                            double d = Math.Sqrt(val);
                            n = (int)d;
                            if (d != n)
                            {
                                valid = false;
                                break;
                            }
                            size = Convert.ToString(n - 1, 2).Length;
                            if (size % 2 != 1)
                            {
                                valid = false;
                                break;
                            }
                            NUMPATTERNS = n;
                            SIZEPATTERNS = size;
                            MAXPATSIZE = val;
                            break;
                        case 2:
                            if (val % 2 != 1)
                            {
                                valid = false;
                                break;
                            }
                            NUMPATTERNS = (int)Math.Pow(2, val);
                            MAXPATSIZE = (long)Math.Pow(2, NUMPATTERNS);
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
                    Console.WriteLine("Please enter a pattern (whole positive)number less than " + MAXPATSIZE + ".");
                    if(!long.TryParse(Console.ReadLine(), out pattern))
                        flag = false;
                    if(flag && (pattern < 0 || pattern > MAXPATSIZE))
                        flag = false;
                }while(!flag);
                do
                {
                    Console.WriteLine("Please enter the generations.(whole positive integer)");
                    

                }while(!int.TryParse(Console.ReadLine(), out generation) || generation < 0);
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
            int wings = SIZEPATTERNS / 2;
            for (int i = 0; i < current.Length; i++)
            {
                String temp = "";
                try
                {
                    temp = current.Substring((i - wings), SIZEPATTERNS);
                }
                catch (Exception)
                {
                    int t = current.Substring(i).Length;
                    //TODO: fix iterations
                    bool start = i-wings > 0;
                    bool index = t < wings+ 1;
                    String t1 = current.Substring(start ? i - wings : 0, start ? SIZEPATTERNS - t : wings + 1 );
                    int togo = SIZEPATTERNS - t1.Length;
                    temp = t1 + current.Substring(start ? 0 : current.Length - togo - 1, togo);
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
        static int[] getPattern(long i)
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
