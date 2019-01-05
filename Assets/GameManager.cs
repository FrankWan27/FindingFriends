using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public symbol trumpSuit = symbol.Spade;
    public int currentLevel = 2;

    public symbol currentSuit = symbol.Spade;


    public GameObject cardPrefab;

    public GameObject SpawnCard(Card c)
    {
        GameObject newCard = Instantiate(cardPrefab);
        newCard.GetComponent<CardManager>().card = c;
        newCard.GetComponent<CardManager>().changeSprite();

        return newCard;
    }

    public void LaunchCard(GameObject newCard)
    {
        Rigidbody rb = newCard.GetComponent<Rigidbody>();

        float randX = Random.Range(-4, 4);
        float randY = Random.Range(-5, -2);
        float randZ = Random.Range(10, 8);

        rb.velocity = new Vector3(randX, randY, randZ);
    }

    //Valid move = same suit, if none of same suit, any suit is valid
    public bool ValidMove(Card card, Pile hand)
    {
        //if we're playing trumps
        if(currentSuit == trumpSuit)
        {
            //check if hand contains trumps
            bool hasTrump = false;
            foreach(Card c in hand.cards)
            {
                if (c.isTrump)
                {
                    hasTrump = true;
                    break;
                }
            }

            if (hasTrump)
            {
                //must play trump
                if (card.isTrump)
                    return true;
                else
                    return false;
            }
            else
                return true;

        }
        else
        {
            //check if hand contains suit
            bool hasSuit = false;
            foreach (Card c in hand.cards)
            {
                if (c.suit == currentSuit)
                {
                    hasSuit = true;
                    break;
                }
            }

            if (hasSuit)
            {
                //must play suit
                if (card.suit == currentSuit)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
    }
}
