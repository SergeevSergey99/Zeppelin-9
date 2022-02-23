using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popEvent : MonoBehaviour
{
   public void Close()
   {
      gameObject.GetComponent<Animator>().Play("EventMoveFromCenter");
      gameObject.SetActive(false);
      gameObject.transform.parent.gameObject.SetActive(false);
   }
}
