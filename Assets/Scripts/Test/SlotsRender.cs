using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotsRender : MonoBehaviour
{
    [System.NonSerialized] public GameObject p;

    public GameObject NewSlot;
    public GameObject SaveSlot;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Profile");
        Render();
    }

    public void Render()
    {
        int cnt = 1;
        foreach (Transform child in gameObject.transform)
            Destroy(child.gameObject);
        foreach (Slot slot in p.GetComponent<Profile>().slots)
        {
            if (slot.Scene.Equals(""))
            {
                if (!SceneManager.GetActiveScene().name.Equals("Main Menu"))
                {
                    GameObject save = Instantiate(NewSlot, transform);
                    save.GetComponent<SaveButton>().i = cnt;
                }
            }
            else
            {
                GameObject save = Instantiate(SaveSlot, transform);
                save.transform.Find("ChapterPanel").Find("ChapterText").GetComponent<TextMeshProUGUI>().text = slot.Scene;
                save.transform.Find("DateInfo").GetComponent<TextMeshProUGUI>().text = "" + slot.dateTime;
                save.transform.Find("RepInfo").GetComponent<TextMeshProUGUI>().text = "" + slot.Reputation;
                save.GetComponent<SaveButton>().i = cnt;
            }

            cnt++;

        }

        

    }

}
