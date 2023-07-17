﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Common;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();

        public Client() { }
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        public void Start()
        {
            if(clientSocket == null || clientSocket.Connected == false) return;
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if(clientSocket == null || clientSocket.Connected == false) return;
                int count = clientSocket.EndReceive(ar);
                if(count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count, OnProcessMessage);
                Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        private void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        private void Close()
        {
            if(clientSocket != null)
                clientSocket.Close();
            server.RemoveClient(this);
        }

        public void Send(ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }
    }
}
