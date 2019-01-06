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
    public bool isTrump = false;
    

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

    public int SuitToInt(symbol trump)
    {
        if(trump == symbol.Spade)
        {
            if (suit == symbol.Heart)
                return 0;
            else if (suit == symbol.Club)
                return 1;
            else if (suit == symbol.Diamond)
                return 2;
            else if (suit == symbol.Spade)
                return 3;
            else
                return -1;

        }
        else if(trump == symbol.Heart)
        {
            if (suit == symbol.Spade)
                return 0;
            else if (suit == symbol.Diamond)
                return 1;
            else if (suit == symbol.Club)
                return 2;
            else if (suit == symbol.Heart)
                return 3;
            else
                return -1;
        }
        else if(trump == symbol.Club)
        {
            if (suit == symbol.Heart)
                return 0;
            else if (suit == symbol.Spade)
                return 1;
            else if (suit == symbol.Diamond)
                return 2;
            else if (suit == symbol.Club)
                return 3;
            else
                return -1;
        }
        else
        {
            return SuitToInt();
        }
    }

    public int SuitToInt(symbol trump, int level)
    {
        if (isTrump)
            return 3;
        return SuitToInt(trump);
    }

    public int PlayingValue(symbol trump, int level)
    {
        if (value == level)
        {
            if (suit == trump)
                return SuitToInt(trump, level) * 13 + 16;
            else
                return SuitToInt(trump, level) * 13 + 15;
        }

        return SuitToInt(trump, level) * 13 + value;
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

        isTrump = true;
    }
}


