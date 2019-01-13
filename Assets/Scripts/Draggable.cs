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
        foreach (GameObject o in gm.selected)
        {
            o.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        if (selected)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            returnToParent = this.transform.parent;
            this.transform.SetParent(returnToParent.parent);
            gm.selected.Add(gameObject);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        foreach (GameObject o in gm.selected)
        {
            o.transform.position = eventData.position;
        }
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(returnToParent);
        gm.selected.Remove(gameObject);
        //TODO: return to hand insert in correct position instead of resetting
        this.transform.SetSiblingIndex(gm.GetPlayer().hand.IndexOf(cm.card));
        if (selected)
        {
            selected = false;
            GameObject.Destroy(placeholder);
        }

        for (int i = gm.selected.Count - 1; i >= 0; i--)
        {
            GameObject o = gm.selected[i];
            o.transform.SetParent(returnToParent);
            Draggable d = o.GetComponent<Draggable>();
            o.transform.SetSiblingIndex(gm.GetPlayer().hand.IndexOf(d.cm.card));
            d.selected = false;
            GameObject.Destroy(d.placeholder);
            o.GetComponent<CanvasGroup>().blocksRaycasts = true;
            gm.selected.Remove(o);
        }


        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selected)
        {
            selected = false;
            this.transform.SetParent(returnToParent);
            this.transform.SetSiblingIndex(gm.GetPlayer().hand.IndexOf(cm.card));
            GameObject.Destroy(placeholder);
            gm.selected.Remove(gameObject);
        }
        else 
        {
            selected = true;
            
            returnToParent = this.transform.parent;
            this.transform.SetParent(returnToParent.parent);
            this.transform.Translate(new Vector3(0, 40, 0));
            this.transform.SetAsFirstSibling();

            gm.selected.Add(gameObject);
            
            //placeholder
            placeholder = Instantiate(gm.emptyPrefab, returnToParent);
            placeholder.transform.SetSiblingIndex(gm.GetPlayer().hand.IndexOf(cm.card));

        }
    }
}
