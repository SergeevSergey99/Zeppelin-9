using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour
{
    [System.NonSerialized]public int i;

    public void DialogueNext()
    {
        transform.parent.GetComponent<DialogueController>().controller.GetComponent<SceneDialogue>().Next();
    }
    public void DialogueAnswer()
    {
        transform.parent.parent.GetComponent<DialogueController>().controller.GetComponent<SceneDialogue>().Next(i);
    }

}
