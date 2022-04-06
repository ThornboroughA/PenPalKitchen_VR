using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    [SerializeField] private GameObject colliderToIgnore;

    private void Start()
    {
        /* if (colliderToIgnore.GetComponent<MeshCollider>())
         {
             Physics.IgnoreCollision(colliderToIgnore.GetComponent<Collider>)
         }
         else if (colliderToIgnore.GetComponent<BoxCollider>())
         {

         } 
         else
         {
             Debug.LogError("colliderToIgnore doesn't have a box or mesh collider.");
         }*/

        Physics.IgnoreCollision(GetComponent<Collider>(), colliderToIgnore.GetComponent<Collider>(), true);
    }

}
