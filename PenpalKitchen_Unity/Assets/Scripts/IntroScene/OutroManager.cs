using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OutroManager : MonoBehaviour
{


    [Tooltip("The white frame that fades out")] [SerializeField] 
    MeshRenderer[] sceneCover;
    [Tooltip("The sprites and frame. These also include the scene interaction that iterates on this script")] [SerializeField] 
    private GameObject[] sceneSprites;

    [SerializeField] private int beforeFirstReveal = 3;
    [SerializeField] private int timeToSceneTransition = 3;
    [SerializeField] private float timeToFade = 3f;

    private bool isFading = false;
    private int currentScene = 1;

    [SerializeField] private GameObject credits;




    public void IterateScene()
    {
        if (!isFading)
        {
            if (currentScene < sceneCover.Length)
            {
                StartCoroutine(NextSprites());
                StartCoroutine(FadeCover());
                currentScene += 1;
            } else
            {
                Debug.Log("Outro end");
                ShowCredits();
            }
        }
    }

    private IEnumerator FadeCover()
    {
        isFading = true;

        MeshRenderer thisCover = sceneCover[currentScene];
        Color changeColor = thisCover.material.color;

        float lerpDuration = timeToFade;
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            changeColor.a = Mathf.Lerp(1f, 0f, timeElapsed / lerpDuration);
            thisCover.material.color = changeColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        isFading = false;

    }
    private IEnumerator NextSprites()
    {
        sceneSprites[currentScene].SetActive(true);
        yield return null;
    }


    private void ShowCredits()
    {

        StartCoroutine(Daechung());
        //FindObjectOfType<BlurCamera>().StartBlur();
        
    }

    private IEnumerator Daechung()
    {
        sceneSprites[0].SetActive(false);
        yield return new WaitForSeconds(1f);
        credits.SetActive(true);
    }



}
