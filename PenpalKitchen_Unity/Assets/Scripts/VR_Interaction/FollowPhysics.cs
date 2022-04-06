using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPhysics : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;

    [SerializeField] private bool rotateNotFollow = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rotateNotFollow == false)
        {
            rb.MovePosition(target.transform.position);
        } else
        {
            rb.MoveRotation(target.transform.rotation);
        }

    }
}
