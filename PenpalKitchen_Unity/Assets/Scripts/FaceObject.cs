using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FaceObject : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFace;


    private void Update()
    {
        Vector3 targetPosition = new Vector3(objectToFace.position.x, this.transform.position.y, objectToFace.position.z);

        transform.LookAt(targetPosition);
    }

}
