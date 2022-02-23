using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleCanvas : MonoBehaviour
{
 public void DoubleToggleCanvas()
    {
        GameObject.Find("Profile").GetComponent<Profile>().language
            = (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)?
                (Language.EN):(Language.RU);
        GameObject go = GameObject.Find("Canvas").gameObject;
        go.SetActive(false);
        go.SetActive(true);
    }
}
