using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollider : MonoBehaviour
{
    private CharacterController characterController;

    private void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
        Physics.IgnoreCollision(GetComponent<Collider>(), characterController, true);
    }


}
