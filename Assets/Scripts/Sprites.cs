using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sprites : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[55];

    public int CardToSprite(Card card)
    {
        if (card.isJoker)
        {
            if (card.big)
                return 53;
            else
                return 52;
        }
        else
            return card.SuitToInt() * 13 + (card.value - 2);
    }

}
