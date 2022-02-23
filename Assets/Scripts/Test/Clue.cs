using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clue : MonoBehaviour
{
    public string name;
    public string nameEN;
    public Sprite AlternativeImage;
    [TextArea]
    public string description;
    public string descriptionEN;

    public bool isSeen = false;
    private GameObject CanvasArea;
  /*  
    void Start()
    {
        foreach (var VARIABLE in gameObject.scene.GetRootGameObjects())
        {
            if (VARIABLE.name == "Canvas")
            {
                CanvasArea = VARIABLE;
            }
        }
         
        //StartDialogue();
    }
*/
  /*
    public void Zoom()
    {
        CanvasArea.transform.Find("ZoomPanel").gameObject.SetActive(true);
        CanvasArea.transform.Find("ZoomPanel").Find("Image").gameObject.GetComponent<Image>().sprite =
            gameObject.GetComponent<Image>().sprite;

    }
    */
  /*
    public void AddToCluesList()
    {
        CanvasArea.transform.Find("ZoomPanel").gameObject.SetActive(false);
        if (isSeen)
        {
            return;
        }
        foreach (var VARIABLE in  gameObject.scene.GetRootGameObjects())
        {
            if (VARIABLE.name == "Profile")
            {
                VARIABLE.GetComponent<Profile>().Clues.Add(gameObject);
                VARIABLE.GetComponent<Profile>().UpdateNotebook();
                VARIABLE.GetComponent<Profile>().seenObjects.Add(gameObject.scene.name + " " + gameObject.name + " " + name);
                
                var colors = gameObject.GetComponent<Image>().color;
                colors = new Color(
                    colors.r,
                    colors.g,
                    colors.b,
                    0.5f);
                
                GetComponent<Image> ().color = colors;
                isSeen = true;
            }
            
        }
       
    }
    public void AddToSeenList()
    {
        CanvasArea.transform.Find("ZoomPanel").gameObject.SetActive(false);
        if (isSeen)
        {
            return;
        }
        foreach (var VARIABLE in  gameObject.scene.GetRootGameObjects())
        {
            if (VARIABLE.name == "Profile")
            {
                VARIABLE.GetComponent<Profile>().seenObjects.Add(gameObject.scene.name + " " + gameObject.name + " " + name);
       
                var colors = gameObject.GetComponent<Image>().color;
                colors = new Color(
                    colors.r,
                    colors.g,
                    colors.b,
                    0.5f);
                GetComponent<Image> ().color = colors;
                isSeen = true;
            }
            
        }
       
    }*/

}
