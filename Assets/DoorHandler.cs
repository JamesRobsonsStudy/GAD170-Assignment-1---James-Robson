using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{

    Rigidbody doorRB;
    Renderer render;
    public float doorLevel = 0f;
    public float doorChanceCoefficient = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        doorRB = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        doorRB.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void CheckUnlock(float level)
    {
        //render.material.color = Color.red;
        Unlock();
    }

    void Unlock()
    {
        render.material.color = Color.green;
        doorRB.constraints = RigidbodyConstraints.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
