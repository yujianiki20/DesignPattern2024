using System.IO;
using System;

namespace IoTest {
    class Program
    {
        static void Main()
        {
            Stream stream = new FileStream("file.txt", FileMode.Open);
            // TextReader
            TextReader streamReader = new StreamReader(stream); // StreamReaderはStreamを継承していない // adapterパターン
            Stream memoryStream = new MemoryStream(new byte[] { 0x31, 0x32, 0x33 }); // メモリを扱うストリーム
            Stream bufferedStream = new BufferedStream(memoryStream);

            Console.WriteLine("streamReader");
            Console.WriteLine(streamReader.ReadToEnd());
            var st = new char[3];
            streamReader.ReadBlock(st, 0, 3);
            Console.WriteLine(new string(st));
            Console.WriteLine(streamReader.ReadToEnd());
            streamReader.Close();
            //textリーダーは読み方のバリエーション
            // StringReader




            // PrintBytes(stream);
            // stream.Close();

            PrintBytes(bufferedStream);
            bufferedStream.Close();

            // PrintBytes(memoryStream);
            // memoryStream.Close();
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
