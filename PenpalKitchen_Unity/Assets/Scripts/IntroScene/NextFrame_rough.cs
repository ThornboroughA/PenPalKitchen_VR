
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NextFrame_rough : MonoBehaviour
{
    public XRNode[] inputSource;
    public bool outputBool;

    private Coroutine SceneSkip;
    private bool skipRunning = false;

    //tutorial sprites (hold button etc)
    private bool vanished = false;
    [SerializeField] private GameObject[] tooltips;

    //audio
    [SerializeField] private AudioClip[] nextSceneSounds = null;
    private AudioSource audioSource;
    private bool audioCooldown = false;

    [SerializeField] private bool outroScene = false;
    private bool cooldown = false;

    #region Singleton
    public static NextFrame_rough instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of NextFrame_rough found!");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        outputBool = GetButtonPresses();

        if (outroScene == false)
        {
            if (outputBool == true && skipRunning == false)
            {
                StartSceneSkip();

                if (vanished == false)
                {
                    StartCoroutine(HideTooltips());
                }
            }
            if (skipRunning == true && outputBool == false)
            {
                StopSceneSkip();
            }
        } else if (outroScene == true)
        {
            if (outputBool == true && cooldown == false)
            {
                print("input get");

                cooldown = true;
                OutroManager_v2.instance.IterateScene();
                StartCoroutine(Cooldown());

                if (vanished == false)
                {
                    StartCoroutine(HideTooltips());
                }
            }
        }
    }

    private IEnumerator HideTooltips()
    {
        vanished = true;
        yield return new WaitForSeconds(1f);

        foreach (GameObject tooltip in tooltips)
        {
            Animator animator = tooltip.GetComponent<Animator>();
            animator.SetBool("EndAnim", true);
        }
        yield return new WaitForSeconds(1f);
        foreach(GameObject tooltip in tooltips)
        {
            Destroy(tooltip);
        }
    }


    private bool GetButtonPresses()
    {
        int pressInt = 0;

        foreach (XRNode input in inputSource)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);

            device.TryGetFeatureValue(CommonUsages.primaryButton, out outputBool);
            if (outputBool == true)
            {
                pressInt++;
            }
            device.TryGetFeatureValue(CommonUsages.secondaryButton, out outputBool);
            if (outputBool == true)
            {
                pressInt++;
            }
        }

        if (pressInt > 0)
        {
            return outputBool = true;
        } else
        {
            return outputBool = false;
        }
    }


    private void StartSceneSkip()
    {
        skipRunning = true;
        SceneSkip = StartCoroutine(SkipScene());
    }
    private void StopSceneSkip()
    {
        StopCoroutine(SceneSkip);
        skipRunning = false;
    }

    private IEnumerator SkipScene()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.NewSceneTransition(1);
    }
    public void PlayAudio()
    {
        if (audioCooldown == false)
        {
            StartCoroutine(Cooldown());
            audioSource.PlayOneShot(nextSceneSounds[Random.Range(0, nextSceneSounds.Length)]);
        }
    }
    private IEnumerator Cooldown()
    {
        audioCooldown = true;
        yield return new WaitForSeconds(0.8f);
        audioCooldown = false;
        cooldown = false;
    }


}
