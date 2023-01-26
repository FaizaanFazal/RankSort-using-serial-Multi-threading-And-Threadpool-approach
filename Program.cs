using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ranksort_withMultithreading
{
    class Program
    {
        public static long PendingWorkItemCount { get; set; }
        static int[] arraysizes = { 100, 500, 1000, 5000, 10000, 20000, 50000, 100000 };
        static int[] numbers, sortednumbers;
        static int min, max, size;

        static void Main(string[] args)
        {


            //serial time
            Console.WriteLine("------------------Serial Time-----------------");
            for (int n = 0; n < arraysizes.Length; n++)
            {
                Console.WriteLine("For n = " + arraysizes[n]);

                int[] numbers = new int[arraysizes[n]]; //to store random generated numbers
                int[] sortednumbers = new int[arraysizes[n]];// to store sorted copy
                int temp; //to keep count

                Random rnd = new Random();
                for (int i = 0; i < numbers.Length; i++) //generating random numbers
                {
                    int num = rnd.Next(1, 1000000);//postive random integers between 1 to 1000000
                    numbers[i] = num;
                }



                //------rank Sorting start here-------
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Console.Write("\nApplying RankSort took --> ");
                foreach (int x in numbers)
                {
                    temp = 0;
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        if (x < numbers[i])
                        {
                            temp++;
                        }
                    }

                    try // to cater index outofrange problem
                    {
                        sortednumbers[temp] = x;
                    }
                    catch
                    {

                    }

                }
                timer.Stop();
                Console.WriteLine("\tTime (hh:mm:ss): {0:hh\\:mm\\:ss\\.fff}", timer.Elapsed);


            }



            // --------------------------sorting using multithreading
            Console.WriteLine("------------------Multi Threading using objects-----------------");
            Thread[] t = new Thread[5];
            for (int n = 0; n < arraysizes.Length; n++)
            {

                Console.WriteLine("For n = " + arraysizes[n]);
                Console.Write("RankSort using Multithreading took --> ");
                int[] numbers = new int[arraysizes[n]]; //to store random generated numbers
                int[] sortednumbers = new int[arraysizes[n]];// to store sorted copy

                Random rnd = new Random();
                for (int i = 0; i < numbers.Length; i++) //generating random numbers
                {
                    int num = rnd.Next(1, 1000000);//postive random integers between 1 to 1000000
                    numbers[i] = num;
                }

                int min = 0;
                int max = 0;
                Stopwatch timer = new Stopwatch();
                timer.Start();
                for (int i = 0; i < 5; i++) // creating threads to sort array
                {
                    int parts = numbers.Length / 5;

                    for (int j = 0; j < 5; j = j + parts)
                    {
                        min = j;
                        max = j + parts;
                        Thread myT = new Thread(() => mySort(min, max, numbers, sortednumbers));
                        t[i] = myT;
                        t[i].Start();
                    }

                }
                timer.Stop();
                Console.WriteLine("\tTime (hh:mm:ss): {0:hh\\:mm\\:ss\\.fff}", timer.Elapsed);
                Console.WriteLine("");
            }


            //--------multithreading using pool--------
            Console.WriteLine("------------------Multi Threading using threadPool-----------------");

            for (int n = 0; n < arraysizes.Length; n++)
            {
                Console.WriteLine("--------Started thread pool For n = " + arraysizes[n] + "---------");
                // Console.Write("Multithreading ranksort using Pool took --> ");

                numbers = new int[arraysizes[n]]; //to store random generated numbers
                sortednumbers = new int[arraysizes[n]];// to store sorted copy

                Random rnd = new Random();
                for (int i = 0; i < numbers.Length; i++) //generating random numbers
                {
                    int num = rnd.Next(1, 1000000);//postive random integers between 1 to 1000000
                    numbers[i] = num;
                }

                //-- creating thread 
                min = 0;
                max = 0;
                size = n;

                Stopwatch timer = new Stopwatch();
                timer.Start();
                AutoResetEvent doneEvent = new AutoResetEvent(false);
                for (int i = 0; i < 5; i++)
                {
                    int parts = numbers.Length / 5;

                    for (int j = 0; j < 5; j = j + parts)
                    {
                        min = j;
                        max = j + parts;

                        // List<int[]> myList = new List<int[]>();
                        // myList.Add(new[] { min });
                        // myList.Add(new[] { max });
                        // myList.Add(new[] { n });
                        // myList.Add(numbers);
                        //  myList.Add(sortednumbers);
                        //List<int> list = new List<List<int>>() { min, max, numbers, sortednumbers };
                        // ThreadPool.QueueUserWorkItem(poolingmethod, new object[] {min, max, numbers, sortednumbers}); new object[] { min, max, numbers, sortednumbers }

                        ThreadPool.QueueUserWorkItem(new WaitCallback(poolingmethod), new object[] { numbers, doneEvent });

                    }

                }
                // Wait for the sort to complete.
                doneEvent.WaitOne();
                /* int mex, max2;
                 ThreadPool.GetMaxThreads(out mex, out max2);
                 int available, available2;
                 ThreadPool.GetAvailableThreads(out available, out available2);
                 int runningThread = mex - available;
                // Console.WriteLine("running therads count: " + runningThread);
                 while (runningThread > 0)
                 {
                     ThreadPool.GetAvailableThreads(out available, out available2);
                     runningThread = mex - available;   

                 }*/
                timer.Stop();
                Console.WriteLine("\tTime (hh:mm:ss): {0:hh\\:mm\\:ss\\.fff}", timer.Elapsed);
                Console.WriteLine("");


            }

            Console.ReadKey();
        }

        public static void poolingmethod(object state)
        {
            object[] stateArray = (object[])state;
            AutoResetEvent doneEvent = (AutoResetEvent)stateArray[1];
            // List<int[]> list = (List<int[]>) state;
            // List<int[]> myList = new List<int[]>();
            //int min2 = min;
            //int max2 = max;
            //int n2 = size;

            int temp = 0;
            //  Stopwatch timer = new Stopwatch();
            //timer.Start();

            foreach (int x in numbers)
            {
                temp = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (x < numbers[i])
                    {
                        temp++;
                    }
                }

                try // to cater index outofrange problem
                {
                    sortednumbers[temp] = x;
                }
                catch
                {

                }
            }
            doneEvent.Set();
            //timer.Stop();
            //string timetaken = "For n = " + n + " " + "Time (hh:mm:ss): {0:hh\\:mm\\:ss\\.fff}" + timer.Elapsed;
            //.WriteLine("Multithreading Using Pool took --> For n = " + arraysizes[n] + "\tTime (hh:mm:ss): {0:hh\\:mm\\:ss\\.fff}", timer.Elapsed);
            // Console.WriteLine("");

        }


        public static void mySort(int min, int max, int[] numbers, int[] sortednumbers) //rank sort
        {
            int temp = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            foreach (int x in numbers)
            {
                temp = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (x < numbers[i])
                    {
                        temp++;
                    }
                }

                try // to cater index outofrange problem
                {
                    sortednumbers[temp] = x;
                }
                catch
                {

                }

            }
        }
    }
}

