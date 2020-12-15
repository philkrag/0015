using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Desktop_Interface
{
    // MASTER
    // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.receive?view=netcore-3.1
    
    
    class EthernetComms
    {

        // ////////////////////////////////////////////////////////////////////////////////////
        // Function:            Inbound UDP Transmissions
        // Version:             1.003
        // Last Modified By:    2020-11-07
        // Last Modified By:    Phillip Kraguljac

        public static string ReceiveUdp()
        {
            string returnValue = "";
            Int16 portAddressInbound = 12001;

            UdpClient receivingUdpClient = new UdpClient(portAddressInbound);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                returnValue = Encoding.ASCII.GetString(receiveBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            receivingUdpClient.Close();
            return returnValue;
        }


        // ////////////////////////////////////////////////////////////////////////////////////
        // Function:            Outbound UDP Transmissions
        // Version:             1.002
        // Last Modified By:    2020-11-07
        // Last Modified By:    Phillip Kraguljac

        public static void sendUdp(string outboundIpAddress, int outboundPortAddress, string dataToBeTransmistted)
        {
            string defaultOutboundIpAddress = "127.0.0.1";
            Int16 defaultOutboundPortAddress = 12000;

            if (outboundIpAddress != null) { outboundIpAddress = defaultOutboundIpAddress; }
            if (outboundPortAddress != null) { outboundPortAddress = defaultOutboundPortAddress; }
            
            UdpClient udpClient = new UdpClient(outboundIpAddress, outboundPortAddress);

            Byte[] sendBytes = Encoding.ASCII.GetBytes(dataToBeTransmistted);
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
