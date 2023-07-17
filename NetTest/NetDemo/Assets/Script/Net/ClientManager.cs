using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using Common;

public class ClientManager : BaseManager
{
    private const string IP = "127.0.0.1";
    private const int PORT = 86;
    private Socket clientSocket;
    private Message msg = new Message();

    public ClientManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        base.OnInit();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
        }catch(Exception e)
        {
            Debug.LogWarning("无法连接到服务器端！" + e);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }catch(Exception e)
        {
            Debug.LogWarning("无法关闭到服务器端的连接！" + e);
        }
    }

    private void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int count = clientSocket.EndReceive(ar);
            Start();
            msg.ReadMessage(count, onProcessDataCallback);
        }catch(Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void onProcessDataCallback(ActionCode actionCode, string data)
    {
        facade.HandleRequest(actionCode, data);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }
}
