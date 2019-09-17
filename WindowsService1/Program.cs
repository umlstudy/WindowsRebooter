using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WindowsService1
{

    static class Program
    {

        private static ILog log = LogManager.GetLogger("Program");

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        static void Main()
        {
            log.Info("MyService starting..");

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new Service1() 
			};

            ServiceBase.Run(ServicesToRun);
            log.Info("MyService started..");
        }
    }
}
