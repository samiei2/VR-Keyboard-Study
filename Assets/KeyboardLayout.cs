using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tobii.Research.Unity;
using UnityEngine;

public abstract class KeyboardLayout : MonoBehaviour
{
    protected Dictionary<KeyID, GameObject> keysDic = new Dictionary<KeyID, GameObject>();
    public event KeyEvents.KeyEvent KeyboardLayout_OnKeyPressed;
    private VREyeTracker _eyeTracker;
    private VRCalibration _calibrationObject;

    public GameObject textArea;

    public static Dictionary<KeyID, char> PRINTABLEKEYS = new Dictionary<KeyID, char>()
    {
        { KeyID.A, 'a' },{ KeyID.B, 'b' },{ KeyID.C, 'c' },{ KeyID.D, 'd' },{ KeyID.E, 'e' },
        { KeyID.F, 'f' },{ KeyID.G, 'g' },{ KeyID.H, 'h' },{ KeyID.I, 'i' },{ KeyID.J, 'j' },
        { KeyID.K, 'k' },{ KeyID.L, 'l' },{ KeyID.M, 'm' },{ KeyID.N, 'n' },{ KeyID.O, 'o' },
        { KeyID.P, 'p' },{ KeyID.Q, 'q' },{ KeyID.R, 'r' },{ KeyID.S, 's' },{ KeyID.T, 't' },
        { KeyID.U, 'u' },{ KeyID.V, 'v' },{ KeyID.W, 'w' },{ KeyID.X, 'x' },{ KeyID.Y, 'y' },
        { KeyID.Z, 'z' },
        { KeyID.Space, ' ' },{ KeyID.Space2, ' ' },{ KeyID.Tab, '\t' },
        { KeyID.N1, '1' },{ KeyID.N2, '2' },{ KeyID.N3, '3' },{ KeyID.N4, '4' },{ KeyID.N5, '5' },
        { KeyID.N6, '6' },{ KeyID.N7, '7' },{ KeyID.N8, '8' },{ KeyID.N9, '9' },{ KeyID.N0, '0' },
        { KeyID.Quote, '\'' },{ KeyID.DoubleQuote, '\"' },{ KeyID.Enter, '\n' },{ KeyID.Dot, '.' }, { KeyID.Comma, ',' },
        { KeyID.Exclamation, '!' },{ KeyID.At, '@' },{ KeyID.Sharp, '#' },{ KeyID.Dollar, '$' },{ KeyID.Percent, '%' },
        { KeyID.Hat, '^' },{ KeyID.Ampersand, '&' },{ KeyID.Star, '*' },{ KeyID.LParathesis, '(' },{ KeyID.RParathesis, ')' },
        { KeyID.Underline, '_' },{ KeyID.Dash, '-' },{ KeyID.Plus, '+' },{ KeyID.Slash, '/' },{ KeyID.BackSlash, '\\' },
    };

    public virtual void Start()
    {
        MainCamera = GameObject.Find("[CameraRig]").transform.Find("Camera (eye)");
        gazeTrailGameObject = GameObject.Find("[VRGazeTrail]");
        CreateMainKeys();
        SetProperties();
        LayoutKeys();
        if (touchHandler != null)
        {
            touchHandler.TouchDataReceivedEvent += Pointer_PointerDataReceivedEvent;
        }

        var cameraRig = GameObject.Find("[CameraRig]");
        if (cameraRig != null)
            leftTrackpadHandler = cameraRig.transform.Find("Controller (left)").GetComponent<ViveTrackpad>();
        if (cameraRig != null)
            rightTrackpadHandler = cameraRig.transform.Find("Controller (right)").GetComponent<ViveTrackpad>();

        if (leftTrackpadHandler!=null)
        {
            leftTrackpadHandler.TrackpadDataReceived += TrackpadHandler_TrackpadDataReceived;
        }
        if (rightTrackpadHandler!=null)
        {
            rightTrackpadHandler.TrackpadDataReceived += TrackpadHandler_TrackpadDataReceived;
        }
        _eyeTracker = VREyeTracker.Instance;
        _calibrationObject = VRCalibration.Instance;


        //transform.position = new Vector3(
        //    Camera.main.transform.position.x,
        //    Camera.main.transform.position.y,
        //    Camera.main.transform.position.z + keyboardDistanceFromCamera);

        transform.position = new Vector3(MainCamera.position.x, MainCamera.position.y, MainCamera.position.z + keyboardDistanceFromCamera);
        //transform.parent = MainCamera;
        
        var keyboardTop = GetKeyboardTop();

        textArea = GameObject.Find("TextArea");
        textArea.transform.parent = this.transform;
        textArea.transform.localPosition = new Vector3(0, keyboardTop + 4, 2);
    }

    private void TrackpadHandler_TrackpadDataReceived(object sender, TouchDataArgs args)
    {
        ViveTrackpad controller = sender as ViveTrackpad;
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (useTrackerInputForPointer && controller.IsRightController())
            {
                if (args.TriggerDown)
                {
                    InputButtonDown = true;
                    InputButtonHeldDown = true;
                    InputButtonUp = false;
                }
                else
                {
                    InputButtonDown = false;
                    InputButtonHeldDown = false;
                    InputButtonUp = true;
                }
            }
            if (InputType == KeyboardInputType.Ray || InputType == KeyboardInputType.DrumStick)
            {
                InputButtonDown = args.TriggerDown;
                InputButtonHeldDown = args.TriggerDown;
                InputButtonUp = args.TriggerUp;
            }

            if (args.moveDirection != MovementDirection.None) 
            {

            }
        });
    }

    private float GetKeyboardTop()
    {
        float top = int.MinValue;
        foreach (var item in keysDic)
        {
            if (item.Value.transform.position.y > top)
                top = item.Value.transform.position.y;
        }
        return top;
    }

    public int pointerSpeedMultiplier = 10;
    
    Vector3 vec, oldVec, lerpedVec, position;
    int action, x, y, top, bottom, left, right, width, height;
    float touchMovementPercentageX, touchMovementPercentageY, initialValX, initialValY; 
    private void Pointer_PointerDataReceivedEvent(object sender, TouchDataArgs args)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (InputType == KeyboardInputType.TouchPad)
            {
                action = args.action;
                x = args.x;
                y = args.y;
                top = args.top;
                bottom = args.bottom;
                left = args.left;
                right = args.right;
                if (action == 0) {
                    prevX = x;
                    prevY = y;
                    //HandleTouchInput(action);
                } else if (action == 2)
                {

                    vec = Camera.main.WorldToScreenPoint(pointer.transform.position);
                    oldVec = Camera.main.WorldToScreenPoint(pointer.transform.position);

                    width = Math.Abs(right - left);
                    height = Math.Abs(bottom - top);

                    //Debug.Log("DiffX: " + (prevX - x) + "," + "DiffY: " + (prevY - y));
                    //Debug.Log("Width: " + width + ",Height: " + height);
                    touchMovementPercentageX = ((prevX - x) * Screen.height) / height*pointerSpeedMultiplier;
                    touchMovementPercentageY = ((prevY - y) * Screen.height) / height*pointerSpeedMultiplier;
                    //Debug.Log("PercentX: " + touchMovementPercentageX + "," + "PercentY: " + touchMovementPercentageY);

                    //float screenMovementX = Screen.height * touchMovementPercentageX;
                    //float screenMovementY = Screen.width * touchMovementPercentageY;

                    initialValX = 0;
                    initialValY = 0;
                    if ((prevX - x) < 0)
                        initialValX = -1;
                    else
                        initialValX = 1;
                    if ((prevY - y) < 0)
                        initialValY = -1;
                    else
                        initialValY = 1;
                    vec.x += -(touchMovementPercentageX / Time.deltaTime/40+ initialValX) ;
                    vec.y += (touchMovementPercentageY / Time.deltaTime/40 + initialValY);
                    
                    lerpedVec = Vector3.Lerp(oldVec, vec, 1.0f - Mathf.Exp(-10 * Time.deltaTime));

                    position = Camera.main.ScreenToWorldPoint(lerpedVec);
                    //Vector3 worldVec = Camera.main.ScreenToWorldPoint(vec);

                    //Vector3 position = Vector3.Lerp(pointer.transform.position, worldVec, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
                    position.z = Math.Abs(Camera.main.transform.position.z - transform.position.z) + pointerDistanceFromCamera;
                    
                    pointer.transform.position = position;

                    prevX = x;
                    prevY = y;
                }
            }
        });


    }

    public virtual void Update()
    {
        if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.S))
        {
            SaveUserLayout();
        }
        else if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.R))
        {
            RotateKeysTowardsHead();
        }
        else if ((Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && Input.GetKeyDown(KeyCode.L))
        {
            LoadLayoutFile();
        }
        //foreach (var item in keysDic)
        //{
        //    Debug.DrawRay(item.Value.transform.position, item.Value.transform.forward);
        //}
        
        if (gazeTrailGameObject != null)
        {
            if (gazeTrailGameObject.activeInHierarchy)
                gazeTrailGameObject.SetActive(false);
        }

        if (updateLayout)
            LayoutKeys();

        //transform.position = new Vector3(
        //    Camera.main.transform.position.x,
        //    Camera.main.transform.position.y,
        //    Camera.main.transform.position.z + keyboardDistanceFromCamera);

        if (InputType != KeyboardInputType.GazeAndDwell)
            dwell = false;
        
        if (InputType == KeyboardInputType.Mouse)
        {
            InputButtonDown = Input.GetMouseButtonDown(0);
            InputButtonUp = Input.GetMouseButtonUp(0);
            HandleMouseInput();

            if (pointer != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                
                //mousePosition.z = 9;
                mousePosition.z = Math.Abs(Camera.main.transform.position.z - transform.position.z) + pointerDistanceFromCamera;

                Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 position = Vector3.Lerp(pointer.transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
                //position.z = transform.position.z + pointerDistanceFromKeyboard;
                pointer.transform.position = position;
            }
        }

        if (InputType == KeyboardInputType.TouchPad)
        {
            InputButtonDown = action == 0 && tapActionEnabled ? true : false;
            InputButtonUp = action == 1 && tapActionEnabled ? true : false;
            HandleTouchInput(-1);
        }

        if(InputType == KeyboardInputType.GazeAndDwell || InputType == KeyboardInputType.GazeAndClick)
        {
            if (gazeTrailGameObject != null)
            {
                if (!gazeTrailGameObject.activeInHierarchy)
                    gazeTrailGameObject.SetActive(true);
            }
            if(InputType == KeyboardInputType.GazeAndDwell)
            {
                if (!dwell)
                    dwell = true;
            }
            if(InputType == KeyboardInputType.GazeAndClick)
            {
                if (!useViveTrackpad)
                {
                    InputButtonDown = action == 0 && tapActionEnabled ? true : false;
                    InputButtonUp = action == 1 && tapActionEnabled ? true : false;
                }
                // else its set in the trigger handler
            }
            HandleGazeInput();
        }

        if(InputType == KeyboardInputType.Swype)
        {
            InputButtonDown = action == 0 && tapActionEnabled ? true : false;
            InputButtonUp = action == 1 && tapActionEnabled ? true : false;
        }

        if (InputType == KeyboardInputType.Ray)
        {
            HandleRayInput();
        }

        if (InputType == KeyboardInputType.DrumStick)
        {
            if (leftTrackpadHandler.GetComponent<ViveCursor>().enabled)
            {
                leftTrackpadHandler.GetComponent<ViveCursor>().enabled = false;
            }
            if (rightTrackpadHandler.GetComponent<ViveCursor>().enabled)
            {
                rightTrackpadHandler.GetComponent<ViveCursor>().enabled = false;
            }
            HandleDrumstickInput();
        }
    }

    private void RotateKeysTowardsHead()
    {
        foreach (var key in keysDic)
        {
            key.Value.transform.LookAt(GameObject.Find("[CameraRig]").transform.Find("Camera (eye)").position);
        }
    }

    private void SaveUserLayout()
    {
        List<GameObjectData> _data = new List<GameObjectData>();

        foreach (var item in keysDic)
        {
            _data.Add(new GameObjectData()
            {
                Key = item.Key,
                Position = new float[] { item.Value.transform.localPosition.x, item.Value.transform.localPosition.y, item.Value.transform.localPosition.z },
                Rotation = new float[] { item.Value.transform.eulerAngles.x, item.Value.transform.eulerAngles.y, item.Value.transform.eulerAngles.z },
                Scale = new float[] { item.Value.transform.localScale.x, item.Value.transform.localScale.y, item.Value.transform.localScale.z }
            });
        }

        String filePath = Application.dataPath + "\\LayoutData\\" + transform.name + "_layout.txt";
        Directory.CreateDirectory(Application.dataPath + "\\LayoutData\\");
        if (File.Exists(filePath))
        {
            if (File.Exists(filePath + "._backup"))
                File.Delete(filePath + "._backup");
            File.Copy(filePath, filePath + "._backup");
        }
        using (StreamWriter file = File.CreateText(filePath))
        {
            JsonSerializer serializer = new JsonSerializer();
            //serialize object directly into file stream
            serializer.Serialize(file, _data);
        }
    }

    protected bool UserLayoutFileExist()
    {
        return File.Exists(Application.dataPath + "\\LayoutData\\" + transform.name + "_layout.txt");
    }


    protected void LoadLayoutFile()
    {
        String filePath = Application.dataPath + "\\LayoutData\\" + transform.name + "_layout.txt";
        using (StreamReader file = File.OpenText(filePath))
        {
            JsonSerializer serializer = new JsonSerializer();

            List<GameObjectData> layoutdata = (List<GameObjectData>)serializer.Deserialize(file, typeof(List<GameObjectData>));
            foreach (var item in layoutdata)
            {
                keysDic[item.Key].transform.localPosition = new Vector3(item.Position[0], item.Position[1], item.Position[2]);
                keysDic[item.Key].transform.eulerAngles = new Vector3(item.Rotation[0], item.Rotation[1], item.Rotation[2]); ;
                keysDic[item.Key].transform.localScale = new Vector3(item.Scale[0], item.Scale[1], item.Scale[2]); ;
            }
        }
    }

    private void HandleDrumstickInput()
    {
        if (leftTrackpadHandler != null && leftTrackpadHandler.transform.gameObject.activeInHierarchy)
        {
            if (leftDrumStick == null)
            {
                leftDrumStick = Instantiate(Resources.Load<GameObject>("DrumstickPrefab"));
                leftDrumStick.transform.forward = leftTrackpadHandler.transform.forward;
                leftDrumStick.transform.parent = leftTrackpadHandler.transform;
            }

            var controllerPosition = leftTrackpadHandler.GetPosition();
            leftDrumStick.transform.Find("Capsule").localScale = new Vector3(leftDrumStick.transform.Find("Capsule").localScale.x, rDrumLength, leftDrumStick.transform.Find("Capsule").localScale.z);
            leftDrumStick.transform.Find("Sphere").localPosition = new Vector3(0, 0, leftDrumStick.transform.Find("Capsule").localScale.y);
            leftDrumStick.transform.localPosition = new Vector3(0, 0, leftDrumStick.transform.Find("Capsule").localScale.y);
        }
        if (rightTrackpadHandler != null && rightTrackpadHandler.transform.gameObject.activeInHierarchy)
        {
            if (rightDrumStick == null)
            {
                rightDrumStick = Instantiate(Resources.Load<GameObject>("DrumstickPrefab"));
                rightDrumStick.transform.forward = rightTrackpadHandler.transform.forward;
                rightDrumStick.transform.parent = rightTrackpadHandler.transform;
            }

            var controllerPosition = rightTrackpadHandler.GetPosition();
            rightDrumStick.transform.Find("Capsule").localScale = new Vector3(rightDrumStick.transform.Find("Capsule").localScale.x, rDrumLength, rightDrumStick.transform.Find("Capsule").localScale.z);
            rightDrumStick.transform.Find("Sphere").localPosition = new Vector3(0, 0, rightDrumStick.transform.Find("Capsule").localScale.y);
            rightDrumStick.transform.localPosition = new Vector3(0, 0, rightDrumStick.transform.Find("Capsule").localScale.y);

            // Automatic length adjustment
            //RaycastHit hit;
            //Ray ray = new Ray(rightTrackpadHandler.GetPosition(), rightTrackpadHandler.GetForward());

            //bool rayHit = Physics.Raycast(ray, out hit);

            //float beamLength = GetBeamLength(rayHit, hit, rDrumLength, rDrumContactTarget, rContactDistance);
            //rightDrumStick.transform.Find("Capsule").localScale = new Vector3(rightDrumStick.transform.Find("Capsule").localScale.x,beamLength, rightDrumStick.transform.Find("Capsule").localScale.z);
            //rightDrumStick.transform.Find("Sphere").localPosition = new Vector3(0, 0, rightDrumStick.transform.Find("Capsule").localScale.y);

        }
    }

    private void HandleRayInput()
    {
        if (leftTrackpadHandler!=null && leftTrackpadHandler.transform.gameObject.activeInHierarchy)
        {
            if (leftTrackpadHandler.GetComponent<DrumCursor>().isActiveAndEnabled)
            {
                leftTrackpadHandler.GetComponent<DrumCursor>().enabled = false;
            }
            if (!leftTrackpadHandler.GetComponent<ViveCursor>().isActiveAndEnabled)
            {
                leftTrackpadHandler.GetComponent<ViveCursor>().enabled = true;
                leftTrackpadHandler.GetComponent<ViveCursor>().transform.eulerAngles = new Vector3(0,0,0);
            }
            RaycastHit hit;
            Ray ray = new Ray(leftTrackpadHandler.GetPosition(), leftTrackpadHandler.GetForward());
            

            // This is necessary because of the current way we handle hits, we need to seperate it for now,later change logic
            if (Physics.Raycast(ray, out hit))
            {
                HandleLeftHit(hit.transform.gameObject);
            }
            else
            {
                HandleLeftNotHit();
            }
        }
        if (rightTrackpadHandler!=null && rightTrackpadHandler.transform.gameObject.activeInHierarchy)
        {
            if (rightTrackpadHandler.GetComponent<DrumCursor>().isActiveAndEnabled)
            {
                rightTrackpadHandler.GetComponent<DrumCursor>().enabled = false;
            }
            if (!rightTrackpadHandler.GetComponent<ViveCursor>().isActiveAndEnabled)
            {
                rightTrackpadHandler.GetComponent<ViveCursor>().enabled = true;
                rightTrackpadHandler.GetComponent<ViveCursor>().transform.eulerAngles = new Vector3(0, 0, 0);
            }
            RaycastHit hit;
            Ray ray = new Ray(rightTrackpadHandler.GetPosition(), rightTrackpadHandler.GetForward());
            
            if (Physics.Raycast(ray, out hit))
            {
                HandleHit(hit.transform.gameObject);
            }
            else
            {
                HandleNotHit();
            }
        }
    }

    private void HandleGazeInput()
    {
        if (_eyeTracker != null)
        {
            Ray ray;
            var valid = GetRay(out ray);
            
            if (valid)
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    HandleHit(hit.transform.gameObject);
                }
                else
                {
                    HandleNotHit();
                }
            }
        }
    }

    private void HandleNotHit()
    {
        if (inKeyPress)
        {
            StopCoroutine("RepeatKeyPress");
            inKeyPress = false;
            if (objectInFocus == null)
            {
                // release all
                foreach (var item in keysDic)
                {
                    item.Value.transform.GetComponent<KeyEvents>().Key_ReleaseEvent();
                }
            }
            else
            {
                try
                {
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.StackTrace);
                }
            }
        }
        else
        {
        }
        if (objectInFocus != null)
        {
            if (objectInFocus.name.Contains("MainShape"))
            {
                //Debug.Log("You realsed the " + objectInFocus.transform.parent.name);
                objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                objectInFocus = null;
                focused = false;
                // There is a bug in visual update and we have to do the following 
                //foreach (Transform child in transform)
                //{
                //    if(child.Find("MainShape")!=null)
                //    child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                //}
                //////////////////////////////////////////////////////////////
            }
        }
    }

    private void HandleHit(GameObject hit)
    {
        if (hit.transform.name.Contains("MainShape"))
        {
            if (InputButtonDown)
            {
                //Debug.Log("Mouse down on the " + hit.transform.parent.name);
                inKeyPress = true;
                hit.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                StartCoroutine("RepeatKeyPress");
                InputButtonDown = false;
            }
            else if (InputButtonUp)
            {
                //Debug.Log("Mouse up on the " + hit.transform.parent.name);
                StopCoroutine("RepeatKeyPress");
                inKeyPress = false;
                hit.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
                InputButtonUp = false;
            }
            else
            {
                if (!inKeyPress && !focused)
                {
                    // There is a bug in visual update and we have to do the following 
                    //foreach (Transform child in transform)
                    {
                        //if (child != null)
                        //if (child.name != hit.transform.gameObject.name)
                        //if (child.GetComponent<KeyEvents>() != null)

                        //child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    }
                    //////////////////////////////////////////////////////////////

                    hit.transform.parent.GetComponent<KeyEvents>().Key_FocusedEvent();
                    objectInFocus = hit.transform.gameObject;
                    focused = true;
                }
            }
        }
    }

    #region LeftHitHandler_TOBEREMOVED
    private bool inKeyPressLeft = false;
    private bool focusedLeft = false;
    private bool InputLeftButtonDown = false;
    private bool InputLeftButtonHeldDown = false;
    private bool InputLeftButtonUp = false;
    private GameObject objectInFocusLeft;
    
    private void HandleLeftNotHit()
    {
        if (inKeyPressLeft)
        {
            StopCoroutine("RepeatKeyPress");
            inKeyPressLeft = false;
            if (objectInFocusLeft == null)
            {
                // release all
                foreach (var item in keysDic)
                {
                    item.Value.transform.GetComponent<KeyEvents>().Key_ReleaseEvent();
                }
            }
            else
            {
                try
                {
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
                }
                catch (Exception e )
                {
                    Debug.LogError(e.StackTrace);
                }
            }
        }
        else
        {
        }
        if (objectInFocusLeft != null)
        {
            if (objectInFocusLeft.name.Contains("MainShape"))
            {
                //Debug.Log("You realsed the " + objectInFocus.transform.parent.name);
                objectInFocusLeft.transform.parent.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                objectInFocusLeft = null;
                focusedLeft = false;
                // There is a bug in visual update and we have to do the following 
                //foreach (Transform child in transform)
                //{
                //    if(child.Find("MainShape")!=null)
                //    child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                //}
                //////////////////////////////////////////////////////////////
            }
        }
    }

    private void HandleLeftHit(GameObject hit)
    {
        if (hit.transform.name.Contains("MainShape"))
        {
            if (InputLeftButtonDown)
            {
                //Debug.Log("Mouse down on the " + hit.transform.parent.name);
                inKeyPressLeft = true;
                hit.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                StartCoroutine("RepeatKeyPress");
                InputLeftButtonDown = false;
            }
            else if (InputLeftButtonUp)
            {
                //Debug.Log("Mouse up on the " + hit.transform.parent.name);
                StopCoroutine("RepeatKeyPress");
                inKeyPressLeft = false;
                hit.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
                InputLeftButtonUp = false;
            }
            else
            {
                if (!inKeyPressLeft && !focusedLeft)
                {
                    // There is a bug in visual update and we have to do the following 
                    //foreach (Transform child in transform)
                    {
                        //if (child != null)
                        //if (child.name != hit.transform.gameObject.name)
                        //if (child.GetComponent<KeyEvents>() != null)

                        //child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    }
                    //////////////////////////////////////////////////////////////

                    hit.transform.parent.GetComponent<KeyEvents>().Key_FocusedEvent();
                    objectInFocusLeft = hit.transform.gameObject;
                    focusedLeft = true;
                }
            }
        }
    }
    #endregion

    protected bool GetRay(out Ray ray)
    {
        if (_eyeTracker == null)
        {
            ray = default(Ray);
            return false;
        }

        var data = _eyeTracker.LatestGazeData;
        ray = data.CombinedGazeRayWorld;
        return data.CombinedGazeRayWorldValid;
    }

    private void HandleMouseInput()
    {
        RaycastHit hit;
        Ray ray = new Ray(pointer.transform.position, pointer.transform.position - Camera.main.transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            HandleHit(hit.transform.gameObject);
        }
        else
        {
            HandleNotHit();
        }
    }

    private void HandleTouchInput(int action)
    {
        RaycastHit hit;
        Ray ray = new Ray(pointer.transform.position, pointer.transform.position - Camera.main.transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            HandleHit(hit.transform.gameObject);
        }
        else
        {
            HandleNotHit();
        }
    }

    float GetBeamLength(bool bHit, RaycastHit hit, float length, Transform contactTarget, float contactDistance)
    {
        float actualLength = length;

        //reset if beam not hitting or hitting new target
        if (!bHit || (contactTarget && contactTarget != hit.transform))
        {
            contactDistance = 0f;
            contactTarget = null;
        }

        //check if beam has hit a new target
        if (bHit)
        {
            if (hit.distance <= 0)
            {

            }
            contactDistance = hit.distance;
            contactTarget = hit.transform;
        }

        //adjust beam length if something is blocking it
        if (bHit && contactDistance < length)
        {
            actualLength = contactDistance;
        }

        if (actualLength <= 0)
        {
            actualLength = length;
        }

        return actualLength; ;
    }

    public System.Collections.IEnumerator RepeatKeyPress()
    {
        if (RepeatedKeyPressEnabled)
        {
            yield return new WaitForSeconds(0.7f); // initial wait
            while (inKeyPress)
            {
                if (objectInFocus != null)
                {
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }

    public GameObject GetKeyObject(char item)
    {
        if (PRINTABLEKEYS.ContainsValue(item))
            return keysDic[PRINTABLEKEYS.FirstOrDefault(x => x.Value == item).Key];
        return null;
    }

    public void ResetKeyBoardExcept(Transform sender)
    {
        foreach (var item in keysDic)
        {
            if (item.Value != sender.gameObject) 
            item.Value.GetComponent<KeyProperties>().ResetToNormal();
        }
    }

    public void ResetKeyBoard()
    {
        foreach (var item in keysDic)
        {
            item.Value.GetComponent<KeyProperties>().ResetToNormal();
        }
    }

    protected virtual void OnKeyPressed(object sender, KeyEventArgs args)
    {
        KeyEvents.KeyEvent handler = KeyboardLayout_OnKeyPressed;
        if(handler!=null)
            handler.Invoke(sender, args);
    }

    public GameObject KeyInFocus { get; set; }
    public bool InputButtonDown { get; protected set; }
    public bool InputButtonHeldDown { get; protected set; }
    public bool InputButtonUp { get; protected set; }

    public Pointer pointer;

    public KeyboardInputType InputType;

    public bool zoomEffect;
    public bool dwell;
    public float dwellWaitTime = 1;

    public bool suggestionEnabled = false;
    private bool inKeyPress;
    private GameObject objectInFocus;
    public bool RepeatedKeyPressEnabled;
    public TouchDataHandler touchHandler;
    public ViveTrackpad rightTrackpadHandler;
    public ViveTrackpad leftTrackpadHandler;
    private int speed = 5;
    public float pointerDistanceFromCamera = 6;
    public float keyboardDistanceFromCamera = 10;
    private int prevX;
    private int prevY;
    public bool tapActionEnabled;
    private bool focused = false;
    public bool useViveTrackpad;
    public bool useTrackerInputForPointer;
    private GameObject rightDrumStick;
    public float rDrumLength;
    private Transform rDrumContactTarget;
    private float rContactDistance;
    private GameObject leftDrumStick;
    public float lDrumLength;
    private Transform lDrumContactTarget;
    private float lContactDistance;
    private Transform MainCamera;
    private GameObject gazeTrailGameObject;

    public float keyXDelta = 0;
    public float keyYDelta = 0;
    public float keyZDelta = 0;
    public bool updateLayout = false;

    public abstract void SetProperties();

    public abstract void LayoutKeys();

    public abstract void CreateMainKeys();
    
    public abstract void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args);

    public abstract void HighlightKeys(List<char> suggestedAlphabet);
}

public enum KeyID
{
    A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, Space, Space2,
    LAlt, RAlt, Alt, LShift, RShift, Shift, LCtrl, RCtrl, Ctrl,
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
    ESC, CpsLck, Tab, Enter,
    N1, N2, N3, N4, N5, N6, N7, N8, N9, N0,
    Sharp, Underline, Dash, LParathesis, RParathesis, Plus, Star, Backspace, Dollar, At, Exclamation, Hat, Equal, Percent, Ampersand,
    Dot, Quote, DoubleQuote, Slash, BackSlash, Comma, Question, Colon, SemiColon,
    Next, Previous
}

public enum KeyboardInputType
{
    GazeAndDwell,
    GazeAndClick,
    GazeAndRay,
    Ray,
    DrumStick,
    Mouse,
    TouchPad,
    Swype
}
