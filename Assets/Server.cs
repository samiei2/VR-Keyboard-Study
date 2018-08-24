using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//using System.Threading.Tasks;
using UnityEngine;

public class Server : MonoBehaviour {
    //public int port = 5000;
    //private readonly TcpListener _listener;
    //private CancellationTokenSource _tokenSource;
    //private bool _listening;
    //private CancellationToken _token;
    //public bool Listening {get { _listening; }}

    //public event EventHandler<DataReceivedEventArgs> OnConnectionMade;
    //public event EventHandler<DataReceivedEventArgs> OnDataReceived;

    //public Server()
    //{
    //    _listener = new TcpListener(IPAddress.Any, port);
    //}

    //async
    //    // Use this for initialization
    //    void Start()
    //{
    //    StartAsync();
    //    OnConnectionMade += Server_OnDataReceived;
    //}

    //private async void Server_OnDataReceived(object sender, DataReceivedEventArgs e)
    //{
    //    Server server = (Server)sender;
    //    TcpClient client = (TcpClient)e.GetData();
    //    NetworkStream ns = (NetworkStream)client.GetStream();
    //    using (ns)
    //    {
    //        byte[] dataReceived = new byte[1024];
    //        while (client.Connected)
    //        {
    //            if(await ns.ReadAsync(dataReceived, 0, dataReceived.Length) != 0)
    //                if (OnDataReceived != null)
    //                    OnDataReceived.Invoke(this,new DataReceivedEventArgs(dataReceived));
    //        }
    //    }
    //}

    //private void OnApplicationQuit()
    //{
    //    Stop();
    //}


    //public async Task StartAsync(CancellationToken? token = null)
    //{
    //    _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(token ?? new CancellationToken());
    //    _token = _tokenSource.Token;
    //    _listener.Start();
    //    _listening = true;

    //    try
    //    {
    //        while (!_token.IsCancellationRequested)
    //        {
    //            await Task.Run(async () =>
    //            {
    //                var tcpClientTask = _listener.AcceptTcpClientAsync();
    //                var result = await tcpClientTask;
    //                if(OnConnectionMade!=null)
    //                    OnConnectionMade.Invoke(this, new DataReceivedEventArgs(result));
    //            }, _token);
    //        }
    //    }
    //    finally
    //    {
    //        _listener.Stop();
    //        _listening = false;
    //    }
    //}

    //public void Stop()
    //{
    //    _tokenSource?.Cancel();
    //}
}
