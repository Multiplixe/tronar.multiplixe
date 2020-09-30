using System;
using System.IO;

namespace multiplixe.ferramentas.comum
{
    public  class Log
    {
        private static string path = @"C:\logs\multiplixe";
        public static void Reset()
        {
            File.WriteAllText(path, string.Empty);
        }

        public static void Write(string text)
        {
            Console.WriteLine(text);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var sw = File.AppendText(Path.Combine(path, "publicador-log.txt")))
            {
                sw.WriteLine(text);
            }

        }
    }
}
