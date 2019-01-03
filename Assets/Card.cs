using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum symbol { Spade, Heart, Club, Diamond };

/*  A = 14  
 *  Level = 15
 *  Trump Level = 16
 *  Small Joker = 17
 *  Big Joker = 18
 */
public class Card
{
    public int value;

    public bool trump = false;
}

public class PlayingCard : Card
{
    symbol suit;

    public PlayingCard (int i, symbol s)
    {
        value = i;
        suit = s;
    }
}

public class JokerCard : Card
{
    public bool big;
    public JokerCard(bool b)
    {
        big = b;
        if (big)
            value = 18;
        else
            value = 17;
        trump = true;
    }
}


