using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    Pile deck;
    Pile[] hands;
    public Transform playerHand;

    public GameObject cardPrefab;

    GameManager gm;

    private void Start()
    {
        playerHand = GameObject.Find("Player Hand").transform;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        hands = initHands();
        deck = GenerateDeck();
        deck = AddCards(deck, GenerateDeck());
        Shuffle(deck);
        DealRound(hands);

        Pile hand = hands[0];
        foreach(Card card in hand.cards)
        {
            Debug.Log(card.value + " " + card.suit + " " + card.PlayingValue(gm.trumpSuit, gm.currentLevel));
        }
            
    }

    Pile[] initHands()
    {
        Pile[] hands = new Pile[5];
        for (int i = 0; i < 5; i++)
            hands[i] = new Pile();
  
        return hands;
    }

    Pile GenerateDeck()
    {
        deck = new Pile();

        // 2-14 (14 is ace)
        foreach (symbol suit in System.Enum.GetValues(typeof(symbol)))
        {
            for (int i = 2; i <= 14; i++)
            {
                deck.Add(new NumberCard(i, suit));
            }
        }

        deck.Add(new JokerCard(false));
        deck.Add(new JokerCard(true));

        return deck;
    }

    //Adds cards from deck2 to deck1
    Pile AddCards(Pile deck1, Pile deck2)
    {
        foreach (Card card in deck2.cards)
            deck1.Add(card);
        return deck1;
    }

    void Shuffle(Pile deck)
    {
        for(int n = deck.Count(); n > 1; )
        {
            n--;
            int k = Random.Range(0, n);
            Card card = deck.At(k);
            deck.Set(k, deck.At(n));
            deck.Set(n, card);
        }
    }

    void DealRound(Pile[] hands)
    {
        while (deck.Count() > 8)
        {
            Deal(hands);
        }
    }

    //deals one card to each player
    void Deal(Pile[] hands)
    {
        foreach(Pile hand in hands)
        {
            Card c = deck.Pop();

            hand.Add(c);

            if (hand == hands[0])
            {
                ResetPlayerHand();
            }
        }
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
        hands[0].Sort();
        ResetHandDisplay(hands[0]);
    }

    public Pile GetPlayerHand()
    {
        return hands[0];
    }

}
