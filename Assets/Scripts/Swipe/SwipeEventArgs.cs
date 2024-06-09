using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEventArgs : EventArgs
{
    private ESwipeDirection _direction;
    private Vector2 _rawDirection;
    private Vector2 _position;
    private GameObject _hitObject;
    private string _cardColor;
    private string _cardShape;
    private int _cardValue;

    public GameObject HitObject
    {
        get { return this._hitObject; }
        set { this._hitObject = value; }
    }

    public ESwipeDirection Direction
    {
        get { return this._direction; }
        set { this._direction = value; }
    }

    public string CardColor
    {
        get { return this._cardColor; }
        set { this._cardColor = value; }
    }

    public string CardShape
    {
        get { return this._cardShape; }
        set { this._cardShape = value; }
    }

    public int CardValue
    {
        get { return this._cardValue; }
        set { this._cardValue = value; }
    }



    public SwipeEventArgs(ESwipeDirection direction, Vector2 rawDirection, Vector2 position, string cardColor, string cardShape, int cardValue, GameObject hitObject = null)
    {
        this._direction = direction;
        this._rawDirection = rawDirection;
        this._position = position;  
        this._cardColor = cardColor;
        this._cardShape = cardShape;
        this._cardValue = cardValue;

    }
}