using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianRoute : MonoBehaviour
{

    public float dist;
    public Rigidbody rb;
    private List<Transform> path;
    public Transform pathGroup;
    public int currentPathObj;
    public float speed = 2.5f;
    public Vector3 velocity;
    Animator m_Animator;
    Vector3 m_Movement;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool("isIdle", false);
        path = new List<Transform>();
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
        float dist = displacement.magnitude;
        if (dist < 0.1f)
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
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
        10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Green")
        {
            m_Animator.SetBool("isIdle", true);
            speed = 0.0f;
        }
        if (other.tag == "Red")
        {
            m_Animator.SetBool("isIdle", false);
            speed = 2.5f;
        }


    }
    void OnTriggerExit(Collider other)
    {
        m_Animator.SetBool("isIdle", false);
        speed = 2.5f;
    }
   



}