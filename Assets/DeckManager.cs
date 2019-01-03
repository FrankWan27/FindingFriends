using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    List<Card> deck;

    private void Start()
    {
        GenerateDeck();
    }

    void GenerateDeck()
    {
        deck = new List<Card>();

        // 2-14 (14 is ace)
        foreach (symbol suit in System.Enum.GetValues(typeof(symbol)))
        {
            for (int i = 2; i <= 14; i++)
            {
                deck.Add(new PlayingCard(i, suit));
            }
        }

        deck.Add(new JokerCard(false));
        deck.Add(new JokerCard(false));
        deck.Add(new JokerCard(true));
        deck.Add(new JokerCard(true));
    }

}
