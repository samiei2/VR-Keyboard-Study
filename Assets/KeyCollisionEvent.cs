using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisionEvent : MonoBehaviour {
    private float movementSpeed = 8f;
    private float movementMultiplier = 0.05f;
    private Vector3 keyPreviousPosition;
    private System.Diagnostics.Stopwatch _watch;


    private void Start()
    {
        keyPreviousPosition = transform.parent.localPosition;
        _watch = new System.Diagnostics.Stopwatch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DrumEvents>() != null)
        {
            var drumDirection = other.GetComponent<DrumEvents>().GetMovementDirection();
            float angle = Vector3.Angle(drumDirection, transform.parent.forward);

            if (angle > 120 && drumDirection.normalized.y > 0.8 && (_watch.IsRunning && _watch.ElapsedMilliseconds > 100))
            {
                transform.parent.Translate(transform.parent.forward * movementMultiplier, Space.Self);
                transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                if (other.transform.parent.parent.GetComponent<SteamVR_TrackedController>() != null)
                    TriggerHapticPulse(other.transform.parent.parent.GetComponent<SteamVR_TrackedObject>());

                _watch.Stop();
                _watch.Reset();
            }
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        _watch.Start();
        transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
    }

    void TriggerHapticPulse(SteamVR_TrackedObject index)
    {
        StartCoroutine(HapticPulse(index));
    }

    IEnumerator HapticPulse(SteamVR_TrackedObject _trackedObject)
    {

        if (_trackedObject.index == SteamVR_TrackedObject.EIndex.None)
        {
            yield break;
        }

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)_trackedObject.index);
        // When I wrote this, two frames of vibration felt like a really solid hit without it feeling sloppy.
        // Also using WaitForEndOfFrame() to match my implementation for the Oculus Touch controllers. Not sure if this is ideal.
        device.TriggerHapticPulse(2500);
        yield return new WaitForEndOfFrame();
        //device.TriggerHapticPulse(1500);
        //yield return new WaitForEndOfFrame();
    }

    private void Update()
    {
        if(_watch.IsRunning)
            if(_watch.ElapsedMilliseconds> 1000)
            {
                _watch.Stop();
                _watch.Reset();
            }
        Debug.DrawRay(transform.parent.position, transform.parent.forward);
        transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition, keyPreviousPosition, Time.deltaTime * movementSpeed);
    }
}
