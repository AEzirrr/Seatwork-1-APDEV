using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEventArgs : EventArgs
{
    private Touch _trackedFinger;
    private GameObject _hitObject;

    public Touch TrackedFinger
    {
        get { return this._trackedFinger; }
        set { this._trackedFinger = value; }
    }
    public GameObject HitObject
    {
        get { return this._hitObject; }
        set { this._hitObject = value; }
    }

    public DragEventArgs(Touch trackedFinger, GameObject hitObject = null)
    {
        this._trackedFinger = trackedFinger;
        this._hitObject = hitObject;
    }
}
