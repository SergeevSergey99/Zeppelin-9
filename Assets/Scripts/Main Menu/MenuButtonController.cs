using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    // Use this for initialization
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] public bool keySubmit = false;
    [SerializeField] public bool Enter = false;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetIndex(int Index)
    {
        index = Index;
    }
    public void SetIndex(bool _Enter)
    {
        Enter = _Enter;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetAxis("Submit") == 1)
        {
            if (keySubmit == false)
            {
                keySubmit = true;
            }
        }
        else
        {
            keySubmit = false;
        }*/

        if (index != 3)
            if (Input.GetAxis("Vertical") != 0)
            {
                if (!keyDown)
                {
                    if (Input.GetAxis("Vertical") < 0)
                    {
                        if (index < maxIndex)
                        {
                            index++;
                        }
                        else
                        {
                            index = 0;
                        }
                    }
                    else if (Input.GetAxis("Vertical") > 0)
                    {
                        if (index > 0)
                        {
                            index--;
                        }
                        else
                        {
                            index = maxIndex;
                        }
                    }

                    keyDown = true;
                }
            }
            else
            {
                keyDown = false;
            }
    }
}