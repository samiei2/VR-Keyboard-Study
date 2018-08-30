using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ViveTrackpad : MonoBehaviour {
    public event TouchDataHandler.TouchDataReceived TrackpadDataReceived;
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    SteamVR_TrackedController controller;
    TouchDataArgs args = new TouchDataArgs();

    void Awake()
    {
        
    }


    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadTouched += Controller_PadTouched;
        controller.TriggerClicked += Controller_TriggerClicked;

        controller.TriggerUnclicked += Controller_TriggerUnclicked ;
    }

    private void Controller_TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        TouchDataArgs args = new TouchDataArgs();
        args.TriggerDown = false;
        if (TrackpadDataReceived != null)
            TrackpadDataReceived(this, args);
    }

    private void Controller_TriggerClicked(object sender, ClickedEventArgs e)
    {
        TouchDataArgs args = new TouchDataArgs();
        args.TriggerDown = true;
        if(TrackpadDataReceived!=null)
            TrackpadDataReceived(this, args);
    }

    private void Controller_PadTouched(object sender, ClickedEventArgs e)
    {
        
    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 touchpad = new Vector2(device.GetAxis().x, device.GetAxis().y);
        if (touchpad.y > 0.5f)
        {
            print("Moving Up");
        }

        else if (touchpad.y < -0.5f)
        {
            print("Moving Down");
        }

        if (touchpad.x > 0.5f)
        {
            print("Moving Right");

        }
        else if (touchpad.x < -0.5f)
        {
            print("Moving left");
        }

        if (device.GetAxis().x != 0 && device.GetAxis().y != 0)
        {
            args.touchPadVector2 = new Vector2(device.GetAxis().x, device.GetAxis().y);
            if (TrackpadDataReceived != null)
                TrackpadDataReceived(this, args);
        }
    }
}
