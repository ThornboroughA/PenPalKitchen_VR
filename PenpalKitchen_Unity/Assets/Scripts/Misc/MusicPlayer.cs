using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips = null;
    private AudioSource audioSource;


    private void Start()
    {
        StartCoroutine(PlayAudioSequentially());

        audioSource = GetComponent<AudioSource>();
    }


    private IEnumerator PlayAudioSequentially()
    {
        yield return null;

        for (int i = 0; i < musicClips.Length; i++)
        {
            audioSource.clip = musicClips[i];

            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }

            //loops back to the first track
            if (i == musicClips.Length - 1)
            {
                i = 0;
            }
        }
    }

}
