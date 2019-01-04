using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//List of cards = Pile (Hands and Deck)
public class Pile
{
    public List<Card> cards;

    public Pile()
    {
        cards = new List<Card>();
    }

    public void Add(Card card)
    {
        cards.Add(card);
    }

    public int Count()
    {
        return cards.Count;
    }

    public Card Pop()
    {
        Card c = cards[0];
        cards.RemoveAt(0);
        return c;
    }

    public Card At(int i)
    {
        return cards[i];
    }

    public void Set(int i, Card c)
    {
        cards[i] = c;
    }
}
