using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serializable class to hold tap properties for gesture recognition
[Serializable]
public class TapProperty
{
    // Private field to store the maximum allowable time for a tap gesture
    [SerializeField] private float _time = 0.7f;

    // Public property to access and modify the maximum allowable time for a tap gesture
    public float Time
    {
        get { return this._time; }
        set { this._time = value; }
    }

    // Private field to store the maximum allowable distance for a tap gesture
    [SerializeField] private float _maxDistance = 0.1f;

    // Public property to access and modify the maximum allowable distance for a tap gesture
    public float MaxDistance
    {
        get { return this._maxDistance; }
        set { this._maxDistance = value; }
    }
}
