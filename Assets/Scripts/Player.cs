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
        gm.players.Add(this);

    }

    public void Draw()
    {
        if (!isLocalPlayer)
            return;
        CmdDraw();
        CmdIncPlayer();
        
    }

    [Command]
    public void CmdIncPlayer()
    {
        gm.IncrementPlayer();
    }

    [Command]
    public void CmdDraw()
    {
        Card c = dm.Deal();
        gm.RpcDestroyCard();
        RpcDealCard(c);
    }

    //gives Card c to Player p
    [ClientRpc]
    public void RpcDealCard(Card c)
    {
        Debug.Log(c.GetType());

        Pile h = gm.GetPlayer().hand;
        hand.Add(c);
        gm.ResetPlayerHand();
    }

    [Command]
    public void CmdDealCard()
    {
        Card c = dm.Deal();
        RpcDealCard(c);
    }
}
