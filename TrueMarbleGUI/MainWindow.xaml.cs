using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media.Imaging;
using TrueMarbleData;
using System.IO;

namespace TrueMarbleGUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    ITMDataController m_tmData;

    public MainWindow()
    {
      InitializeComponent();



    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      try
      {
        ChannelFactory<ITMDataController> dataFactory;
        String sURL = "net.tcp://localhost:50001/TMData";
        NetTcpBinding tcpBinding = new NetTcpBinding();
        tcpBinding.MaxReceivedMessageSize = System.Int32.MaxValue;
        tcpBinding.ReaderQuotas.MaxArrayLength = System.Int32.MaxValue;
        dataFactory = new ChannelFactory<ITMDataController>(tcpBinding, sURL);
        m_tmData = dataFactory.CreateChannel();
      }catch (Exception ex)
      {
        Console.WriteLine("Something went wrong");
        Console.Write(ex.StackTrace);

      }
    }

    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        int zoom = 4;
        int x = 0;
        int y = 0;
        Console.WriteLine("get 1");
        byte[] tile = m_tmData.LoadTile(zoom, x, y);
        Console.WriteLine("get 2");
        MemoryStream tileStream = new MemoryStream(tile);
        Console.WriteLine("get 3");
        JpegBitmapDecoder decoder = new JpegBitmapDecoder(tileStream, BitmapCreateOptions.None, BitmapCacheOption.None);
        Console.WriteLine("get 4");
        BitmapFrame frame = decoder.Frames[0];
        Console.WriteLine("get 5");
        imgTile.Source = frame;
        Console.WriteLine("get 6");
      }
      catch (FaultException ex)
      {
        Console.WriteLine("A Fault Exception has occured");
        Console.Write(ex.StackTrace);

      }
      catch (CommunicationObjectFaultedException ex)
      {
        Console.WriteLine("Communication with the server has faulted");
        Console.Write(ex.StackTrace);

      }



    }
  }
}
