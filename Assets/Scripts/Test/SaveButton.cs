using Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButton : MonoBehaviour
{
    [System.NonSerialized] public int i;


    public void DoAction()
    {
        if (SceneManager.GetActiveScene().name.Equals("Main Menu"))
        {
            LoadSave();
        }
        else
        {
            CreateSave();
        }
    }
    public void CreateSave()
    {
        transform.parent.GetComponent<SlotsRender>().p.GetComponent<Profile>().SaveProfile(i);
        transform.parent.GetComponent<SlotsRender>().Render();
    }
    public void DeleteSave()
    {
        transform.parent.GetComponent<SlotsRender>().p.GetComponent<Profile>().DeleteProfile(i);
        transform.parent.GetComponent<SlotsRender>().Render();
    }
    public void LoadSave()
    {
        Debug.Log(i);
        transform.parent.GetComponent<SlotsRender>().p.GetComponent<Profile>().LoadProfile(i);
    }
    
}