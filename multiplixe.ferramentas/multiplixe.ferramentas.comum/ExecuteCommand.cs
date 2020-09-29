using multiplixe.ferramentas.comum.commands;
using System;
using System.Diagnostics;

namespace multiplixe.ferramentas.comum
{
    public class ExecuteCommand
    {
        public static void Execute(Command command)
        {
            try
            {
                using (var process = new Process())
                {
                    //process.StartInfo.FileName = dotnet; // pack C:\inetpub\wwwroot\versiontest\versiontest\versiontest.csproj"; // --version-suffix ci1234 --output C:\inetpub\wwwroot\tronar.gamificacao\bin";
                    //process.StartInfo.Arguments = @"pack C:\inetpub\wwwroot\versiontest\versiontest\versiontest.csproj --version-suffix ci1234 --output C:\inetpub\wwwroot\tronar.gamificacao\bin";

                    process.StartInfo.FileName = @"cmd.exe";
                    process.StartInfo.Arguments = command.ToString();
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.OutputDataReceived += (sender, data) => Log.Write(data.Data);
                    process.ErrorDataReceived += (sender, data) =>
                    {
                        Log.Write("****************************************************************");
                        Log.Write("ERRO");
                        Log.Write("****************************************************************");
                        Log.Write(data.Data);
                        Log.Write("****************************************************************");
                    };

                    Log.Write(string.Format("COMANDO: {0}", command.ToString()));

                    process.Start();

                    string result = process.StandardOutput.ReadToEnd();
                    string resultError = process.StandardError.ReadToEnd();

                    Log.Write(result);

                    if (!string.IsNullOrEmpty(resultError))
                    {
                        Log.Write("****************************************************************");
                        Log.Write(">> ERRO");
                        Log.Write("****************************************************************");
                        Log.Write(resultError);
                        Log.Write("****************************************************************");

                        throw new Exception($"{command.Name} -->> {resultError}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0}", ex.Message);
            }
        }
    }
}

