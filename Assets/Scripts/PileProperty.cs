using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileProperty : MonoBehaviour
{
    public List<CardProperty> cards = new List<CardProperty>();
    public int GetTopCardValue()
    {
        if(cards.Count > 0)
        {
            return cards[cards.Count - 1].CardValue;
        }
        return 0;
    }

    public string GetTopCardColor()
    {
        if(cards.Count > 0)
        {
            return cards[cards.Count - 1].CardColor;
        }
        return string.Empty;
    }

    public void AddCard(CardProperty card)
    {
        cards.Add(card);
        card.transform.SetParent(this.transform);
        card.transform.localPosition = new Vector3(0, -cards.Count * 0.1f, -cards.Count * 0.01f);
    }
}
