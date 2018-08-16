
using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyHighlightEffect : MonoBehaviour
{
    internal Highlights SelectedEffect;

    //public bool colorHighlight;
    //public bool fadeHighlight;
    //public bool gradientHighlight;
    //public bool disablingHighlight;
    //public bool sizeHighlight;

    public void HighlightKeys(List<char> suggestedAlphabet)
    {
        if (SelectedEffect.Equals(Highlights.Size))
        {

        }
    }
}

public enum Highlights
{
    Size, Color, Gradient, Fade, Disabling
}