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
        Image img = this.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        img.sprite = spriteLib.sprites[spriteLib.CardToSprite(card)];
    }
}
