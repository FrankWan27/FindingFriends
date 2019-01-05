using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public symbol trumpSuit = symbol.Spade;
    public int currentLevel = 2;

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
}
