using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EggBreak : MonoBehaviour
{
    
    private Rigidbody _rigidbody;

    private float speedBeforeUpdate;
    [SerializeField]
    private float maxSpeed = 8;
    [SerializeField]
    private GameObject splatParticles;

    private bool hasFired = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        speedBeforeUpdate = _rigidbody.velocity.magnitude;
    }
    private void OnCollisionEnter(Collision collision)
    {

        print(speedBeforeUpdate);
        if (speedBeforeUpdate >= 3 && !hasFired)
        {
            Splat(collision);
        }
    }

    private void Splat(Collision collision)
    {
        hasFired = true;

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Vector3 position = contact.point;
        GameObject splat = Instantiate(splatParticles, position, rotation);
        splat.transform.Translate(Vector3.up * 0.005f);

        Destroy(splat, 3.0f);
    }

}
