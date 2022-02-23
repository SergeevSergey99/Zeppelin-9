using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject itemBeingDragged;

    private Vector3 startPosition;

    private Transform startParent;
    Vector2 limits = Vector2.zero;
    Vector2 dragOffset = Vector2.zero;
    public void ShowClue()
    {
        //GameObject.Find("ProfileManager").GetComponent<ProfileManager>().ShowClueEvent(transform.Find("Text").GetComponent<Text>().text);
        GameObject.Find("ProfileManager").GetComponent<ProfileManager>().ShowClueEvent(gameObject.GetComponent<Clue>().name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.localPosition;
        startParent = transform.parent;
        
        dragOffset = eventData.position - (Vector2)transform.position;
        limits = transform.parent.GetComponent<RectTransform>().rect.max;
        
        //transform.SetParent(GameObject.Find("Canvas").transform);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*Debug.Log((Input.mousePosition));
        transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        */
        Vector2 tmp = new Vector2(eventData.position.x / Screen.width * 1920, eventData.position.y / Screen.height*1080);
        tmp =((tmp * transform.lossyScale.x) - 
         new Vector2(transform.parent.position.x, transform.parent.position.x / 2/*(Screen.width*1.0f/Screen.height)*/)) * 2;
        transform.position = new Vector3(tmp.x, tmp.y, 0.0f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.0f);
//        Debug.Log(transform.position+"\t"+(transform.parent.position));
    }

    public void Dropping()
    {
        
        dragOffset = Vector2.zero;
        if (GetComponent<CanvasGroup>().blocksRaycasts)
        {
            return;
        }

        itemBeingDragged = null;
        transform.SetParent(startParent);
        transform.localPosition = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dropping();
    }
}
