using System;
using System.Diagnostics;

namespace Resolver.Web
{
    public class serve_angular
    {
        public static void Main(string[] args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.Start();

            cmd.StandardInput.WriteLine($"ng serve --configuration={Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower()}");
            cmd.StandardInput.Flush();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }
}
