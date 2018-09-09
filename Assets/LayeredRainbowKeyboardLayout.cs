using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayeredRainbowKeyboardLayout : RainbowKeyboardLayout {
    public float radius = 12;
    public float theta = -90;
    public int r1 = 12;
    public int r2 = 9;
    public int r3 = 5;
    public float deg1 = 13.3f;
    public float deg2 = 13.3f;
    public float deg3 = 5.3f;
    public float degTotalf = 120f;

    List<KeyID> keyList1 = new List<KeyID>() {
        // Row 1
        KeyID.A,KeyID.B,KeyID.C,KeyID.D,KeyID.E,KeyID.F,KeyID.G,KeyID.H,KeyID.I,KeyID.J,KeyID.K,KeyID.L,
        KeyID.M,KeyID.N,KeyID.O,KeyID.P,KeyID.Q,KeyID.R,KeyID.S,KeyID.T,KeyID.U,
        KeyID.V,KeyID.W,KeyID.X,KeyID.Y,KeyID.Z
    };

    List<KeyID> keyList2 = new List<KeyID>() {
        // Row 1
        KeyID.Q,KeyID.W,KeyID.E,KeyID.R,KeyID.T,KeyID.Y,KeyID.U,KeyID.I,KeyID.O,KeyID.P,
        KeyID.A,KeyID.S,KeyID.D,KeyID.F,KeyID.G,KeyID.H,KeyID.J,KeyID.K,KeyID.L,
        KeyID.Z,KeyID.X, KeyID.C,KeyID.V, KeyID.B,KeyID.N,KeyID.M,
    };
    
    public float delta;
    
    public float initR1 = 0 ;
    public float initR2 = 0;
    public float initR3 = 0;


    public override void LayoutKeys()
    {
        var keys = keyList2;

        var xcenter = 0;
        var ycenter = 0;
        var radius1 = radius + 2 * delta;
        theta = -degTotalf / 2;

        foreach (var item in keysDic)
        {
            keysDic[item.Key].transform.localScale = new Vector3(keyScale, keyScale, keyScale);
        }

        foreach (var key in keys.GetRange(0, r1))
        {
            var x = xcenter + radius1 * Mathf.Sin(ConvertToRadians(theta + initR1));
            var y = ycenter + radius1 * Mathf.Cos(ConvertToRadians(theta + initR1));
            var z = 0;

            theta += (degTotalf / deg1);
            keysDic[key].transform.localPosition = new Vector3(x, y, z);
        }

        theta = -degTotalf / 2;
        radius1 = radius + delta;
        foreach (var key in keys.GetRange(r1, r2))
        {
            var x = xcenter + radius1 * Mathf.Sin(ConvertToRadians(theta + initR2));
            var y = ycenter + radius1 * Mathf.Cos(ConvertToRadians(theta + initR2));
            var z = 0;

            theta += (degTotalf / deg2);
            keysDic[key].transform.localPosition = new Vector3(x, y, z);
        }

        theta = -degTotalf / 2;
        radius1 = radius;
        foreach (var key in keys.GetRange(r1 + r2, r3))
        {
            var x = xcenter + radius1 * Mathf.Sin(ConvertToRadians(theta + initR3));
            var y = ycenter + radius1 * Mathf.Cos(ConvertToRadians(theta + initR3));
            var z = 0;

            theta += (degTotalf / deg3);
            keysDic[key].transform.localPosition = new Vector3(x, y, z);
        }
        var rightMostKeyCoord = getRightMostKeyCoords();

        var leftMostKeyCoord = getLeftMostKeyCoords();
        

        keysDic[KeyID.Space].transform.localPosition = new Vector3(0, keysDic[KeyID.Space].transform.localScale.y, 0);
        keysDic[KeyID.Backspace].transform.localPosition = new Vector3(leftMostKeyCoord.x + ((2 * keyScale) + keyScale / 3), leftMostKeyCoord.y + keyScale, 0);
        keysDic[KeyID.Shift].transform.localPosition = new Vector3(leftMostKeyCoord.x + ((2 * keyScale) + keyScale / 3), leftMostKeyCoord.y, 0);
        keysDic[KeyID.Enter].transform.localPosition = new Vector3(leftMostKeyCoord.x + ((3 * keyScale) + keyScale / 3), leftMostKeyCoord.y, 0);
        keysDic[KeyID.Dot].transform.localPosition = new Vector3(rightMostKeyCoord.x - ((2 * keyScale) + keyScale / 3), rightMostKeyCoord.y + keyScale , 0);
        keysDic[KeyID.Comma].transform.localPosition = new Vector3(rightMostKeyCoord.x - ((2 * keyScale) + keyScale / 3), rightMostKeyCoord.y, 0);
        keysDic[KeyID.Next].transform.localPosition = new Vector3(rightMostKeyCoord.x - ((3 * keyScale) + keyScale / 3), rightMostKeyCoord.y, 0);

        //Debug.LogError(Vector3.Angle(new Vector3(0, 1, 0) - new Vector3(0, 0, 0), keysDic[KeyID.Comma].transform.localPosition - new Vector3(0, 0, 0)));

        foreach (var item in keysDic)
        {
            var angle = Vector3.Angle(new Vector3(0, 1, 0) - new Vector3(0, 0, 0), item.Value.transform.localPosition - new Vector3(0, 0, 0)) / 2;
            item.Value.transform.eulerAngles = new Vector3(0,0, item.Value.transform.localPosition.x < 0?angle:-angle);
        }
    }

    private Vector3 getLeftMostKeyCoords()
    {
        var left = keysDic[KeyID.Space].transform.localPosition.x;
        GameObject leftMostKey = null;
        foreach (var item in keyList2)
        {
            if (left < keysDic[item].transform.localPosition.x)
            {
                left = keysDic[item].transform.localPosition.x;
                leftMostKey = keysDic[item];
            }
        }
        return leftMostKey.transform.localPosition;
    }

    private Vector3 getRightMostKeyCoords()
    {
        var right = keysDic[KeyID.Space].transform.localPosition.x; ;
        GameObject rightMostKey = null;
        foreach (var item in keyList2)
        {
            if (right > keysDic[item].transform.localPosition.x)
            {
                right = keysDic[item].transform.localPosition.x;
                rightMostKey = keysDic[item];
            }
        }
        return rightMostKey.transform.localPosition;
    }

    public override void Update()
    {
        base.Update();
        
    }
}
