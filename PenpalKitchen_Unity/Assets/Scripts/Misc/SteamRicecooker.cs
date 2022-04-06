using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamRicecooker : MonoBehaviour
{

    [SerializeField] private ParticleSystem steamParticles = null;
    private bool isPlaying = false;


    private void Start()
    {
        steamParticles.Stop();
    }

    private void Update()
    {
        if (transform.localEulerAngles.z >= 20f)
        {
            ShowSteam(true);
        } else
        {
            ShowSteam(false);
        }

    }

    private void ShowSteam(bool reveal)
    {
        if (reveal == true && isPlaying == false)
        {
            isPlaying = true;
            steamParticles.Play();

        } else if (reveal == false && isPlaying == true)
        {
            isPlaying = false;
            steamParticles.Stop();
        }
    }


}
