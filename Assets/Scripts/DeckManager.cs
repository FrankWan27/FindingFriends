using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Pile deck;
    Pile[] hands;
    public Transform deckObject;
    GameManager gm;

    private void Start()
    {
        deckObject = GameObject.Find("Deck").transform;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        hands = initHands();
        deck = GenerateDeck();
        deck = AddCards(deck, GenerateDeck());
        Shuffle(deck);
        //DealRound(hands);

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

    //deprecated
    void DealRound(Pile[] hands)
    {
        while (deck.Count() > 8)
        {
            Deal();
        }
    }

    //deals one card to each player
    public void Deal()
    {
        //TODO: Change to 1 by 1 with multiplayer
        for (int i = 0; i < hands.Length; i++)
            GameObject.Destroy(deckObject.GetChild(i).gameObject);

        foreach (Pile hand in hands)
        {
            Card c = deck.Pop();
           

            hand.Add(c);

            if (hand == GetPlayerHand())
            {
                gm.ResetPlayerHand();
            }
        }
    }

    public void LandlordBurry()
    {
        while (deck.Count() > 0)
        {
            Card c = deck.Pop();
            GetLandlordHand().Add(c);

            if(GetLandlordHand() == GetPlayerHand())
            {
                gm.ResetPlayerHand();
            }
        }
    }

    public Pile GetPlayerHand()
    {
        return hands[0];
    }

    public Pile GetLandlordHand()
    {
        return hands[0];
    }
}
