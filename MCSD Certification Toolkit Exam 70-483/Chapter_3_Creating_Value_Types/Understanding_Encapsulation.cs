using System;

namespace ConsoleApplication16.Chapter_3_Creating_Value_Types
{
    class Understanding_Encapsulation
    {
        public class IPAddress
        {
            private int[] ip;
            public int this[int index]
            {
                get
                {
                    return ip[index];
                }
                set
                {
                    if (value == 0 || value == 1)
                        ip[index] = value;
                    else
                        throw new Exception("Invalid value");
                }
            }
            public static void RunMe()
            {
                var myIP = new IPAddress();
                // initialize the IP address to all zeros
                for (int i = 0; i < 32; i++)
                {
                    myIP[i] = 0;
                }
            }
        }
    }
}