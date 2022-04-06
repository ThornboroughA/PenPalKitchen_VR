using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimbapComponent : MonoBehaviour
{
    [SerializeField] private GameObject nextPhase;

    public void ReplaceSelf()
    {

        GameObject newGimbap = Instantiate(nextPhase, transform.position, transform.rotation);

        transform.position = new Vector3(500f, 500f, 500f);

        //Destroy(gameObject);

    }



}
