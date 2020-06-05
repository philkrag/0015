using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Device_Interfacer
{
    class Program
    {

        
        static string Incoming_String;
        static int X_Axis_Movement;
        static int Y_Axis_Movement;
        static int Z_Axis_Movement;

        static string Controller_IP_Address = "127.0.0.1";
        static string Broadcast_IP_Address = "127.0.0.1";



        static bool Upload_Array = false;
        //static var data;
        //string[] stringArray = new string[200];
        static string[,] XZ_Movement_Format = new string[101,101];


        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 5);

            int CSV_X_Index = 0;

            if (!Upload_Array)
            {
                string filePath = @"C:\TEMP\XZ_Movement_Format.csv";
                StreamReader sr = new StreamReader(filePath);                
                while (!sr.EndOfStream)
                {                    
                    string[] Line = sr.ReadLine().Split(',');
                    for (int CSV_Z_Index = 0; CSV_Z_Index < Line.Length; CSV_Z_Index++) {
                        XZ_Movement_Format[CSV_X_Index, CSV_Z_Index] = Line[CSV_Z_Index];
                    }
                    CSV_X_Index++;
                }
                Console.WriteLine("Upload Completed...");
                Upload_Array = true;
            }







            var data = new byte[7000];
            IPEndPoint ServerEndPoint = new IPEndPoint(IPAddress.Any, 9050);
            Socket WinSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            WinSocket.Bind(ServerEndPoint);
            
            do
            {                
                Console.WriteLine("Waiting for client...");
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                EndPoint Remote = (EndPoint)(sender);
                int recv = WinSocket.ReceiveFrom(data, ref Remote);
                Console.Write("Message received from {0}: ", Remote.ToString());

                Incoming_String = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(Incoming_String);

                Test_Procedure(Incoming_String);

            } while (true);


            









        }


        static bool Obsticle_Detect_090 = false;


        static void Test_Procedure(string Incoming_String)
        {
            
            if (Incoming_String.Contains(";"))
            {
                string[] Incoming_String_Array = Incoming_String.Split(';');

                for (int i = 0; i < Incoming_String_Array.Length; i++)
                {

                    string[] Item_Split = Incoming_String_Array[i].Split(':');

                    switch (Item_Split[0])
                    {
                        case "X-AXIS_MOV":
                            X_Axis_Movement = Convert.ToInt32(Item_Split[1]);
                            break;
                        case "Y-AXIS_MOV":
                            Y_Axis_Movement = Convert.ToInt32(Item_Split[1]);
                            break;
                        case "Z-AXIS_MOV":
                            Z_Axis_Movement = Convert.ToInt32(Item_Split[1]);
                            break;
                        case "S090":
                            if (Convert.ToInt32(Item_Split[1]) == 0)
                            {
                                Obsticle_Detect_090 = false;
                                Console.WriteLine("iii:"+ Item_Split[1]);
                            }
                            else
                            {
                                Obsticle_Detect_090 = true;
                                Console.WriteLine("iid:" + Item_Split[1]);
                            }


                            break;


                        default:
                            
                            break;
                    }

                }



                int X_Adjustment = -X_Axis_Movement + 50;
                int Z_Adjustment = Z_Axis_Movement + 50;
                

                int M090D010_Temp = 0;
                int M270D010_Temp = 0;







                if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("SPN") &&
                   (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("RGH")))
                {
                    M090D010_Temp = Z_Adjustment - 50;
                    M270D010_Temp = -(Z_Adjustment - 50);
                }

                if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("SPN") &&
                    (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("LFT")))
                {
                    M090D010_Temp = Z_Adjustment - 50;
                    M270D010_Temp = -(Z_Adjustment - 50);                    
                }
                                             

                if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("FWD") &&
                    XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("LFT") &&
                    Obsticle_Detect_090==false
                    )
                {
                    M090D010_Temp = (X_Adjustment - 50);
                    M270D010_Temp = (X_Adjustment-50)-(Z_Adjustment-50);
                }

                if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("FWD") &&
                    XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("RGH") &&
                    Obsticle_Detect_090 == false
                    )
                {
                    M090D010_Temp = (X_Adjustment - 50) + (Z_Adjustment - 50);
                    M270D010_Temp = (X_Adjustment - 50);
                }

                if (Obsticle_Detect_090 == true)
                {
                    M090D010_Temp = 0;
                    M270D010_Temp = 0;
                    Console.WriteLine("tttt:");
                }


                    if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("REV") &&
                    XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("LFT"))
                {
                    M090D010_Temp = (X_Adjustment - 50) + (Z_Adjustment - 50);
                    M270D010_Temp = (X_Adjustment - 50);
                }

                if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("REV") &&
                    XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("RGH"))
                {
                    
                    M090D010_Temp = (X_Adjustment - 50);
                    M270D010_Temp = (X_Adjustment - 50) - (Z_Adjustment - 50);
                }



                if (XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("FWD") &&
                    !XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("LFT") &&
                    !XZ_Movement_Format[X_Adjustment, Z_Adjustment].Contains("RGH"))
                {
                    M090D010_Temp = (X_Adjustment - 50);
                    M270D010_Temp = (X_Adjustment - 50);
                }






                string String_To_Send =
                    "M090D010:" + M090D010_Temp +
                    ";M270D010:" + M270D010_Temp +
                    ";";



                Send_UDP(String_To_Send);
                Console.WriteLine("Message Sent: "+String_To_Send);
                Console.WriteLine("TEMP=>" + XZ_Movement_Format[X_Adjustment, Z_Adjustment]);


            }
            else
            {
                Console.WriteLine(Incoming_String);

            }

        }



        static void Send_UDP(string Data_To_Send)
        {
            var data = new byte[7000];
            //IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9051);
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(Controller_IP_Address), 9051);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            data = Encoding.ASCII.GetBytes(Data_To_Send);
            server.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
        }




        private void Calculate_M090D010()
        {



        }





        //private int Invert_Value(int set_value, int max_value)
        //{
        //    int Return_Value = 0;



        //    return Return_Value;
        //}












    }
}
