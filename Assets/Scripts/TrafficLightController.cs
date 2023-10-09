using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    
    public Transform t3;
  
    public GameObject t3green;
    public GameObject t3red;

    public Transform t1;
    public GameObject t1green;
    public GameObject t1red; 

    public Transform t2;
    public GameObject t2green;
    public GameObject t2red;


    public Transform t4;
    public GameObject t4green;
    public GameObject t4red;

    public float stateTimer;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
  
        t3 = transform.Find("TL3");
        t2 = transform.Find("TL2");
        t1 = transform.Find("TL1");
        t4 = transform.Find("TL4");

        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;

        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;

        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;

        t4green = t4.Find("Green light").gameObject;
        t4red = t4.Find("Red light").gameObject;

        stateTimer = 10.0f;
        SetState(1);
    }

    // Update is called once per frame
    void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            // Change state and reset the timer
            if (state == 1)
            {
                SetState(2);
            }
            else
            {
                SetState(1);
            }

            stateTimer = 10.0f; // Reset the timer to 10 seconds
        }
    }
    void SetState(int c)
    {
        state = c;
        if (c == 1)
        {
   
            t3green.SetActive(false);
            t3red.SetActive(true);
            t2green.SetActive(false);
            t2red.SetActive(true);

            t1green.SetActive(true);
            t1red.SetActive(false);
            t4green.SetActive(true);
            t4red.SetActive(false);
        }
        else
        {
   
            t3green.SetActive(true);
            t3red.SetActive(false);
            t2green.SetActive(true);
            t2red.SetActive(false);

            t1green.SetActive(false);
            t1red.SetActive(true);
            t4green.SetActive(false);
            t4red.SetActive(true);
        }
    }

}
