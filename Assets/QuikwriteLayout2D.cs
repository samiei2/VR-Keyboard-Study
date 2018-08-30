using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuikwriteLayout2D : KeyboardLayout {
    
    private Material zoneHighlightMat;
    private Material invisibleMat;
    public float radius = -1;
    private String path = "";
    public bool HighlightZone = false;
    private Transform focusedZone;

    public override void CreateMainKeys()
    {
        GameObject hexPrefab = Resources.Load("BoxPrefab", typeof(GameObject)) as GameObject;

        keysDic.Add(KeyID.Q, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.W, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.E, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.R, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.T, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Y, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.U, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.I, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.O, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.P, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.A, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.S, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.D, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.F, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.G, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.H, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.J, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.K, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.L, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Z, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.X, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.C, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.V, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.B, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.N, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.M, Instantiate(hexPrefab) as GameObject);

        keysDic.Add(KeyID.Dot, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Backspace, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Comma, Instantiate(hexPrefab) as GameObject);
        //keysDic.Add(KeyID.Space, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Shift, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Enter, Instantiate(hexPrefab) as GameObject);
        keysDic.Add(KeyID.Next, Instantiate(hexPrefab) as GameObject);
    }

    public override void LayoutKeys()
    {
        foreach (var item in keysDic)
        {
            item.Value.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
        // Sector Top Left
        keysDic[KeyID.K].transform.position = new Vector3(-2, 4, 0);
        keysDic[KeyID.S].transform.position = new Vector3(-3, 4, 0);
        keysDic[KeyID.A].transform.position = new Vector3(-4, 4, 0);
        keysDic[KeyID.M].transform.position = new Vector3(-4, 3, 0);
        keysDic[KeyID.Q].transform.position = new Vector3(-4, 2, 0);

        // Sector Left
        keysDic[KeyID.H].transform.position = new Vector3(-4, 1, 0);
        keysDic[KeyID.E].transform.position = new Vector3(-4, 0, 0);
        keysDic[KeyID.C].transform.position = new Vector3(-4, -1, 0);

        // Sector Bottom Left
        keysDic[KeyID.V].transform.position = new Vector3(-4, -2, 0);
        keysDic[KeyID.W].transform.position = new Vector3(-4, -3, 0);
        keysDic[KeyID.O].transform.position = new Vector3(-4, -4, 0);
        keysDic[KeyID.G].transform.position = new Vector3(-3, -4, 0);
        keysDic[KeyID.Z].transform.position = new Vector3(-2, -4, 0);

        // Sector Bottom
        keysDic[KeyID.Backspace].transform.position = new Vector3(-1, -4, 0);
        keysDic[KeyID.Dot].transform.position = new Vector3(0, -4, 0);
        keysDic[KeyID.Next].transform.position = new Vector3(1, -4, 0);

        // Sector Bottom Right
        keysDic[KeyID.B].transform.position = new Vector3(2, -4, 0);
        keysDic[KeyID.D].transform.position = new Vector3(3, -4, 0);
        keysDic[KeyID.I].transform.position = new Vector3(4, -4, 0);
        keysDic[KeyID.R].transform.position = new Vector3(4, -3, 0);
        keysDic[KeyID.J].transform.position = new Vector3(4, -2, 0);

        // Sector Right
        keysDic[KeyID.Y].transform.position = new Vector3(4, -1, 0);
        keysDic[KeyID.T].transform.position = new Vector3(4, 0, 0);
        keysDic[KeyID.U].transform.position = new Vector3(4, 1, 0);

        // Sector Top Right
        keysDic[KeyID.X].transform.position = new Vector3(4, 2, 0);
        keysDic[KeyID.L].transform.position = new Vector3(4, 3, 0);
        keysDic[KeyID.N].transform.position = new Vector3(4, 4, 0);
        keysDic[KeyID.F].transform.position = new Vector3(3, 4, 0);
        keysDic[KeyID.P].transform.position = new Vector3(2, 4, 0);

        // Sector Top
        keysDic[KeyID.Enter].transform.position = new Vector3(1, 4, 0);
        keysDic[KeyID.Shift].transform.position = new Vector3(0, 4, 0);
        keysDic[KeyID.Comma].transform.position = new Vector3(-1, 4, 0);
        
    }

    public override void SetProperties()
    {
        foreach (var key in keysDic.Keys)
        {
            keysDic[key].name = "Key_" + key;

            keysDic[key].transform.parent = this.transform;
            if (PRINTABLEKEYS.ContainsKey(key))
                keysDic[key].transform.Find("Text").GetComponent<TextMeshPro>().text = PRINTABLEKEYS[key].ToString().ToUpper();
            keysDic[key].AddComponent<KeyEvents>();
            keysDic[key].AddComponent<KeyProperties>();
            if (PRINTABLEKEYS.ContainsKey(key))
                keysDic[key].GetComponent<KeyProperties>().KeyText = PRINTABLEKEYS[key].ToString();
            keysDic[key].GetComponent<KeyProperties>().ID = key;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyFocused += KeyboardEventHandler_OnFocusedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyUnfocused += KeyboardEventHandler_OnUnfocusedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyPressed += KeyboardEventHandler_OnPressedHandler;
            keysDic[key].GetComponent<KeyEvents>().KeyEvents_OnKeyReleased += KeyboardEventHandler_OnReleasedHandler;
        }

        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 8;
        keysDic[KeyID.Backspace].transform.Find("Text").GetComponent<TextMeshPro>().text = "⇦";
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 7.5f;
        keysDic[KeyID.Shift].transform.Find("Text").GetComponent<TextMeshPro>().text = "⇪";
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 4;
        keysDic[KeyID.Next].transform.Find("Text").GetComponent<TextMeshPro>().text = "Next";
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 7;
        keysDic[KeyID.Enter].transform.Find("Text").GetComponent<TextMeshPro>().text = "↵";

        keysDic[KeyID.Comma].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 7;
        keysDic[KeyID.Dot].transform.Find("Text").GetComponent<TextMeshPro>().fontSize = 7;
    }

    public override void HighlightKeys(List<char> suggestedAlphabet)
    {
        
    }

    public override void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;


        transform.Find("Tint").gameObject.SetActive(true);
        if (zoomEffect)
        {
            transform.localScale *= 1.5f;
            //ChangeClosebyKeys(transform);
        }

        if (dwell)
        {
            transform.GetComponent<KeyProperties>().StartDwell(dwellWaitTime);
        }
    }

    public override void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;

        transform.Find("MainShape").GetComponent<MeshRenderer>().material = transform.GetComponent<KeyProperties>().pressedMat;

        if (args.KeyPrintable)
        {
            if (textArea != null)
            {
                OnKeyPressed(sender, args);
                textArea.GetComponent<TextMeshPro>().text += args.KeyText;
            }
        }
        else
        {
            HandleNonPrintable(sender, args);
        }
    }

    public override void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;
        transform.Find("MainShape").GetComponent<Renderer>().material = transform.GetComponent<KeyProperties>().invisibleMat;
    }

    public override void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;

        transform.Find("Tint").gameObject.SetActive(false);
        if (zoomEffect)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (dwell)
        {
            transform.GetComponent<KeyProperties>().StopDwell();
        }
    }

    private void HandleNonPrintable(object sender, KeyEventArgs args)
    {
        var transform = (Transform)sender;
        if (args.KeyId == KeyID.Backspace)
        {
            if (textArea.GetComponent<TextMeshPro>().text.Length > 0)
                textArea.GetComponent<TextMeshPro>().text =
                    textArea.GetComponent<TextMeshPro>().text.Remove(textArea.GetComponent<TextMeshPro>().text.Length - 1, 1);
        }
        else if (args.KeyId == KeyID.Enter)
        {
            textArea.GetComponent<TextMeshPro>().text += '\n';
        }
    }

    // Use this for initialization
    public override void Start ()
    {
        
        zoneHighlightMat = Resources.Load("ZoneHighlightMat") as Material;
        invisibleMat = Resources.Load("InvisibleMaterial") as Material;
        base.Start();
	}

    public override void Update()
    {
        base.Update();

        InputButtonDown = Input.GetMouseButton(0);
        if (InputButtonDown)
        {
            if (IsInRestZone(pointer))
            {
                if (path.Length != 0) // We might have returned to zone after visiting other zones or just resting in zone 0
                {
                    if (path.Length > 2) // We have been to other zones
                    {
                        path += "Z0";
                        // Parse path ...
                        ProcessPathAction(path);
                        path = "";
                    }
                    else
                    { // we are just resting in zone 0
                        path = "Z0";
                    }
                }
                else
                {
                    path += "Z0"; // Add Zone 0 as start of path
                }
            }
            else
            {
                if (path.StartsWith("Z0"))
                {
                    // Into other zones
                    String currentZone = GetZoneOfPointer();
                    if(currentZone != oldZone)
                    {
                        path += currentZone;
                        oldZone = currentZone;
                    }
                }
            }
        }
        else if (InputButtonUp)
        {
            if (path.StartsWith("Z0") && path.Length > 2)  // Path might be valid
            {
                ProcessPathAction(path);
                path = "";
            }
            else
            { // Discard path
                path = "";
            }
        }
    }

    private void ProcessPathAction(string path)
    {
        if (path.StartsWith("Z0") && path.Length > 2)
        {
            var pathWithoutStartZone = path.Remove(0,2);// Dont need the rest zone Z0 in processing
            String characterCode = "";
            for (int i = 0; i < pathWithoutStartZone.Length; i+=2)
            {
                var zone = pathWithoutStartZone.Substring(i, 2); // Each zone is 2 char code (ex. Z1) 
                characterCode += zone.Substring(1, 1); // Just append the zone number
            }
            if (!characterCode.EndsWith("0"))
                characterCode += "0";
            int code = Int32.Parse(characterCode);
            Debug.Log(characterCode);
        }
    }

    private string GetZoneOfPointer()
    {
        // Visual bug problem. needs to be fixed
        foreach (Transform zone in transform.Find("RegionQuads"))
        {
            zone.GetComponent<MeshRenderer>().material = invisibleMat;
        }
        Ray ray = new Ray(pointer.transform.position, pointer.transform.position - Camera.main.transform.position);
        Debug.DrawRay(ray.origin,ray.direction,Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,20f))
        {
            if (hit.transform.name.Contains("Zone"))
            {
                // Light up zone?
                if (HighlightZone)
                {
                    focusedZone = hit.transform;
                    focusedZone.GetComponent<MeshRenderer>().material = zoneHighlightMat;
                }
                return hit.transform.name.Replace("one", ""); // Removes "one" from "Zone" which results in "Z1" etc
            }
        }
        return "";
    }

    private bool IsInRestZone(Pointer pointer)
    {
        var center = transform.position;
        if (radius == -1)
            radius = FindRadius();

        var distance = Math.Sqrt(
            Math.Pow(pointer.transform.position.x - center.x,2) + Math.Pow(pointer.transform.position.y - center.y, 2));
        //Debug.Log(distance + "," + radius);
        if(distance < Math.Pow(radius, 2))
        {
            //Debug.Log("In Rest");
            return true;
        }
        //Debug.Log("Outside");
        return false;
    }

    private float FindRadius()
    {
        var center = transform.position;
        var parent = transform.Find("Seperators");
        float maxDist = -1;
        foreach (Transform seperator in parent)
        {
            var sphere = seperator.Find("Sphere");
            var dist = sphere.position - center;
            if (dist.magnitude > maxDist)
                maxDist = dist.magnitude;
        }
        return maxDist;
    }

    private Dictionary<int, KeyID> codeToKeyId = new Dictionary<int, KeyID>
    {
        { 080,KeyID.A },
        { 04560,KeyID.B },
        { 0760,KeyID.C },
        { 0450,KeyID.D },
        { 070,KeyID.E },
        { 0210,KeyID.F },
    };
    private string oldZone;
}
