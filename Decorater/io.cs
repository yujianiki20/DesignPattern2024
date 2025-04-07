using System.IO;
using System;

namespace IoTest {
    class Program
    {
        static void Main()
        {
            Stream stream = new FileStream("file.txt", FileMode.Open);
            //Stream streamReader = new StreamReader(stream);
            //Stream bufferedStream = new BufferedStream(stream);


            PrintBytes(stream);
            stream.Close(); // 忘れずに！
        }

        static void PrintBytes(Stream stream)
        {
            int b;
            while ((b = stream.ReadByte()) != -1)
            {
                Console.Write((char)b);
            }
            Console.WriteLine("\n---");
        }
    }
}
