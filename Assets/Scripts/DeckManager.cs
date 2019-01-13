using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeckManager : MonoBehaviour
{

    public Pile deck;
    //Pile[] hands;
    public Transform deckObject;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void BeginGame()
    {


        deckObject = GameObject.Find("DeckCopy(Clone)").transform;
        //        hands = initHands();
        deck = SmallDeck();
        //deck = GenerateDeck();
        //deck = AddCards(deck, GenerateDeck());
        Shuffle(deck);
        //DealRound(hands);
    }

    Pile[] initHands()
    {
        Pile[] hands = new Pile[5];
        for (int i = 0; i < 5; i++)
            hands[i] = new Pile();
  
        return hands;
    }

    Pile SmallDeck()
    {
        deck = new Pile();

        for (int i = 2; i <= 14; i++)
            deck.Add(new Card(i, symbol.Diamond));

        return deck;
    }

    Pile GenerateDeck()
    {
        deck = new Pile();

        // 2-14 (14 is ace)
        foreach (symbol suit in System.Enum.GetValues(typeof(symbol)))
        {
            for (int i = 2; i <= 14; i++)
            {
                deck.Add(new Card(i, suit));
            }
        }

        deck.Add(new Card(false));
        deck.Add(new Card(true));

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

    //return a card
    public Card Deal()
    {
        //TODO: Change to 1 by 1 with multiplayer
        // for (int i = 0; i < hands.Length; i++)
        //     GameObject.Destroy(deckObject.GetChild(i).gameObject);

        Card c = deck.Pop();

        return c;  
    }

    public void LandlordBurry()
    {
        while (deck.Count() > 0)
        {
            Card c = deck.Pop();
            gm.GetLandlordHand().Add(c);

            if(gm.GetLandlordHand() == gm.GetPlayer().hand)
            {
                gm.ResetPlayerHand();
            }
        }
    }


}
