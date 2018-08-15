using System.Collections.Generic;
using UnityEngine;

public abstract class KeyboardLayout: MonoBehaviour
{
    public event KeyEvents.KeyEvent KeyboardLayout_OnKeyPressed;

    protected virtual void OnKeyPressed(object sender, KeyEventArgs args)
    {
        KeyEvents.KeyEvent handler = KeyboardLayout_OnKeyPressed;
        handler?.Invoke(sender, args);
    }

    public GameObject KeyInFocus { get; set; }

    public bool zoomEffect;

    public abstract void SetProperties();
    public abstract void LayoutKeys();

    public abstract void CreateMainKeys();

    public abstract void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args);
    public abstract void HighlightKeys(List<char> suggestedAlphabet);
}