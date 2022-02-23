using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaIgnore : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

}