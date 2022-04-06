using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCollider : MonoBehaviour
{
    private bool hasPlayed = false;

    private IntroManagerRough introManager;

    private void Start()
    {
        introManager = GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManagerRough>();
    }


    public void ActivateCollider()
    {
        if (hasPlayed == false)
        {
            introManager.PlaySound();
            introManager.IterateScene();
            GetComponent<BoxCollider>().enabled = false;
            hasPlayed = true;
        }
    }




}
