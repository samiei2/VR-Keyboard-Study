using System;
using UnityEngine;

public abstract class KeyEffects : MonoBehaviour
{
    private bool _Activated;

    public abstract void Apply();

    public void Update()
    {
        if(_Activated)
            Apply();
    }

    internal void Activate()
    {
        this._Activated = true;
    }

    internal void Deactivate()
    {
        this._Activated = false;
    }
}