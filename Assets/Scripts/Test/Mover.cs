using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEditor;
using UnityEngine;

public class Mover : MonoBehaviour
{

    private float newX = 0;
    private float dir = 0;
    private bool isGo = false;

    public void setMover(float new_pose)
    {
        isGo = true;
        newX = (float) Math.Round(new_pose, 0);
        dir = new_pose - GetComponent<RectTransform>().localPosition.x;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isGo)
        {
            if (Math.Abs(Math.Abs(GetComponent<RectTransform>().localPosition.x) - Math.Abs(newX)) < 0.1f)
            {
                
                GetComponent<RectTransform>().localPosition = new Vector3((float) Math.Round(newX,0), 0, 0);
                isGo = false;
                GameObject.Find("ProfileManager").GetComponent<ProfileManager>().buttonCheck();
            }
            else
            {
                GetComponent<RectTransform>().localPosition += new Vector3(dir * Time.deltaTime, 0, 0);
            }
        }
    }
}
