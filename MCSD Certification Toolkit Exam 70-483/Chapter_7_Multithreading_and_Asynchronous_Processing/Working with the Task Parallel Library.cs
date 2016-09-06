namespace ConsoleApplication16.Chapter_7_Multithreading_and_Asynchronous_Processing
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Working_with_the_Task_Parallel_Library
    {
        public class Working_with_Continuations
        {
            static void Step1()
            {
                Console.WriteLine("Step1");
            }
            static void Step2()
            {
                Console.WriteLine("Step2");
            }
            static void Step3()
            {
                Console.WriteLine("Step3");
            }

            // The result of running the previous code is unpredictable, meaning that the methods can be run in
            // any order, but considering the independent nature of the steps, it shouldn’t matter.
            public static void Example_1_AllStepsIndependent()
            {
                Parallel.Invoke(Step1, Step2, Step3);
            }

            /* The only guarantee you have by running this is that  Step3 runs after Step1 . Nothing can be said
             * about when  Step2 will be executed.The last line of code  Task.WaitAll(step2Task, step3Task);
             * guarantees that you are waiting to collect the results.Without it the Main method just returns, and
             * the application might not get a chance to run the tasks.You don’t need to wait for  Step1 because
             * Step3 starts only after  Step1 finishes.
             */
            public static void Example_2_Step3_After_Step1_Finishes()
            {
                var step1Task = Task.Run(() => Step1());
                var step2Task = Task.Run(() => Step2());
                var step3Task = step1Task.ContinueWith((previousTask) => Step3());
                Task.WaitAll(step2Task, step3Task);
            }

            /* For this call  ContinueWhenAll that takes as a first parameter an array of tasks, and as a second 
             * parameter a delegate to run when all the tasks finish. It returns a new task, which you can use to wait for all 
             * the tasks to complete. The delegate takes as in parameter the array of tasks it was waiting for.
            */
            public static void Example_3_Step3_After_Step1_and_Step2_Finish()
            {
                var step1Task = Task.Run(() => Step1());
                var step2Task = Task.Run(() => Step2());
                var step3Task = Task.Factory.ContinueWhenAll(new Task[] { step1Task, step2Task }, (previousTasks) => Step3());
                step3Task.Wait();
            }

            /* By calling  ContinueWhenAny , you create a task that runs the delegate after any task from the list 
             * completes. The delegate takes as a parameter the completed task. If the completed task returns 
             * something, you can get that value from the  previousTask.Result property. This scenario is quite
             * common when you have some redundant services and you care only about the value retrieved by
             * the quickest one.
             */
            public static void Example_4_Step3_After_Step1_or_Step2_Finish()
            {
                var step1Task = Task.Run(() => Step1());
                var step2Task = Task.Run(() => Step2());
                var step3Task = Task.Factory.ContinueWhenAny(new Task[] { step1Task, step2Task }, (previousTasks) => Step3());
                step3Task.Wait();
            }
        }

        public class Programming_Ansychronous_Applications
        {
            static double ReadDataFromIO()
            {
                // We are simulating an I/O by putting the current thread to sleep.
                Thread.Sleep(2000);
                return 10d;
            }

            static Task<double> ReadDataFromIOAsync()
            {
                return Task.Run(new Func<double>(ReadDataFromIO));
            }

            private static async Task GetDataAsync()
            {
                var task1 = ReadDataFromIOAsync();
                var task2 = ReadDataFromIOAsync();
                // Here we can do more processing
                // that doesn't need the data from the previous calls.
                // Now we need the data so we have to wait
                await Task.WhenAll(task1, task2);
                Console.WriteLine(task1.Result);
                Console.WriteLine(task2.Result);
            }

            public static void RunGetDataAsync()
            {
                GetDataAsync();
            }
        }

        public class Synchronizing_Resources
        {
            static double ReadDataFromIO()
            {
                // We are simulating an I/O by putting the current thread to sleep.
                Thread.Sleep(2000);
                return 10d;
            }

            static double DoIntensiveCalculations()
            {
                // We are simulating intensive calculations
                // by doing nonsens divisions and multiplications
                double result = 10000d;
                var maxValue = Int32.MaxValue >> 4;
                for (int i = 1; i < maxValue; i++)
                {
                    if (i % 2 == 0)
                    {
                        result /= i;
                    }
                    else
                    {
                        result *= i;
                    }
                }
                return result;
            }

            public static void RunInThreadPoolWithEvents()
            {
                double result = 0d;
                // We use this event to signal when the thread is don executing.
                EventWaitHandle calculationDone = new EventWaitHandle(false, EventResetMode.ManualReset);
                // Create a work item to read from I/O
                ThreadPool.QueueUserWorkItem((x) =>
                {
                    result += ReadDataFromIO();
                    calculationDone.Set();
                });
                // Save the result of the calculation into another variable
                double result2 = DoIntensiveCalculations();
                // Wait for the thread to finish
                calculationDone.WaitOne();
                // Calculate the end result
                result += result2;
                // Print the result
                Console.WriteLine("The result is {0}", result);
            }

            public static void RunBarrierSimpleProgram()
            {
                var participants = 5;
                Barrier barrier = new Barrier(participants + 1,
                // We add one for the main thread.
                b =>
                { // This method is only called when all the paricipants arrived.
                    Console.WriteLine("{0} paricipants are at rendez-vous point {1}.",
                    b.ParticipantCount - 1, // We substract the main thread.
                    b.CurrentPhaseNumber);
                });
                for (int i = 0; i < participants; i++)
                {
                    var localCopy = i;
                    Task.Run(() =>
                    {
                        Console.WriteLine("Task {0} left point A!", localCopy);
                        Thread.Sleep(1000 * localCopy + 1); // Do some "work"
                        if (localCopy % 2 == 0)
                        {
                            Console.WriteLine("Task {0} arrived at point B!", localCopy);
                            barrier.SignalAndWait();
                        }
                        else
                        {
                            Console.WriteLine("Task {0} changed its mind and went back!", localCopy);
                            barrier.RemoveParticipant();
                            return;
                        }
                        Thread.Sleep(1000 * (participants - localCopy)); // Do some "more work"
                        Console.WriteLine("Task {0} arrived at point C!", localCopy);
                        barrier.SignalAndWait();
                    });
                }
                Console.WriteLine("Main thread is waiting for {0} tasks!", barrier.ParticipantCount - 1);
                barrier.SignalAndWait(); // Waiting at the first phase
                barrier.SignalAndWait(); // Waiting at the second phase
                Console.WriteLine("Main thread is done!");
            }
        }

        public class Working_with_Cancellations
        {
            public static void RunExample()
            {
                var participants = 5;
                // We create a CancellationTokenSource to be able to initiate the cancellation
                var tokenSource = new CancellationTokenSource();
                // We create a barrier object to use it for the rendez-vous points
                var barrier = new Barrier(participants,
                b =>
                {
                    Console.WriteLine("{0} paricipants are at rendez-vous point {1}.", b.ParticipantCount, b.CurrentPhaseNumber);
                });
                for (int i = 0; i < participants; i++)
                {
                    var localCopy = i;
                    Task.Run(() =>
                    {
                        Console.WriteLine("Task {0} left point A!", localCopy);
                        Thread.Sleep(1000 * localCopy + 1); // Do some "work"
                        if (localCopy % 2 == 0)
                        {
                            Console.WriteLine("Task {0} arrived at point B!", localCopy);
                            barrier.SignalAndWait(tokenSource.Token);
                        }
                        else
                        {
                            Console.WriteLine("Task {0} changed its mind and went back!",
                            localCopy);
                            barrier.RemoveParticipant();
                            return;
                        }
                        Thread.Sleep(1000 * localCopy + 1);
                        Console.WriteLine("Task {0} arrived at point C!", localCopy);
                        barrier.SignalAndWait(tokenSource.Token);
                    });
                }
                Console.WriteLine("Main thread is waiting for {0} tasks!",
                barrier.ParticipantsRemaining - 1);
                Console.WriteLine("Press enter to cancel!");
                Console.ReadLine();
                if (barrier.CurrentPhaseNumber < 2)
                {
                    tokenSource.Cancel();
                    Console.WriteLine("We canceled the operation!");
                }
                else
                {
                    Console.WriteLine("Too late to cancel!");
                }
                Console.WriteLine("Main thread is done!");
            }
        }
    }
}