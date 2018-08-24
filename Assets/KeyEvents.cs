using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvents : MonoBehaviour {
    private bool clicked = false;

    public delegate void KeyEvent(object sender, KeyEventArgs args);
    public event KeyEvent KeyEvents_OnKeyFocused;
    public event KeyEvent KeyEvents_OnKeyUnfocused;
    public event KeyEvent KeyEvents_OnKeyPressed;
    public event KeyEvent KeyEvents_OnKeyReleased;

    public void Key_FocusedEvent()
    {
        KeyEvents_OnKeyFocused?.Invoke(transform, KeyEventArgs.Empty);
    }

    public void Key_UnfocusedEvent()
    {
        KeyEvents_OnKeyUnfocused?.Invoke(transform, KeyEventArgs.Empty);
    }

    public void Key_PressedEvent()
    {
        KeyEventArgs args = new KeyEventArgs();
        args.KeyPrintable = GetComponent<KeyProperties>().IsPrintable;
        args.KeyText = GetComponent<KeyProperties>().KeyText;
        args.KeyId = GetComponent<KeyProperties>().ID;
        KeyEvents_OnKeyPressed?.Invoke(transform, args);
    }

    public void Key_ReleaseEvent()
    {
        KeyEvents_OnKeyReleased?.Invoke(transform, KeyEventArgs.Empty);
    }
}

public class KeyEventArgs: EventArgs
{
    public static KeyEventArgs Empty;

    public bool KeyPrintable { get; internal set; }
    public string KeyText { get; internal set; }
    public KeyID KeyId { get; internal set; }
}