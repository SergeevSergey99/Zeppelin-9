using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBlinker : StateMachineBehaviour
{

    readonly float blinkMinTime = 5;
    readonly float blinkMaxTime = 8;

    float blinkTimer = 0;

    string[] blinkTriggers= {"Blink", "LongBlink", "DoubleBlink"};

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (blinkTimer <= 0) {
           RandomBlink(animator);
           blinkTimer = Random.Range(blinkMinTime, blinkMaxTime);
       } else {
           blinkTimer -= Time.deltaTime;
       }
    }

    void RandomBlink(Animator animator) {
        System.Random rnd = new System.Random();
        int eyePosition = rnd.Next(blinkTriggers.Length);
        string blinkTrigger = blinkTriggers[eyePosition];
        animator.SetTrigger(blinkTrigger);
    }


}