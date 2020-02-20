using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
        public Transform goalPosition;
    public float moveSpeed = 1f;
    Vector3 goalVector;

    void Start()
    {
        goalVector = goalPosition.position - this.transform.position;
    }

    void Update()
    {
        this.transform.position += goalVector/2 * Time.deltaTime * Mathf.Sin(Time.time * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = this.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
