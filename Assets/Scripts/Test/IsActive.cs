using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    ХОТЯ_БЫ_ОДИН_ФЛАГ_СООТВЕТСТВУЕТ,
    ОБЯЗАТЕЛЬНО_ВСЕ_ФЛАГИ_СООТВЕТСТВУЕТ
}
public class IsActive : MonoBehaviour
{
    public Mode mode;
    public List<FlagVariable> flags;
    // Start is called before the first frame update
    void Start()
    {
        StartActiveSetter();
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
    public void ActiveSetter()
    {
        GameObject pm = GameObject.Find("ProfileManager");

        if (mode == Mode.ХОТЯ_БЫ_ОДИН_ФЛАГ_СООТВЕТСТВУЕТ)
        {
            foreach (FlagVariable flag in flags)
            {
                if (pm.GetComponent<ProfileManager>().isFlagExist(flag.name) 
                    && pm.GetComponent<ProfileManager>().IsFlagRaised(flag.name) == flag.isRaised)
                {
                    gameObject.SetActive(true);
                    return;
                }
            }
        }
        else
        {
            foreach (FlagVariable flag in flags)
            {
                if (!(pm.GetComponent<ProfileManager>().isFlagExist(flag.name) 
                      && pm.GetComponent<ProfileManager>().IsFlagRaised(flag.name) == flag.isRaised))
                {
                    gameObject.GetComponent<Animator>().Play("Disepiar");
                    return;
                }
            }
            gameObject.SetActive(true);
        }
        gameObject.GetComponent<Animator>().Play("Disepiar");
    }
    public void StartActiveSetter()
    {
        GameObject pm = GameObject.Find("ProfileManager");

        if (mode == Mode.ХОТЯ_БЫ_ОДИН_ФЛАГ_СООТВЕТСТВУЕТ)
        {
            foreach (FlagVariable flag in flags)
            {
                if (pm.GetComponent<ProfileManager>().isFlagExist(flag.name) 
                    && pm.GetComponent<ProfileManager>().IsFlagRaised(flag.name) == flag.isRaised)
                {
                    gameObject.SetActive(true);
                    return;
                }
            }
        }
        else
        {
            foreach (FlagVariable flag in flags)
            {
                if (!(pm.GetComponent<ProfileManager>().isFlagExist(flag.name) 
                      && pm.GetComponent<ProfileManager>().IsFlagRaised(flag.name) == flag.isRaised))
                {
                    Deactive();
                    return;
                }
            }
            gameObject.SetActive(true);
        }
        Deactive();
    }
}
