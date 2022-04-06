using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ingredient")
        {
            other.gameObject.GetComponent<Ingredient>().outOfBounds = true;
            other.gameObject.GetComponent<Ingredient>().Dropped();
        }
    }

}
