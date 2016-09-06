namespace ConsoleApplication16.Chapter_9_Working_with_Data
{
    using System.Diagnostics;
    using System.IO;

    public class Performing_IO_Opertations
    {
        public class StringReaderExample
        {
            public static void Example1()
            {
                StringReader stringReader = new StringReader("Hello\nGoodbye");
                int pos = stringReader.Read();
                while (pos != -1)
                {
                    Debug.WriteLine("{0}", (char)pos);
                    pos = stringReader.Read();
                }
                stringReader.Close();
            }
        }

        public class StreamWriterExample
        {
            public static void Example1()
            {
                StreamWriter streamWriter = new StreamWriter("StreamWriter.txt");
                streamWriter.WriteLine("ABC");
                streamWriter.Write(true);
                streamWriter.Write(1);
                streamWriter.Close();
            }
        }

        public class BinaryWriterExample
        {
            public static void Example1()
            {
                FileStream fileStream = new FileStream("BinaryWriter.txt", FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write("ABC");
                binaryWriter.Write(true);
                binaryWriter.Write(1);
                binaryWriter.Close();
            }
        }

        public class BinaryReaderExample
        {
            public static void Example1()
            {
                FileStream fileStream = new FileStream(@"c:\Chapter9Samples\BinaryWriter.txt", FileMode.Open);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                string abs = binaryReader.ReadString();
                bool b = binaryReader.ReadBoolean();
                int i = binaryReader.ReadInt32();
                binaryReader.Close();
            }
        }
    }
}