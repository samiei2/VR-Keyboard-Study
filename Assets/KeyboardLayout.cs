using UnityEngine;

public abstract class KeyboardLayout: MonoBehaviour
{
    public GameObject KeyInFocus { get; set; }

    public bool zoomEffect;

    public abstract void SetProperties();
    public abstract void LayoutKeys();

    public abstract void CreateMainKeys();

    public abstract void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args);

    public abstract void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args);
}