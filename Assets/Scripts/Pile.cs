﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//List of cards = Pile (Hands and Deck)
public class Pile
{
    public List<Card> cards;
    GameManager gm;


    public Pile()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cards = new List<Card>();
    }

    public void Sort()
    {
        UpdateValues();

        //Keep BRBR or RBRB order
        
        for(int i = 0; i < Count(); i++)
        {
            Card c = At(i);
            for(int j = 0; j < i; j++)
            {
                if(c.PlayingValue(gm.trumpSuit, gm.currentLevel) < At(j).PlayingValue(gm.trumpSuit, gm.currentLevel))
                {
                    cards.RemoveAt(i);
                    cards.Insert(j, c);
                    break;
                }
            }

        }
    }

    

    public void UpdateValues()
    {
        foreach (Card card in cards)
        {
            if (card.suit == gm.trumpSuit)
                card.trump = true;
            else
                card.trump = false;

            if(card.value == gm.currentLevel)
            {
                card.trump = true;
                if (card.suit == gm.trumpSuit)
                    card.value = 16;
                else
                    card.value = 15;

            }
        }
    }

    public void Add(Card card)
    {
        cards.Add(card);
    }

    public int Count()
    {
        return cards.Count;
    }

    public Card Pop()
    {
        Card c = cards[0];
        cards.RemoveAt(0);
        return c;
    }

    public Card At(int i)
    {
        return cards[i];
    }

    public void Set(int i, Card c)
    {
        cards[i] = c;
    }
}