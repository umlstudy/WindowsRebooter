using System.Net;
using System.ServiceProcess;
using System.Net.Sockets;
using System;
using log4net;
using System.Threading;
using System.Text;
using System.Diagnostics;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private static ILog log = LogManager.GetLogger("Service1");

        private Boolean running;

        public Service1()
        {
            InitializeComponent();
            running = true;
        }

        protected override void OnStart(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();
        }

        void Run()
        {
            try
            {
                RunReal();
            }
            catch (Exception ex)
            {
                log.Debug("error", ex);
            }
        }

        void RunReal()
        {
            log.Debug("Create udp Socket...");
            
            IPEndPoint ipEndPoing = new IPEndPoint(IPAddress.Any, 9050);
            Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(ipEndPoing);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);

            byte[] data = new byte[1024];
            int recv;

            while (running)
            {
                data = new byte[1024];
                recv = udpSocket.ReceiveFrom(data, ref Remote);

                log.Debug(String.Format("Message received from {0}:", Remote.ToString()));

                string recvString = Encoding.ASCII.GetString(data, 0, recv);
                log.Debug(recvString);

                // udpSocket.SendTo(data, recv, SocketFlags.None, Remote);

                if (recvString.StartsWith("rebootnow!!") )
                {
                    log.Debug("shutdown cmd run...");
                    runCommand("shutdown -f -r -t 0");

                    //System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    //startInfo.FileName = "cmd.exe";
                    //startInfo.Arguments = "/C shutdown -f -r -t 0";
                    //process.StartInfo = startInfo;
                    //process.Start();
                }
            }
        }

        static void runCommand(string cmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c " + cmd;
            process.StartInfo.UseShellExecute = false;
            // process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            log.Debug(outLine.Data);
        }

        protected override void OnStop()
        {
            running = false;
        }
    }
}
