namespace ConsoleApplication16.Chapter_4_Using_Types
{
    using System;
    using System.Globalization;
    using System.Threading;

    public class Converting_Between_Types
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.OverflowException"></exception>
        public static void ConvertIntegerToShort()
        {
            var a = Convert.ToInt16(int.MaxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.OverflowException"></exception>
        public static void ConvertIntegerToShortCheckedCode()
        {
            checked
            {
                const int big = 1000000;
                //var small = (short)big;
            }
        }

        public static void ConvertIntegerToShortUncheckedCode()
        {
            unchecked
            {
                const int big = 1000000;
                var small = (short)big;
            }
        }

        public static void BitConverterBytes()
        {
            var packedValue = int.MaxValue;
            var valueBytes = BitConverter.GetBytes(packedValue);
            short value1, value2;
            value1 = BitConverter.ToInt16(valueBytes, 0);
            value2 = BitConverter.ToInt16(valueBytes, 2);
        }

        public static void HandlingDynamicTypes()
        {
            // Make an array of numbers.
            int[] array1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            // This doesn't work because array1.Clone is an object.
            //int[] array2 = array1.Clone();
            // This works.
            int[] array3 = (int[])array1.Clone();
            array3[5] = 55;
            // This also works.
            dynamic array4 = array1.Clone();
            array4[6] = 66;
            array4[7] = "This won't work";
        }

        public static void Currency()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
            Console.WriteLine("{0:C4}", 163);
        }
    }
}