using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAutoReset
{
    class Simulation
    {
        private int lowerRange = 0;
        private int upperRange = 0;
        private int time = 0;
        private int isSignaling = 0;

        private static AutoResetEvent event_1 = new AutoResetEvent(false);
        private static AutoResetEvent event_2 = new AutoResetEvent(false);


        public void Start()
        {
            do
            { 
                RunScenerios();
                
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        public void RunScenerios()
        {
            Console.Clear();
            Console.Write("Zakres dolny: ");
            lowerRange = Int32.Parse(Console.ReadLine());
            Console.Write("Zakres górny: ");
            upperRange = Int32.Parse(Console.ReadLine());
            Console.Write("Odstęp czasowy(ms): ");
            time = Int32.Parse(Console.ReadLine());
            Console.Write("Sygnalizacja (0/1): ");
            isSignaling = Int32.Parse(Console.ReadLine());


            switch (isSignaling)
            {
                case 0:
                    Console.WriteLine("Bez sygnalizacji.");
                    RunNormal();
                    break;

                case 1:
                    Console.WriteLine("Z sygnalizacją.");
                    RunWithSignaling();
                    break;

                default:
                    Console.WriteLine("Nie ma takiej opcji.");
                    break;
            }
        }

        private void RunNormal()
        {
            int startEven = 0;
            int endEven = 0;
            int startOdd = 0;
            int endOdd = 0;

            PrepareRanges(ref startEven, ref endEven, ref startOdd, ref endOdd);

            //Console.WriteLine("Zakres dla parzystych: {0} - {1}", startEven, endEven);
            //Console.WriteLine("Zakres dla nieparzystych: {0} - {1}", startOdd, endOdd);

            Thread thread1 = new Thread(() => showNormal(startEven, endEven));
            Thread thread2 = new Thread(() => showNormal(startOdd, endOdd));

            if (startEven < startOdd)
            {
                
                thread1.Start();
                thread2.Start();
            }
            else
            {
                thread2.Start();
                thread1.Start();
            }
        }

        private void RunWithSignaling()
        {
            int startEven = 0;
            int endEven = 0;
            int startOdd = 0;
            int endOdd = 0;

            PrepareRanges(ref startEven, ref endEven, ref startOdd, ref endOdd);

            //Console.WriteLine("Zakres dla parzystych: {0} - {1}", startEven, endEven);
            //Console.WriteLine("Zakres dla nieparzystych: {0} - {1}", startOdd, endOdd);

            if (startEven < startOdd)
            {
                Thread thread1 = new Thread(() => ShowEvenAuto(startEven, endEven, true));
                Thread thread2 = new Thread(() => ShowOddAuto(startOdd, endOdd, false));
                thread1.Start();
                thread2.Start();
            }
            else
            {
                Thread thread1 = new Thread(() => ShowEvenAuto(startEven, endEven, false));
                Thread thread2 = new Thread(() => ShowOddAuto(startOdd, endOdd, true));
                thread2.Start();
                thread1.Start();
            }
        }

        private void PrepareRanges(ref int startEven, ref int endEven, ref int startOdd, ref int endOdd)
        {
            if (lowerRange % 2 == 0)
            {
                startEven = lowerRange;
                startOdd = lowerRange + 1;
            }
            else
            {
                startEven = lowerRange + 1;
                startOdd = lowerRange;
            }

            if (upperRange % 2 == 0)
            {
                endEven = upperRange;
                endOdd = upperRange - 1;
            }
            else
            {
                endEven = upperRange - 1;
                endOdd = upperRange;
            }
        }

        private void showNormal(int start, int end)
        {
            for (int i = start; i <= end; i += 2)
            {
                Console.WriteLine(i);
                Thread.Sleep(time);
            }
        }

        private void ShowEvenAuto(int start, int end, bool isFirst)
        {
            
           for (int i = start; i <= end; i += 2)
            {
                if (!isFirst && i == start)
                {
                    event_1.WaitOne();
                }
                Console.WriteLine(i);
                Thread.Sleep(time);

                event_2.Set();
                
                if (i != end)
                {
                    event_1.WaitOne();
                }
                
                
            }

        }

        private void ShowOddAuto(int start, int end, bool isFirst)
        {

            for (int i = start; i <= end; i += 2)
            {
                if (!isFirst && i == start)
                {
                    event_2.WaitOne();
                }

                Console.WriteLine(i);
                Thread.Sleep(time);

                event_1.Set();
                
                if (i != end)
                {
                    event_2.WaitOne();
                }
            }
        }
    }
}
