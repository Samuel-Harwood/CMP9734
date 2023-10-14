using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCarController : MonoBehaviour
{
    private List<Transform> path;
    public Transform pathGroup;
    public int currentPathObj;
    public Vector3 velocity;
    public Rigidbody rb;
    public float speed = 10.0f;
    public float distFromPath = 2f; //20
    public float acceleration = 0.3f;


    public GameObject brake1;
    public GameObject brake2;


    void Start()
    {
   /*     brakelight1 = brake1.Find("Spot Light").gameObject;
        brakelight2 = brake2.Find("Spot Light (1)").gameObject;*/
        brake1.SetActive(false);
        brake2.SetActive(false);

        path = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        getPath();

    }

    void getPath()
    {
        Transform[] childObjects = pathGroup.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childObjects.Length; i++)
        {
            Transform temp = childObjects[i];
            if (temp.gameObject.GetInstanceID() != GetInstanceID())
                path.Add(temp);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 displacement = path[currentPathObj].position - transform.position;
        displacement.y = 0;
        float distFromPath = displacement.magnitude;
        if (distFromPath < 1.2f)
        {
            currentPathObj++;
            if (currentPathObj >= path.Count)
            {
                currentPathObj = 0;
                return;
            }
        }

        //calculate velocity for this frame
        velocity = displacement;
        velocity.Normalize();
        velocity *= speed;
        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);

        //align to velocity

        // Move towards the current waypoint
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
        speed * Time.deltaTime, 0.0f); //10.0f


        Quaternion rotation = Quaternion.LookRotation(desiredForward);

        rb.MoveRotation(rotation);
        
    }


   
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Red")
        {
            brake1.SetActive(true);
            brake2.SetActive(true);
            
            if (speed > 0.0f)
            {
                speed -= acceleration;
            }
        }
       
        if (other.tag == "Green")
        {
            brake1.SetActive(false);
            brake2.SetActive(false);
            if (speed < 10.0f)
            {
                speed += acceleration;
            }
        }
        if (other.tag == "Car")
        {
            brake1.SetActive(true);
            brake2.SetActive(true);
            Debug.Log("See Car");
            if (speed > 0.0f)
            {
                speed -= acceleration;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        brake1.SetActive(false);
        brake2.SetActive(false);
        speed = 8.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pedestrian")
        {
            speed = 0;
        }

    }

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exited Trigger");
    //}


}
