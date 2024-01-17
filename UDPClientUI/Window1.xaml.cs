using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Net;

namespace UDPClientUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public struct UdpState
        {
            public Socket u;
            public EndPoint e;
        }

        public Window1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets called when user clicks the login button. handles login in to the chat.
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            /*
            code for a login that will not be used in this assignment
            UdpState udpState = makesocket("192.168.8.100:25000");
            //sql injection risk fix later
            udpState.u.SendTo(System.Text.Encoding.ASCII.GetBytes("Login;"+username.Text+';'+password.Text), udpState.e);
            int byteamount = 0;
            byte[] rec = new byte[3000];
            byteamount = udpState.u.Receive(rec);
            string message = System.Text.Encoding.ASCII.GetString(rec, 0, byteamount);

            if (message.Equals("Login Successful"))
            {
                MainWindow window = new MainWindow(username.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Invalid login");
            }
            */
            MainWindow window = new MainWindow(username.Text);
            Close();
        }
        /// <summary>
        /// Is called when user clicks the Register button
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            /* Registering that will not be used
            UdpState udpState = makesocket("192.168.8.100:25000");
            //sql injection rist fix later
            udpState.u.SendTo(System.Text.Encoding.ASCII.GetBytes("Register;" + username.Text + ';' + password.Text), udpState.e);
            int byteamount = 0;
            byte[] rec = new byte[3000];
            byteamount = udpState.u.Receive(rec);
            string message = System.Text.Encoding.ASCII.GetString(rec, 0, byteamount);
            if (message.Equals("Registeration successful"))
            {
                //MainWindow window = new MainWindow(username.Text);
                //Close();
                MessageBox.Show("Successful registeration");
            }
            else
            {
                MessageBox.Show("Invalid registeration");
            }
            */


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

    }
}
