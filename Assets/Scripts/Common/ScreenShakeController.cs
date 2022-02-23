using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{

    private float shakeTimeRemaining, shakePower, shakeFadeTime;
    public float length , power;


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.K)) {
            StartShake();
        }
        
    }

    private int cnt = 3;
    private void LateUpdate() {
        if(shakeTimeRemaining > 0) {
            shakeTimeRemaining -= Time.deltaTime;

            if (shakeTimeRemaining <= 0)
            {
                transform.localPosition = Vector3.zero;
                //gameObject.GetComponent<ScreenShakeController>().enabled = false;
                return;
            }
            
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            if (cnt <= 0)
            {
                transform.localPosition = Vector3.zero;
                cnt = 3;
            }

            cnt--;
            transform.localPosition +=  new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }

    }

    public void StartShake (){
            shakeTimeRemaining = length;
            shakePower = power;

            shakeFadeTime = power / length;
    }

        public void StartShake (float l, float p){
            shakeTimeRemaining = l;
            shakePower = p;

            shakeFadeTime = p / l;
    }
}