using System.Collections;
using System.Collections.Generic;
using Tobii.Research.Unity;
using UnityEngine;

public class testgaze : MonoBehaviour {
    private VREyeTracker _eyeTracker;

    // Use this for initialization
    void Start () {
        _eyeTracker = VREyeTracker.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (_eyeTracker.Connected)
        {
            Vector3 pos = _eyeTracker.LatestProcessedGazeData.Pose.Position;
            pos.z = Camera.main.transform.position.z + 6;

            transform.position = pos;

        }
	}
}
