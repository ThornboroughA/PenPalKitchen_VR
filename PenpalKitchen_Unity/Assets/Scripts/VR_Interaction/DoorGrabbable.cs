using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrabbable : MonoBehaviour
{
    [SerializeField]
    private Transform handler;
    private Transform original;



    [SerializeField] private Rigidbody[] rigidBody;

    public void Start()
    {
        original = transform;
    }


    public void GrabStart()
    {
        foreach (Rigidbody rb in rigidBody)
        {
            rb.isKinematic = false;
        }
    }
    public void GrabEnd()
    {
        foreach (Rigidbody rb in rigidBody)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;
        transform.localScale = original.localScale;

        

    }
}
