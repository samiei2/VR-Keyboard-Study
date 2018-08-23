
using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyHighlightEffect : MonoBehaviour
{
    public Highlights SelectedEffect;

    //public bool colorHighlight;
    //public bool fadeHighlight;
    //public bool gradientHighlight;
    //public bool disablingHighlight;
    //public bool sizeHighlight;
    public Material highlightMaterial;

    public void HighlightKeys(List<char> suggestedAlphabet)
    {
        foreach (var item in suggestedAlphabet)
        {
            var gobject = GetComponent<KeyboardLayout>().GetKeyObject(item);
            if (SelectedEffect.Equals(Highlights.Color))
            {
                if (highlightMaterial != null)
                    gobject.transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = highlightMaterial;
                else
                    gobject.transform.Find("HexCylinder").GetComponent<MeshRenderer>().material = gobject.GetComponent<KeyProperties>().highlightMat;
            }
            if (SelectedEffect.Equals(Highlights.Size))
            {

            }
        }
    }
}

public enum Highlights
{
    Size, Color, Gradient, Fade, Disabling
}