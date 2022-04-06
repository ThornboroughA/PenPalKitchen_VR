using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManagerRough : MonoBehaviour
{
    //fix index overflowing array once all are clicked


    private int currentScene = 0;

    //[SerializeField] GameObject[] scenesIndividual;
    [Tooltip("The white frame that fades out")][SerializeField] MeshRenderer[] sceneCover;
    //[SerializeField] SpriteRenderer[] sceneFrame;
    [Tooltip("The sprites and frame. These also include the scene interaction that iterates on this script")]
    [SerializeField] private GameObject[] sceneSprites;

    [SerializeField] private int beforeFirstReveal = 5;
    [SerializeField] private int timeToSceneTransition = 5;
    [SerializeField] private float timeToFade = 3f;

    private AudioSource audioSource;

    private GameManager gameManager;

    private bool isFading = false;

    #region Singleton
    public static IntroManagerRough instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of IntroManagerRough found!");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        Invoke("IterateScene", beforeFirstReveal);

    }
   /* private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            IterateScene();
        }
    }*/

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void IterateScene()
    {
        if (!isFading)
        {
            
            //StartCoroutine(FadeFrame());

            if (currentScene < sceneCover.Length)
            {
                AudioListsIntro.instance.StopAudio();

                StartCoroutine(NextSprites());
                StartCoroutine(FadeCover());
                currentScene += 1;

                
            }
            else
            {
                Debug.Log("intro end");
               // gameManager.NewSceneTransition(timeToSceneTransition);
            }

        }
        
    }

    private IEnumerator FadeCover()
    {
        print(currentScene);
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

            if (timeElapsed < ( lerpDuration / 2))
            {
                AudioListsIntro.instance.PlayAudioClip(currentScene);
            }

            yield return null;
        }
        isFading = false;
        

    }
    private IEnumerator NextSprites()
    {
        sceneSprites[currentScene].SetActive(true);
        yield return null;
    }

   /* private IEnumerator FadeFrame()
    {
        SpriteRenderer thisFrame = sceneFrame[currentScene];
        Color changeColor = thisFrame.material.color;

        float lerpDuration = timeToFade;
        float timeElapsed = 0;

        thisFrame.gameObject.SetActive(true);

        while (timeElapsed < lerpDuration)
        {
           // Debug.Log(thisFrame.material.color + " " + changeColor);

            changeColor.a = Mathf.Lerp(0f, 1f, timeElapsed / lerpDuration);
            thisFrame.material.color = changeColor;
            timeElapsed += Time.deltaTime;

            yield return null;
        }

    }*/



}
