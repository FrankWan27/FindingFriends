﻿using System.Collections;
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
        Debug.Log(eventData.pointerDrag.name);
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        CardManager cm = eventData.pointerDrag.GetComponent<CardManager>();

        dm.GetPlayerHand().Remove(cm.card);
        GameObject newCard = gm.SpawnCard(cm.card);

        gm.LaunchCard(newCard);

        //d.returnToParent = this.transform;
    }
}
