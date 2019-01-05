using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum state { Draw, Burry, Play, Score };

public class GameManager : MonoBehaviour
{

    public state gameState;

    bool revealed = false;
    public symbol trumpSuit = symbol.Spade;
    public int currentLevel = 2;

    public symbol currentSuit = symbol.Spade;
    DeckManager dm;

    public Transform playerHand;

    public GameObject cardPrefab;
    public GameObject card3DPrefab;
    public GameObject emptyPrefab;

    public List<GameObject> selected;

    void Start()
    {
        gameState = state.Draw;
        dm = gameObject.GetComponent<DeckManager>();
        selected = new List<GameObject>();
    }

    public void Draw()
    {
        if(gameState == state.Draw)
            dm.Deal();
        else
            ResetPlayerHand();

        if (dm.deck.Count() <= 8)
            gameState = state.Burry;

    }

    void ResetHandDisplay(Pile hand)
    {
        for (int i = playerHand.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(playerHand.GetChild(i).gameObject);
        }
        foreach (Card c in hand.cards)
        {
            GameObject newCard = Instantiate(cardPrefab, playerHand);
            newCard.GetComponent<CardManager>().card = c;
            newCard.GetComponent<CardManager>().changeSprite();
        }
    }

    public void ResetPlayerHand()
    {
        dm.GetPlayerHand().Sort();
        ResetHandDisplay(dm.GetPlayerHand());


        foreach (GameObject o in selected)
            GameObject.Destroy(o);
        selected.Clear();
    }



    public GameObject SpawnCard(Card c)
    {

        GameObject newCard = Instantiate(card3DPrefab, new Vector3(0, 4f, -5f), Quaternion.Euler(90, 0, 0));

        newCard.GetComponent<CardManager>().card = c;
        newCard.GetComponent<CardManager>().changeSprite();

        return newCard;
    }

    public void SpawnDeck()
    {
        for (int i = 0; i < 108; i++)
        {
            GameObject newCard = Instantiate(card3DPrefab, new Vector3(0, 1 + (0.05f * i), 0), Quaternion.Euler(-90, 0, 0));
        }
    }

    public void LaunchCard(GameObject newCard)
    {
        Rigidbody rb = newCard.GetComponent<Rigidbody>();

        float randX = Random.Range(-4, 4);
        float randY = Random.Range(-5, -2);
        float randZ = Random.Range(6, 12);

        rb.velocity = new Vector3(randX, randY, randZ);
    }

    //Valid move = same suit, if none of same suit, any suit is valid
    public bool ValidMove(Card card, Pile hand)
    {
        if (gameState == state.Draw)
            return ValidReveal(card);
        else if (gameState == state.Play)
            return ValidPlay(card, hand);
        else
            return false;
    }

    bool ValidReveal(Card card)
    {
        if (revealed)
        {
            //figure out how to do doubles
            return false;
        }
        else
        {
            if (card.value == currentLevel)
                return true;
            else
                return false;
        }
    }

    bool ValidPlay(Card card, Pile hand)
    {
        //if we're playing trumps
        if (currentSuit == trumpSuit)
        {
            //check if hand contains trumps
            bool hasTrump = false;
            foreach (Card c in hand.cards)
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

    public void PlayMove(Card card, Pile hand)
    {
        if (gameState == state.Draw)
        {
            revealed = true;
            trumpSuit = card.suit;
            GameObject newCard = SpawnCard(card);
            LaunchCard(newCard);
        }
        else if (gameState == state.Play)
        {
            hand.Remove(card);
            GameObject newCard = SpawnCard(card);
            LaunchCard(newCard);
        }
    }
}
