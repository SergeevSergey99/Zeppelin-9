using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

public class SceneStartDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        gameObject.GetComponent<SceneDialogue>().StartDialogue();
    }

}