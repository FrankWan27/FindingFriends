using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CardManager : MonoBehaviour
{
    public Card card;
    public Sprites spriteLib;

    private void Awake()
    {
        spriteLib = GameObject.Find("GameManager").GetComponent<Sprites>();
    }
    public void changeSprite()
    {
        Image img = this.transform.GetChild(0).GetComponent<Image>();
        if (img != null)
        {
            img.sprite = spriteLib.sprites[spriteLib.CardToSprite(card)];
        }
        else
        {
            SpriteRenderer sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.sprite = spriteLib.sprites[spriteLib.CardToSprite(card)];
        }
    }

    
}
