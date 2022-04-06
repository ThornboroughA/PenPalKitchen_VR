using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoldableScript : MonoBehaviour
{

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        
    }




    /*private XRGrabInteractable xrGrabInteractable;

    private void Start()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void PickedUpEvent()
    {
        XRBaseInteractor beingHeldBy = xrGrabInteractable.selectingInteractor;
        Debug.Log($"{this} is being held by {beingHeldBy}");

        StartCoroutine(Drop(beingHeldBy));
    }


    private IEnumerator Drop(XRBaseInteractor beingHeld)
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Coroutine start");
        beingHeld.allowSelect = false;
    }*/



}
