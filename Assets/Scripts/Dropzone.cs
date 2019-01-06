using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler
{
    DeckManager dm;
    GameManager gm;

    public void Awake()
    {
        dm = GameObject.Find("GameManager").GetComponent<DeckManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        CardManager cm = eventData.pointerDrag.GetComponent<CardManager>();

        

        if (gm.ValidMove(cm.card, dm.GetPlayerHand()))
        {
            gm.PlayMove(dm.GetPlayerHand());
        }
        //d.returnToParent = this.transform;
    }
}
