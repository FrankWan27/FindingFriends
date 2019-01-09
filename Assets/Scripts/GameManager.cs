using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum state { Lobby, Draw, Burry, Play, Score };

public class GameManager : NetworkBehaviour
{
    [SyncVar]
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
    public GameObject deckPrefab;

    public List<GameObject> selected;

    public Text playerCountText;
    public Text currentLevelText;
    public Text trumpSuitText;
    public Text currentSuitText;

    [SyncVar]
    public int playerCount = 0;


    private void Update()
    {
        if(gameState == state.Lobby && isServer)
            playerCount = NetworkServer.connections.Count;

        playerCountText.text = "# of players: " + playerCount;
        currentLevelText.text = "Current level: " + currentLevel;
        trumpSuitText.text = "Trump Suit: " + trumpSuit;
        currentSuitText.text = "Current Suit: " + currentSuit;
    }


    void Start()
    {
        dm = gameObject.GetComponent<DeckManager>();
        selected = new List<GameObject>();
    }

    [ClientRpc]
    public void RpcBeginGame()
    {
        gameState = state.Draw;
        SpawnDeck();
        dm.BeginGame();
        //hide gui
        GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().showGUI = false;
        GameObject.Destroy(GameObject.Find("BeginButton"));
    }

    public void Draw()
    {
        if(gameState == state.Draw)
            dm.Deal();
        else
            ResetPlayerHand();

        if (dm.deck.Count() <= 0)
        {
            gameState = state.Burry;
            dm.LandlordBurry();
        }

    }

    void LandlordBurry()
    {
        
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

        Instantiate(deckPrefab, new Vector3(0, 9, -7), Quaternion.Euler(0, 0, 0));
        
     //   for (int i = 0; i < 108; i++)
     //   {
     //       GameObject newCard = Instantiate(card3DPrefab, new Vector3(0, 1 + (0.05f * i), 0), Quaternion.Euler(-90, 0, 0));
     //   }
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
        Debug.Log("Validating" + card.value);
        if (gameState == state.Draw)
            return ValidReveal();
        else if (gameState == state.Burry)
            return ValidReveal();
        else if (gameState == state.Play)
            return ValidPlay(card, hand);
        else
            return false;
    }

    bool ValidReveal()
    {
        List<Card> cards = SelectedToCards();
        Debug.Log("Revealing" + cards[0].value);

        if (revealed)
        {
            //only thing you can play is double level
            if(selected.Count == 2)
            {
                //TODO: overload == and then just see if equal and value = level
                if (cards[0].value == currentLevel && cards[1].value == currentLevel && cards[0].suit == cards[1].suit)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            if(selected.Count == 1 && cards[0].value == currentLevel)
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

    public void PlayMove(Pile hand)
    {
        List<Card> cards = SelectedToCards();
        if (gameState == state.Draw || gameState == state.Burry)
        {
            revealed = true;
            trumpSuit = cards[0].suit;
            foreach (Card card in cards)
            {
                GameObject newCard = SpawnCard(card);
                LaunchCard(newCard);
            }
        }
        else if (gameState == state.Play)
        {
            foreach (Card card in cards)
            {
                Debug.Log("Playing cards");
                hand.Remove(card);
                GameObject newCard = SpawnCard(card);
                LaunchCard(newCard);
            }
            ResetPlayerHand();
        }
    }

    List<Card> SelectedToCards()
    {
        List<Card> cards = new List<Card>();
        foreach(GameObject o in selected)
        {
            cards.Add(o.GetComponent<CardManager>().card);
        }
        return cards;
    }
}
