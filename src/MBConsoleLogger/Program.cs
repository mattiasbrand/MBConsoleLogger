using System;
using System.Net;
using System.Net.Sockets;
using log4net;

namespace MBConsoleLogger
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main()
        {
            var port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"]);

            try
            {
                Console.WriteLine("On which port do you want to listen to log messages? (default is " + port + ")");
                var userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput) == false) port = int.Parse(userInput);

                var sender = new IPEndPoint(IPAddress.Any, 0);
                var client = new UdpClient(port);
                Log.Info("Listening to port: " + port);

                while (true)
                {
                    var buffer = client.Receive(ref sender);
                    var logLine = System.Text.Encoding.Default.GetString(buffer);

                    if (logLine.IndexOf("{INFO}", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Log.Info(logLine.Replace("{INFO}", "[I] "));
                    }
                    else if (logLine.IndexOf("{DEBUG}", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Log.Debug(logLine.Replace("{DEBUG}", "[D] "));
                    }
                    else if (logLine.IndexOf("{ERROR}", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Log.Error(logLine.Replace("{ERROR}", "[E] "));
                    }
                    else if (logLine.IndexOf("{WARN}", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Log.Warn(logLine.Replace("{WARN}", "[W] "));
                    }
                    else
                    {
                        Log.Warn(logLine);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(Environment.NewLine + "Press any key to close...");
                Console.ReadLine();
            }
        }
    }
}
