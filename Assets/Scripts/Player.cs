using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    GameManager gm;
    DeckManager dm;
    public Pile hand;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dm = GameObject.Find("GameManager").GetComponent<DeckManager>();

        hand = new Pile();
        dm.players.Add(this);

    }

    public void Draw(Card c)
    {
        if (!isLocalPlayer)
            return;

        hand.Add(c);
        gm.ResetPlayerHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
