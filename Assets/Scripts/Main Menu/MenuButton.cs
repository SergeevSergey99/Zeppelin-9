using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;


    private void OnMouseEnter()
    {
        menuButtonController.Enter = true;
        menuButtonController.index = thisIndex;
    }

    private void OnMouseExit()
    {
        menuButtonController.Enter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            {
                if (Input.GetAxis("SubmitMouse") == 1)
                {
                    if (menuButtonController.keySubmit == false && menuButtonController.Enter)
                    {
                        animator.SetBool("pressed", true);
                        menuButtonController.keySubmit = true;
                    }
                }
                else if (Input.GetAxis("Submit") == 1)
                {
                    if (menuButtonController.keySubmit == false)
                    {
                        GetComponent<Button>().onClick.Invoke();
                        animator.SetBool("pressed", true);
                        menuButtonController.keySubmit = true;
                    }
                }
                else
                {    menuButtonController.keySubmit = false;

                    if (animator.GetBool("pressed"))
                    {
                        animator.SetBool("pressed", false);
                        animatorFunctions.disableOnce = true;

                    }
                }
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}