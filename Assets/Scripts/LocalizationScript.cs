using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Language
{
    RU,
    EN
};
public class LocalizationScript : MonoBehaviour
{
    [TextArea]
    public String RU_Str = "";
    [TextArea]
    public String EN_Str = "";

    private void OnEnable()
    {
        if (GameObject.Find("Profile").GetComponent<Profile>().language == Language.RU)
        {
            if(GetComponent<Text>())
                GetComponent<Text>().text = RU_Str;
            if(GetComponent<TextMeshPro>())
                GetComponent<TextMeshPro>().text = RU_Str;
            if(GetComponent<TextMeshProUGUI>())
                GetComponent<TextMeshProUGUI>().text = RU_Str;

        }
        else if(GameObject.Find("Profile").GetComponent<Profile>().language == Language.EN)
        {
            
            if(GetComponent<Text>())
                GetComponent<Text>().text = EN_Str;
            if(GetComponent<TextMeshProUGUI>())
                GetComponent<TextMeshProUGUI>().text = EN_Str;
            if(GetComponent<TextMeshPro>())
                GetComponent<TextMeshPro>().text = EN_Str;
        }
    }

}
