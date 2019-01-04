using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    DeckManager dm;
    public Transform returnToParent = null;


    public void Awake()
    {
       dm = GameObject.Find("GameManager").GetComponent<DeckManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        returnToParent = this.transform.parent;
        this.transform.SetParent(returnToParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(returnToParent);
        //TODO: return to hand insert in correct position

        dm.ResetPlayerHand();

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }



}
