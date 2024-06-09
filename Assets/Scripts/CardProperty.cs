using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProperty : MonoBehaviour
{
    [SerializeField]  private string _cardColor;
    [SerializeField]  private string _cardShape;
    [SerializeField]  private int _cardValue;
    [SerializeField] private bool _isFaceUp; // Face-up state tracker
    [SerializeField] private GameObject faceUpObject; // Face-up state object
    [SerializeField] private GameObject faceDownObject; // Face-down state object


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
    public bool IsFaceUp
    {
        get { return this._isFaceUp; }
    }

    public void SetFaceUp(bool faceUp)
    {
        this._isFaceUp = faceUp;
        faceUpObject.SetActive(faceUp);
        faceDownObject.SetActive(!faceUp);
    }
}

