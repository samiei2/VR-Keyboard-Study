using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitalyKeyboardLayout : KeyboardLayout {

    // Use this for initialization
    void Start()
    {
        CreateMainKeys();
        LayoutKeys();
        SetProperties();
    }

    public override void CreateMainKeys()
    {
        throw new NotImplementedException();
    }

    public override void LayoutKeys()
    {
        throw new NotImplementedException();
    }

    public override void SetProperties()
    {
        throw new NotImplementedException();
    }

    public override void KeyboardEventHandler_OnFocusedHandler(object sender, KeyEventArgs args)
    {
        throw new NotImplementedException();
    }

    public override void KeyboardEventHandler_OnPressedHandler(object sender, KeyEventArgs args)
    {
        throw new NotImplementedException();
    }

    public override void KeyboardEventHandler_OnReleasedHandler(object sender, KeyEventArgs args)
    {
        throw new NotImplementedException();
    }

    public override void KeyboardEventHandler_OnUnfocusedHandler(object sender, KeyEventArgs args)
    {
        throw new NotImplementedException();
    }

    public override void HighlightKeys(List<char> suggestedAlphabet)
    {
        throw new NotImplementedException();
    }
}
