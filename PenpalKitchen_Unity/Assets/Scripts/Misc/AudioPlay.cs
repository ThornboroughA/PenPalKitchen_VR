using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{

    private AudioSource _audioSource;

    private bool coolDown = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (coolDown == false)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            StartCoroutine(CoolDown());
        }
    }


    private IEnumerator CoolDown()
    {
        coolDown = true;

        yield return new WaitForSeconds(1f);

        coolDown = false;
    }


}
