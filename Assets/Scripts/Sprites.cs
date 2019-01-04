using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sprites : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[55];

    public int CardToSprite(Card card)
    {
        if (card.GetType() == typeof(NumberCard))
        {
            return card.SuitToInt() * 13 + (card.value - 2);
        }
        else if (card.GetType() == typeof(JokerCard))
        {
            if (((JokerCard)card).big)
                return 53;
            else
                return 52;
        }
        else
            return 54;
    }

}
