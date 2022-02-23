using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookScript : MonoBehaviour
{
    public GameObject mover;
    // Start is called before the first frame update
    private GameObject profile;
    void Start()
    {
        
        profile =  GameObject.FindGameObjectsWithTag("Player")[0];
        
    }

    public void ShowNotebook()
    {
        if (mover.GetComponent<Animator>().GetBool("IsOpen"))
        {
            mover.GetComponent<Animator>().Play("Close");
        }
        else
        {
            mover.GetComponent<Animator>().Play("Open");
        }

    }

    public void OpenState()
    {
        
        mover.GetComponent<Animator>().SetBool("IsOpen", true);
    }
public void CLoseState()
{
    mover.GetComponent<Animator>().SetBool("IsOpen", false);
    
}
    
    public void Next()
    {
        profile.GetComponent<Profile>().NextPageInNotebook();
    }
    public void Previous()
    {
        profile.GetComponent<Profile>().PreviousPageInNotebook();
    }
}
