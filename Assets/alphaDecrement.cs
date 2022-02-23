using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class A: Button{}
public class alphaDecrement : MonoBehaviour
{
    public float decrementingTime = 5;
    private float _decrementingTime = 5;
    private bool Decrementing = false;
    // Start is called before the first frame update
    
    [SerializeField]
    private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();
    void Start()
    {
        _decrementingTime = decrementingTime;
    }


    public void StartDecrementing()
    {
        Decrementing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Decrementing)
        {
            if (decrementingTime > 0)
            {
                decrementingTime -= Time.deltaTime;
                Color c = GetComponent<Image>().color;
                c.a = decrementingTime / _decrementingTime;
                GetComponent<Image>().color = c;
                
            }
            else
            {
                m_OnClick.Invoke();
            }
        }
    }
}
