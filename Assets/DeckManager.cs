using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    List<Card> deck;
    List<Card>[] hands;
    public Transform playerHand;

    public GameObject CardPrefab;


    private void Start()
    {
        playerHand = GameObject.Find("Player Hand").transform;
        hands = initHands();
        deck = GenerateDeck();
        deck = AddCards(deck, GenerateDeck());
        Shuffle(deck);
        DealRound(hands);
        
        foreach(List<Card> hand in hands)
        {
            Debug.Log("In Hand " + hand);
            foreach(Card card in hand)
            {
                Debug.Log(card.value + " " + card.suit);
            }
        }
            
    }

    List<Card>[] initHands()
    {
        List<Card>[] hands = new List<Card>[5];
        for (int i = 0; i < 5; i++)
            hands[i] = new List<Card>();
  
        return hands;
    }

    List<Card> GenerateDeck()
    {
        deck = new List<Card>();

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
    List<Card> AddCards(List<Card> deck1, List<Card> deck2)
    {
        foreach (Card card in deck2)
            deck1.Add(card);
        return deck1;
    }

    void Shuffle(List<Card> deck)
    {
        for(int n = deck.Count; n > 1; )
        {
            n--;
            int k = Random.Range(0, n);
            Card card = deck[k];
            deck[k] = deck[n];
            deck[n] = card;
        }
    }

    void DealRound(List<Card>[] hands)
    {
        while (deck.Count > 8)
        {
            Deal(hands);
        }
    }

    //deals one card to each player
    void Deal(List<Card>[] hands)
    {
        foreach(List<Card> hand in hands)
        {
            hand.Add(deck[0]);

            if (hand == hands[0])
            {
                GameObject newCard = Instantiate(CardPrefab, playerHand);
                newCard.GetComponent<CardManager>().card = deck[0];
                newCard.GetComponent<CardManager>().changeSprite();
            }
            deck.RemoveAt(0);
        }
    }

}
