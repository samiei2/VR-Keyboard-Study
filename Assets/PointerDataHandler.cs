using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PointerDataHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Server").GetComponent<Server>().OnDataReceived += PointerDataHandler_OnDataReceived;
        GameObject.Find("Server").GetComponent<AsyncServer>().OnMessageReceived += PointerDataHandler_OnMessageReceived;

    }

    private void PointerDataHandler_OnMessageReceived(object sender, ClientEventArgs e)
    {
        Client client = e.Client; //Get the Client
        AsyncServer server = sender as AsyncServer;
        string msg = Encoding.ASCII.GetString(client.Message); //Get the message as a string
        bool hasData = false;
        for (int i = 0; i < client.Message.Length; i++)
        {
            if (client.Message[i] != 0)
                hasData = true;
        }
        if(!hasData)
            server.CloseConnection(client);
        Debug.Log(msg);
    }

    private void PointerDataHandler_OnDataReceived(object sender, DataReceivedEventArgs e)
    {
        byte[] data = (byte[])e.GetData();
        Debug.Log(Encoding.Default.GetString(data));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
