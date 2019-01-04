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
    public symbol suit;
    public bool trump = false;

    public int SuitToInt()
    {
        if (suit == symbol.Spade)
            return 0;
        else if (suit == symbol.Heart)
            return 1;
        else if (suit == symbol.Club)
            return 2;
        else if (suit == symbol.Diamond)
            return 3;
        else
            return -1;
    }
}

public class NumberCard : Card
{
    public NumberCard(int i, symbol s)
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


