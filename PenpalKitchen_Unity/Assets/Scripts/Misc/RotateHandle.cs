using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHandle : MonoBehaviour
{
    [SerializeField] private GameObject objectToReveal;

    [SerializeField] private float angle = 80;


    private void Start()
    {
        angle = transform.rotation.eulerAngles.x;
    }

    private void Update()
    {
        if (transform.rotation.eulerAngles.x > angle + 30 || transform.rotation.eulerAngles.x < 300)
        {
            objectToReveal.SetActive(true);
        } else
        {
            objectToReveal.SetActive(false);
        }
    }


    

}
