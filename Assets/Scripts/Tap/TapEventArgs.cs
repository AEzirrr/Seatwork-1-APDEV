using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to hold event data for tap gestures
public class TapEventArgs : EventArgs
{
    // Private field to store the position of the tap
    private Vector3 _position;
    private GameObject _hitObject;

    // Public property to access the position of the tap
    public Vector3 Position
    {
        get { return this._position; }
        set { this._position = value; }
    }
    public GameObject HitObject
    {
        get { return this._hitObject; }
        set { this._hitObject = value; }
    }

    // Constructor to initialize the TapEventArgs with the position of the tap
    public TapEventArgs(Vector2 position, GameObject hitObject = null)
    {
        // Convert the 2D position to a 3D position (with z = 0)
        this._position = position;
        this._hitObject = hitObject;
    }
}
