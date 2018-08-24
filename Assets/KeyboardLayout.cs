using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class KeyboardLayout : MonoBehaviour
{
    protected Dictionary<KeyID, GameObject> keysDic = new Dictionary<KeyID, GameObject>();
    public event KeyEvents.KeyEvent KeyboardLayout_OnKeyPressed;

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
        CreateMainKeys();
        LayoutKeys();
        SetProperties();
        if (touchHandler != null)
        {
            touchHandler.TouchDataReceivedEvent += Pointer_PointerDataReceivedEvent;
        }
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
                    HandleTouchInput(action);
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
                    position.z = Camera.main.transform.position.z + distanceFromCamera;
                    pointer.transform.position = position;

                    prevX = x;
                    prevY = y;
                }
            }
        });


    }

    public virtual void Update()
    {
        if (InputType == KeyboardInputType.Mouse)
        {
            HandleMouseInput();

            if (pointer != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = distanceFromCamera;

                Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);

                Vector3 position = Vector3.Lerp(pointer.transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));

                pointer.transform.position = position;
            }
        }

        if (InputType == KeyboardInputType.TouchPad)
        {
            HandleTouchInput(-1);
        }

        if(InputType == KeyboardInputType.GazeAndDwell || InputType == KeyboardInputType.GazeAndClick)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsRecent()) // Use IsValid property instead to process old but valid data
            {
                // Note: Values can be negative if the user looks outside the game view.
                //print("Gaze point on Screen (X,Y): " + gazePoint.Screen.x + ", " + gazePoint.Screen.y);
            }
        }


        // Lock boundaries
        Vector3 screenPos = Camera.main.WorldToScreenPoint(pointer.transform.position);
        //ebug.Log("X: " + screenPos.x+", Y: "+screenPos.y);

        if (screenPos.x < 0 || screenPos.y < 0 ||
            screenPos.x > Screen.width || screenPos.y > Screen.height)
        {
            float x = screenPos.x;
            float y = screenPos.y;
            float z = screenPos.z;

            if (x < 0)
                x = 0;
            if (x > Screen.width)
                x = Screen.width;
            if (y < 0)
                y = 0;
            if (y > Screen.height)
                y = Screen.height;
            //Debug.LogError("X: " + x + ", Y: " + y);
            pointer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, z));
            //transform.position = Camera.main.ScreenToWorldPoint(newPos);
        }
    }

    private void HandleMouseInput()
    {
        RaycastHit hit;
        Ray ray = new Ray(pointer.transform.position, pointer.transform.position - Camera.main.transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.name.Contains("MainShape"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log("Mouse down on the " + hit.transform.parent.name);
                    inKeyPress = true;
                    hit.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                    StartCoroutine("RepeatKeyPress");
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    //Debug.Log("Mouse up on the " + hit.transform.parent.name);
                    StopCoroutine("RepeatKeyPress");
                    inKeyPress = false;
                    hit.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
                }
                else
                {
                    if (!inKeyPress)
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
                    }
                }
            }
        }
        else
        {
            if (inKeyPress)
            {
                StopCoroutine("RepeatKeyPress");
                inKeyPress = false;
                objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
            }
            if (objectInFocus != null)
            {
                if (objectInFocus.name.Contains("MainShape"))
                {
                    //Debug.Log("You realsed the " + objectInFocus.transform.parent.name);
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    objectInFocus = null;
                    // There is a bug in visual update and we have to do the following 
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    }
                    //////////////////////////////////////////////////////////////
                }
            }
        }
    }

    private void HandleTouchInput(int action)
    {
        RaycastHit hit;
        Ray ray = new Ray(pointer.transform.position, pointer.transform.position - Camera.main.transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.name.Contains("MainShape"))
            {
                if (action == 0 && tapActionEnabled)
                {
                    //Debug.Log("Mouse down on the " + hit.transform.parent.name);
                    inKeyPress = true;
                    hit.transform.parent.GetComponent<KeyEvents>().Key_PressedEvent();
                    StartCoroutine("RepeatKeyPress");
                }
                else if (action == 1 && tapActionEnabled)
                {
                    //Debug.Log("Mouse up on the " + hit.transform.parent.name);
                    StopCoroutine("RepeatKeyPress");
                    inKeyPress = false;
                    hit.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
                }
                else
                {
                    if (!inKeyPress)
                    {
                        // There is a bug in visual update and we have to do the following 
                        //foreach (Transform child in transform)
                        {
                            //if (child != hit.transform.gameObject)
                                //if (child != null)
                                    //if (child.GetComponent<KeyEvents>() != null)
                                        //child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                        }
                        //////////////////////////////////////////////////////////////

                        hit.transform.parent.GetComponent<KeyEvents>().Key_FocusedEvent();
                        objectInFocus = hit.transform.gameObject;
                    }
                }
            }
        }
        else
        {
            if (inKeyPress)
            {
                StopCoroutine("RepeatKeyPress");
                inKeyPress = false;
                objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_ReleaseEvent();
            }
            if (objectInFocus != null)
            {
                if (objectInFocus.name.Contains("MainShape"))
                {
                    //Debug.Log("You realsed the " + objectInFocus.transform.parent.name);
                    objectInFocus.transform.parent.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    objectInFocus = null;
                    // There is a bug in visual update and we have to do the following 
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<KeyEvents>().Key_UnfocusedEvent();
                    }
                    //////////////////////////////////////////////////////////////
                }
            }
        }
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

    protected virtual void OnKeyPressed(object sender, KeyEventArgs args)
    {
        KeyEvents.KeyEvent handler = KeyboardLayout_OnKeyPressed;
        if(handler!=null)
            handler.Invoke(sender, args);
    }

    public GameObject KeyInFocus { get; set; }
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
    private int speed = 5;
    public float distanceFromCamera = 6;
    private int prevX;
    private int prevY;
    public bool tapActionEnabled;

    public abstract void SetProperties();

    public abstract void LayoutKeys();

    public abstract void CreateMainKeys();

    public abstract void ResetKeyBoard();

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
    Mouse,
    TouchPad
}
