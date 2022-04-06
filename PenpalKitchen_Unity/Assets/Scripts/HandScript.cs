using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandScript : MonoBehaviour
{

    XRDirectInteractor interactor;

    XRController thisController;


    private void Start()
    {
        Debug.Log("HandScript active on " + gameObject);

        interactor = GetComponent<XRDirectInteractor>();
        thisController = GetComponent<XRController>();
    }

    public void CallDropSelection()
    {
        StartCoroutine(DropSelection());
    }

    private IEnumerator DropSelection()
    {
        Debug.Log("HandScript drop referenced.");
        interactor.allowSelect = false;
        yield return new WaitForSeconds(0.5f);
        interactor.allowSelect = true;
    }


}
