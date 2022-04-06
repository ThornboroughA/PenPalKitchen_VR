using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandsDrop : MonoBehaviour
{
    XRDirectInteractor interactor;
    public bool hasUtensil = false;

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<XRDirectInteractor>();
    }


    public float ObjectDistance(Transform dropObject)
    {
        float dist = Vector3.Distance(dropObject.position, transform.position);
        return dist;
    }

    public void DropSelection(Transform dropObject)
    {
        if (hasUtensil)
        {
            return;
        }

        StartCoroutine(DropSelectionRoutine());
    }
    //if holding something, drop it when called
    private IEnumerator DropSelectionRoutine()
    {

        interactor.allowSelect = false;
        yield return new WaitForSeconds(0.5f);
        interactor.allowSelect = true;
    }
}
