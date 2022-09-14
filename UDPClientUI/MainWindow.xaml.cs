using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UDPClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private readonly Dispatcher _uiDispatcher;
        public struct UdpState
        {
            public Socket u;
            public EndPoint e;
        }

        private UdpState transmitter = new UdpState();

        public MainWindow()
        {
            InitializeComponent();
            _uiDispatcher = Dispatcher.CurrentDispatcher;
        }

        /// <summary>
        /// Does the initial connection and sets up the listener for messages.
        /// The parameters are not used.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(IPPORT.Text) && IPPORT.Text.Contains(':'))
            {
                string IPandPORT = IPPORT.Text;
                Socket soketti = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                string[] IP = IPandPORT.Split(':');
                IPAddress[] addresses = Dns.GetHostAddresses(IP[0]);
                IPEndPoint end = new IPEndPoint(addresses[0], int.Parse(IP[1]));
                EndPoint senderRemote = (EndPoint)end;
                transmitter.e = senderRemote;
                transmitter.u = soketti;



                soketti.SendTo(System.Text.Encoding.ASCII.GetBytes("connecting;"), transmitter.e);
                Task.Factory.StartNew(() => ReceiveMessages(IPandPORT));




            }
        }

        /// <summary>
        /// makes a udp state struck that houses a upd socket and a endpoint
        /// </summary>
        /// <param name="ipandport">string that has the IP and port that the socket will connect to in "IP:port" form</param>
        /// <returns>returns UdpState struck with a socket and a endpoint</returns>
        private UdpState makesocket(string ipandport)
        {
            Socket soketti = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpState udpclient = new UdpState();
            string[] IP = ipandport.Split(':');
            IPAddress[] addresses = Dns.GetHostAddresses(IP[0]);
            IPEndPoint end = new IPEndPoint(addresses[0], int.Parse(IP[1]));
            EndPoint senderRemote = (EndPoint)end;
            udpclient.e = senderRemote;
            udpclient.u = soketti;
            return udpclient;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipandport">String containing the IP and port that is been connected to</param>
        private void ReceiveMessages(string ipandport)
        {
            UdpState udpclient = makesocket(ipandport);
            int byteamount = 0;
            byte[] rec = new byte[3000];
           


            while (true)
            {
                byteamount = transmitter.u.Receive(rec);
                string message = System.Text.Encoding.ASCII.GetString(rec, 0, byteamount);
                updateChat(message);

            }


            //Chat.Items.Add(receiveString);
        }

        private void updateChat(string message)
        {
            if (!_uiDispatcher.CheckAccess())
            {
                _uiDispatcher.BeginInvoke(DispatcherPriority.Normal, () => { updateChat(message); });
                return;
            }
            Chat.Items.Add(message);
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            transmitter.u.SendTo(System.Text.Encoding.ASCII.GetBytes(Message.Text), transmitter.e);
            Message.Clear();
        }
    }
}
