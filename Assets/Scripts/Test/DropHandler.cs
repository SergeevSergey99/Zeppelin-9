using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler
{
   
   

    public void OnDrop(PointerEventData eventData)
    {
        GameObject tmp = DragHandler.itemBeingDragged;
        foreach (Transform VARIABLE in GameObject.Find("Canvas").transform.Find("Interface").Find("RightButtons").Find("Notebook").Find("Panel"))
        {
            if (VARIABLE.gameObject.name.StartsWith("Clue"))
            {
                VARIABLE.gameObject.GetComponent<DragHandler>().Dropping();
            }
        }

        
        GameObject.Find("ProfileManager").GetComponent<ProfileManager>().
            ShowClue(tmp.GetComponent<Clue>().name);
    }
}
