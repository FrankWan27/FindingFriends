using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gm;
    DeckManager dm;
    public Pile hand;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dm = GameObject.Find("GameManager").GetComponent<DeckManager>();

        dm.players.Add(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
