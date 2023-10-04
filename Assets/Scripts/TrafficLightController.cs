using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public Transform t1;
    public Transform t2;
    public Transform t3;
    public GameObject t1green;
    public GameObject t1red;
    public GameObject t2green;
    public GameObject t2red;
    public GameObject t3green;
    public GameObject t3red;
    public float stateTimer;
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        t1 = transform.Find("TL1");
        t2 = transform.Find("TL2");
        t3 = transform.Find("TL3");
        t1green = t1.Find("Green light").gameObject;
        t1red = t1.Find("Red light").gameObject;
        t2green = t2.Find("Green light").gameObject;
        t2red = t2.Find("Red light").gameObject;
        t3green = t3.Find("Green light").gameObject;
        t3red = t3.Find("Red light").gameObject;

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
            t1green.active = true;
            t1red.active = false;
            t2green.active = false;
            t2red.active = true;
            t3green.SetActive(false);
            t3red.SetActive(true);
        }
        else
        {
            t1green.active = false;
            t1red.active = true;
            t2green.active = true;
            t2red.active = false;
            t3green.SetActive(true);
            t3red.SetActive(false);
        }
    }

}
