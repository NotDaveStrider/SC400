using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TrueMarbleData
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        ServiceHost host;

        NetTcpBinding tcpBinding = new NetTcpBinding();
        tcpBinding.MaxReceivedMessageSize = System.Int32.MaxValue;
        tcpBinding.ReaderQuotas.MaxArrayLength = System.Int32.MaxValue;
        String sURL = "net.tcp://localhost:50001/TMData";
        host = new ServiceHost(typeof(TMDataControllerImpl));
        host.AddServiceEndpoint(typeof(ITMDataController), tcpBinding, sURL);

        host.Open();
        System.Console.WriteLine("Press Enter to exit");
        System.Console.ReadLine();

        host.Close();

      }catch (FaultException ex)
      {
        Console.WriteLine("A Fault Exception occured");
        Console.WriteLine(ex.StackTrace);

      }


    }
  }
}
