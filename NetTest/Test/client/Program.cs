using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace client
{
    class Program
    {
        static byte[] dataBuffer = new byte[1024];
        static Thread thread = null;
        static Socket clientSocket = null;
        static void Main(string[] args)
        {
            StartClientSync();
            Console.ReadKey();
        }

        static void StartClientSync()
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.102"), 88));

            int count = clientSocket.Receive(dataBuffer);
            string msg = Encoding.UTF8.GetString(dataBuffer, 0, count);
            Console.Write("来自服务器:" + msg);

            thread = new Thread(recv);
            thread.IsBackground = true;
            thread.Start();
            
            while(true)
            {
                string s = Console.ReadLine();
                //Console.Write(s);
                if(s == "c")
                {
                    clientSocket.Close();
                    return;
                }
                clientSocket.Send(Message.GetBytes(s));
                
                //clientSocket.Send(Encoding.UTF8.GetBytes(s));
            }


            //for(int i = 0; i < 100; i++)
            //{
            //    clientSocket.Send(Message.GetBytes(i.ToString() + "长度"));
            //}

            //string s = @"你好呀！！";
            //clientSocket.Send(Encoding.UTF8.GetBytes(s));

            string ns = Console.ReadLine();
            clientSocket.Send(Encoding.UTF8.GetBytes(ns));

            Console.ReadKey();
            clientSocket.Close();
        }

        static void recv()
        {
            while(true)
            {
                int count = clientSocket.Receive(dataBuffer);
                string msg = Encoding.UTF8.GetString(dataBuffer, 0, count);
                Console.Write("来自服务器:" + msg);
            }
        }
    }
}
