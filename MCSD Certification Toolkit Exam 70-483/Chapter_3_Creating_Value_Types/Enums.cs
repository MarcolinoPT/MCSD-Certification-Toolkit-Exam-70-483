using System;

namespace ConsoleApplication16.Chapter_3_Creating_Value_Types
{
    class Enums
    {
        enum Months { Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sept, Oct, Nov, Dec };

        public void RunMe()
        {
            var name = Enum.GetName(typeof(Months), 8);
            Console.WriteLine("The 8th month in the enum is " + name);
            Console.WriteLine("The underlying values of the Months enum:");
            foreach (int values in Enum.GetValues(typeof(Months)))
            {
                Console.WriteLine(values);
            }
        }
    }
}