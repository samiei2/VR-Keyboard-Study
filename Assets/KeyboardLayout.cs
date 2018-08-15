using System.Collections.Generic;
using System.Text;
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

public enum KeyID
{
    A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, Space,
    LAlt, RAlt, Alt, LShift, RShift, Shift, LCtrl, RCtrl, Ctrl,
    F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
    ESC, CpsLck, Tab, Enter,
    N1, N2, N3, N4, N5, N6, N7, N8, N9, N0,
    Sharp, Underline, Dash, LParathesis, RParathesis, Plus, Minus, Multi, Divide, Backspace, Dollar, At, Exclamation, Hat, Equal,
    Dot, Quote, DoubleQuote, Slash, BackSlash, Comma, Question, Colon, SemiColon,
}