using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TouchDataHandler : MonoBehaviour {
    public delegate void TouchDataReceived(object sender, TouchDataArgs args);
    public event TouchDataReceived TouchDataReceivedEvent;

	// Use this for initialization
	void Start () {
        //GameObject.Find("Server").GetComponent<Server>().OnDataReceived += TouchDataHandler_OnDataReceived;
        GameObject.Find("Server").GetComponent<AsyncServer>().OnMessageReceived += TouchDataHandler_OnMessageReceived;

    }

    private void TouchDataHandler_OnMessageReceived(object sender, ClientEventArgs e)
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
        //Debug.Log("MSG: " + msg);
        
        if (msg.Contains(","))
        {
            string[] lines = msg.Split('\n');
            foreach (var line in lines)
            {
                //Debug.Log("Line: " + line);
                if (line.Contains(","))
                {
                    try
                    {
                        var newline = line.Replace(",", "");
                        string[] values = newline.Split(':');
                        //Debug.Log("v0:"+ values[0]+ ",v1:" + values[1]+ ",v2:" + values[2]);
                        int action = Int32.Parse(values[0].Trim());
                        int x = Int32.Parse(values[1].Trim());
                        int y = Int32.Parse(values[2].Trim());
                        int top = Int32.Parse(values[3].Trim());
                        int bottom = Int32.Parse(values[4].Trim());
                        int left = Int32.Parse(values[5].Trim());
                        int right = Int32.Parse(values[6].Trim());

                        TouchDataArgs args = new TouchDataArgs
                        {
                            action = action,
                            x = x,
                            y = y,
                            top = top,
                            bottom = bottom,
                            left = left,
                            right = right,
                        };

                        if(TouchDataReceivedEvent != null)
                            TouchDataReceivedEvent.Invoke(this, args);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
            }
        }
    }

    private void TouchDataHandler_OnDataReceived(object sender, DataReceivedEventArgs e)
    {
        byte[] data = (byte[])e.GetData();
        Debug.Log(Encoding.Default.GetString(data));
    }

    // Update is called once per frame
    void Update () {
		
	}
}

public class TouchDataArgs
{
    internal int action;
    internal int x;
    internal int y;
    internal int top;
    internal int bottom;
    internal int left;
    internal int right;
}
