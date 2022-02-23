using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class SearchingButton : MonoBehaviour
{
    // Start is called before the first frame update
   
    
    
    private bool isSearching = false;

    public bool GetSearching()
    {
        return isSearching;
    }
    private GameObject dialugue;
    private GameObject background;
    private GameObject clues;
    private GameObject characters;
     void Start()
        {
            
            foreach (var VARIABLE in gameObject.scene.GetRootGameObjects())
            {
                if (VARIABLE.name == "Canvas")
                {
                    background = VARIABLE.transform.Find("Background").gameObject;
                    clues = background.transform.Find("Clues").gameObject;
                    dialugue = VARIABLE.transform.Find("Dialog").gameObject;
                    characters = background.transform.Find("Characters").gameObject;
                }
            }
        }
    public void Search()
    {
        if (!isSearching)
        {
            if (!gameObject.transform.parent.parent.GetComponent<Animator>().GetBool("IsOpen"))
            {
                
            dialugue.SetActive(false);
            characters.SetActive(false);
            gameObject.GetComponent<Image>().color = Color.yellow;
                //FFE300
            gameObject.transform.parent.parent.Find("Notebook").gameObject.SetActive(false);
            gameObject.transform.parent.parent.Find("Map").gameObject.SetActive(false);
            foreach (Transform child in clues.transform)
            {
                if(child.gameObject.GetComponent<Button>() != null)
                    child.gameObject.GetComponent<Button>().interactable = true;
            }

            isSearching = true;
            
            }
        }
        else
        {
            
            gameObject.GetComponent<Image>().color = Color.black;
            characters.SetActive(true);
            //dialugue.SetActive(true);
            gameObject.transform.parent.parent.Find("Notebook").gameObject.SetActive(true);
            gameObject.transform.parent.parent.Find("Map").gameObject.SetActive(true);

            foreach (Transform child in clues.transform)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }

            isSearching = false;
            
        }
    }

}
