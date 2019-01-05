using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    DeckManager dm;
    GameManager gm;
    CardManager cm;
    public Transform returnToParent = null;

    bool selected = false;

    GameObject placeholder;

    public void Awake()
    {
        dm = GameObject.Find("GameManager").GetComponent<DeckManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cm = gameObject.GetComponent<CardManager>();
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
        //TODO: return to hand insert in correct position instead of resetting
        gm.ResetPlayerHand();

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selected)
        {
            selected = false;
            this.transform.SetParent(returnToParent);
            this.transform.SetSiblingIndex(dm.GetPlayerHand().IndexOf(cm.card));
            GameObject.Destroy(placeholder);
            gm.selected.Remove(gameObject);
        }
        else 
        {
            selected = true;
            
            returnToParent = this.transform.parent;
            this.transform.SetParent(returnToParent.parent);
            this.transform.Translate(new Vector3(0, 50, 0));
            this.transform.SetAsFirstSibling();

            gm.selected.Add(gameObject);
            
            //placeholder
            placeholder = Instantiate(gm.emptyPrefab, returnToParent);
            placeholder.transform.SetSiblingIndex(dm.GetPlayerHand().IndexOf(cm.card));

        }
    }
}
