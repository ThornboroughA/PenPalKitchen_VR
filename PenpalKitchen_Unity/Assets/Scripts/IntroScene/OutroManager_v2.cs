using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroManager_v2 : MonoBehaviour
{

    [SerializeField] private GameObject[] scenes;
    [SerializeField] private GameObject[] spritesPeople;
    [SerializeField] private GameObject[] spritesBubbles;
    [SerializeField] private GameObject[] spritesBG;

    [SerializeField] private MeshRenderer[] sceneCover;

    [SerializeField] private int beforeFirstReveal = 3, timeToSceneTransition = 3;
    [SerializeField] private float timeToFade = 3f;

    [SerializeField] private GameObject backgroundFrames;


    private bool isFading = false;
    private int currentScene = 0;

    [SerializeField] private GameObject credits;

    #region Singleton
    public static OutroManager_v2 instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of OutroManager_v2 found!");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        foreach (GameObject people in spritesPeople)
        {
            people.SetActive(false);
        }
        foreach(GameObject bubbles in spritesBubbles)
        {
            bubbles.SetActive(false);
        }
        foreach(GameObject bgs in spritesBG)
        {
            bgs.SetActive(false);
        }
    }


    public void IterateScene()
    {
        if (!isFading)
        {

            if (currentScene < sceneCover.Length)
            {
                StartCoroutine(FadeCover());
            } else
            {
                Debug.Log("Outro end");
                ShowCredits();
            }

        }
    }

    //next frame start
    private IEnumerator FadeCover()
    {
        isFading = true;

        MeshRenderer thisCover = sceneCover[currentScene];
        Color changeColor = thisCover.material.color;

        float lerpDuration = timeToFade;
        float timeElapsed = 0;

        scenes[currentScene].SetActive(true);

        while (timeElapsed < lerpDuration)
        {
            changeColor.a = Mathf.Lerp(1f, 0f, timeElapsed / lerpDuration);
            thisCover.material.color = changeColor;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ActivateSprites();
    }
    private void ActivateSprites()
    {
        spritesPeople[currentScene].SetActive(true);
        spritesBubbles[currentScene].SetActive(true);
        spritesBG[currentScene].SetActive(true);
        
        isFading = false;
        currentScene++;
    }
    //next frame end

    private void ShowCredits()
    {

        StartCoroutine(Daechung());
        //FindObjectOfType<BlurCamera>().StartBlur();

    }

    private IEnumerator Daechung()
    {
        yield return new WaitForSeconds(2f);
        foreach (GameObject scene in scenes)
        {
            scene.SetActive(false);
        }
        backgroundFrames.SetActive(false);

        yield return new WaitForSeconds(1f);
        credits.SetActive(true);
    }

}
