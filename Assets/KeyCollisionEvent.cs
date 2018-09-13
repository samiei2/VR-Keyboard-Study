using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollisionEvent : MonoBehaviour {
    public float movementMultiplier = 0.02f;
    private Vector3 keyPreviousPosition;
    private System.Diagnostics.Stopwatch _watch;
    private bool _isLerping;
    private float _timeStartedLerping;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    public float timeTakenDuringLerp = .35f;

    private void Start()
    {
        keyPreviousPosition = transform.parent.localPosition;
        _watch = new System.Diagnostics.Stopwatch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isLerping)
        {
            keyPreviousPosition = transform.parent.localPosition;
        }
        if (other.GetComponent<DrumEvents>() != null)
        {
            var drumDirection = other.GetComponent<DrumEvents>().GetMovementDirection();
            float angle = Vector3.Angle(drumDirection, transform.parent.forward);
            //print("trigger enter: " + transform.parent.name + "," + angle +"," + drumDirection.normalized.y +  "," + _watch.IsRunning + "," + _watch.ElapsedMilliseconds);
            if (angle > 100 && drumDirection.normalized.y > 0.7 )
            {
                if (_watch.IsRunning)
                {
                    if (_watch.ElapsedMilliseconds > 100)
                    {
                        StartLerping();
                        StartCoroutine("TriggerClick");
                        if (other.transform.parent.parent.GetComponent<SteamVR_TrackedController>() != null)
                            TriggerHapticPulse(other.transform.parent.parent.GetComponent<SteamVR_TrackedObject>());

                        _watch.Stop();
                        _watch.Reset();
                    }
                }
                else
                {
                    StartLerping();
                    StartCoroutine("TriggerClick");
                    if (other.transform.parent.parent.GetComponent<SteamVR_TrackedController>() != null)
                        TriggerHapticPulse(other.transform.parent.parent.GetComponent<SteamVR_TrackedObject>());

                    _watch.Stop();
                    _watch.Reset();
                }
            }
        }
        
    }

    private void TriggerClick()
    {
        transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
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

    private void FixedUpdate()
    {
        if (_watch != null)
        {
            if (_watch.IsRunning)
                if (_watch.ElapsedMilliseconds > 1000)
                {
                    _watch.Stop();
                    _watch.Reset();
                }
        }
        Debug.DrawRay(transform.parent.position, transform.parent.forward);
        //transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition, keyPreviousPosition, Time.deltaTime * movementSpeed);

        if (_isLerping)
        {
            //We want percentage = 0.0 when Time.time = _timeStartedLerping
            //and percentage = 1.0 when Time.time = _timeStartedLerping + timeTakenDuringLerp
            //In other words, we want to know what percentage of "timeTakenDuringLerp" the value
            //"Time.time - _timeStartedLerping" is.
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentageComplete = timeSinceStarted / timeTakenDuringLerp;

            //Perform the actual lerping.  Notice that the first two parameters will always be the same
            //throughout a single lerp-processs (ie. they won't change until we hit the space-bar again
            //to start another lerp)
            transform.parent.localPosition = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);

            //When we've completed the lerp, we set _isLerping to false
            if (percentageComplete >= 1.0f)
            {
                _isLerping = false;
            }
        }
    }

    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerping = Time.time;

        //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
        
        transform.parent.Translate(transform.parent.forward * movementMultiplier, Space.Self);
        _startPosition = transform.parent.localPosition;
        _endPosition = keyPreviousPosition;
    }
}
