using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListsIntro : MonoBehaviour
{

    [SerializeField] private AudioClip[] sceneSounds;
    [SerializeField] private AudioClip[] nextFrameSounds;

    private AudioSource audioSource; 

    #region Singleton
    public static AudioListsIntro instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of AudioListsIntro found!");
            return;
        }
        instance = this;
    }
    #endregion
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(int sceneNumber)
    {
        audioSource.Stop();
        audioSource.clip = sceneSounds[sceneNumber];
        audioSource.Play();
    }

    public void StopAudio()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float fadeTime = 1.5f;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime * fadeTime;

            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
