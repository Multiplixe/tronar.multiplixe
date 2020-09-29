using System;
using System.IO;

namespace multiplixe.ferramentas.comum
{
    public  class Log
    {
        private static string path = @"C:\inetpub\wwwroot\tronar.gamificacao\multipixel.ferramentas\Log.txt";
        public static void Reset()
        {
            File.WriteAllText(path, string.Empty);
        }

        public static void Write(string text)
        {
            Console.WriteLine(text);

            using (var sw = File.AppendText(path))
            {
                sw.WriteLine(text);
            }

        }
    }
}
