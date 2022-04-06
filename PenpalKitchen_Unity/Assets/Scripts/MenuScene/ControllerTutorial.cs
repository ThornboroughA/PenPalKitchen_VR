using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class ControllerTutorial : MonoBehaviour
{
    public XRNode[] inputSource;
    [SerializeField] private Animator buttonSprites = null;
    private bool buttonCheck, triggerCheck = false;

    private AudioSource audioSource;

    [SerializeField] private GameObject[] hands = null;

    [SerializeField] private GameObject loadingSprite = null;

    #region Singleton
    public static ControllerTutorial instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ControllerTutorial found!");
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
        if (buttonCheck == false)
        {
            if (GetButtonPresses() == true)
            {
                buttonSprites.SetInteger("spriteState", 1);
                audioSource.PlayOneShot(audioSource.clip);
                buttonCheck = true;
            }
        }
        if (buttonCheck == true && triggerCheck == false)
        {
            if (GetTriggerPresses() == true)
            {
                buttonSprites.SetInteger("spriteState", 2);
                audioSource.PlayOneShot(audioSource.clip);
                triggerCheck = true;

                StartCoroutine(ChangeSceneAsync());
                //StartCoroutine(ChangeScene());
            }
        }
    }


    private IEnumerator ChangeSceneAsync()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (GameObject hand in hands)
        {
            hand.SetActive(false);
        }

        loadingSprite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("QuillScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
    private IEnumerator ChangeScene()
    {
        foreach(GameObject hand in hands)
        {
            hand.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("QuillScene");
    }

    private bool GetButtonPresses()
    {
        bool outputBool = false;
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
        }
        else
        {
            return outputBool = false;
        }
    }
    private bool GetTriggerPresses()
    {
        bool outputBool = false;
        int pressInt = 0;

        foreach (XRNode input in inputSource)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);

            device.TryGetFeatureValue(CommonUsages.gripButton, out outputBool);
            if (outputBool == true)
            {
                pressInt++;
            }
        }
        if (pressInt > 0)
        {
            return outputBool = true;
        }
        else
        {
            return outputBool = false;
        }
    }



}
