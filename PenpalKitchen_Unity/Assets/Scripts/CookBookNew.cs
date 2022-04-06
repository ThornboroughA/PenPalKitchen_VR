using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CookBookNew : MonoBehaviour
{

    //input
    public XRNode[] inputSource;
    public bool currentlyActive = false;
    [SerializeField] private GameObject clipboardObject = null;
    private bool cooldown = false;

    //recipes
    public BaseIngredients currentIngredient;
    [SerializeField] private GameObject activePage;

    [SerializeField] private GameObject[] recipePages = null;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField] SphereCollider handCollider;


    [SerializeField] private bool startBool = true;
    [SerializeField] private bool endBool = false;    
    
    [SerializeField] private GameObject particles = null;



    #region Singleton
    public static CookBookNew instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CookBookNew found!");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("missing audiosource on CookBookNew in " + gameObject);
        }
        StartCoroutine(StartAfterDelay());
    }

    private void OnEnable()
    {
        currentIngredient = BaseIngredients.Null;
        activePage = recipePages[0];
    }

    private void Update()
    {
        ToggleCookbook();
        SwitchPage();
        RenderPages();
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        clipboardObject.SetActive(true);
        GameObject particle = Instantiate(particles, clipboardObject.transform.position, clipboardObject.transform.rotation);
        particle.transform.localScale *= 2;
    }

    /// <summary>
    ///  Cookbook pages manager
    /// </summary>

    private void SwitchPage()
    {

        if (startBool == true)
        {
            activePage = recipePages[6];
        } 
        else if (endBool == true) 
        {
            activePage = recipePages[7];
        }
        else
        {
            switch (currentIngredient)
            {
                case (BaseIngredients.Carrot):
                    activePage = recipePages[1];
                    break;
                case (BaseIngredients.Spinach):
                    activePage = recipePages[2];
                    break;
                case (BaseIngredients.Egg):
                    activePage = recipePages[3];
                    break;
                case (BaseIngredients.Cucumber):
                    activePage = recipePages[4];
                    break;
                case (BaseIngredients.Danmuji):
                    activePage = recipePages[5];
                    break;
                case (BaseIngredients.Null):
                    activePage = recipePages[0];
                    break;
                default:
                    activePage = recipePages[0];
                    break;
            }
        }
    }
    private void RenderPages()
    {
        foreach (GameObject page in recipePages)
        {
            if (page == activePage)
            {
                /*if (isActiveAndEnabled == true)
                {
                    break;
                } else
                {*/
                    page.SetActive(true);
                //}
            } else
            {
                /*if (isActiveAndEnabled == false)
                {
                    break;
                } else
                {*/
                    page.SetActive(false);
                //}
            }
        }

    }

    public void SetStartBool()
    {
        startBool = false;
    }
    public void SetEndBool()
    {
        startBool = false;
        endBool = true;
    }

    /// <summary>
    /// Toggle Cookbook Behavior
    /// </summary>
    private void ToggleCookbook()
    {
        bool inputCheck = CheckControllers();
        //if this is true, start coroutine until false
        //get length of coroutine
        //if over certian length, options
        //if under certain length, toggle


        if (inputCheck == true && cooldown == false)
        {
            //audioSource.PlayOneShot(audioSource.clip);

            audioSource.PlayOneShot(AudioLists.instance.GiveRandomClip("paper"));

            StartCoroutine(Cooldown(0.4f));
            if (currentlyActive == false)
            {
                handCollider.enabled = false;

                currentIngredient = BaseIngredients.Null;
                activePage = recipePages[0];
                
                currentlyActive = true;
                clipboardObject.SetActive(true);

                GameObject particle = Instantiate(particles, clipboardObject.transform.position, clipboardObject.transform.rotation);
                particle.transform.localScale *= 2;
            }
            else
            {
                currentlyActive = false;
                clipboardObject.SetActive(false);

                handCollider.enabled = true;
            }
        }
    }
    private bool CheckControllers()
    {
        foreach (XRNode inputSources in inputSource)
        {
            bool[] output = { false, false };


            
            InputDevice device = InputDevices.GetDeviceAtXRNode(inputSources);
            
            
            device.TryGetFeatureValue(CommonUsages.primaryButton, out output[0]);
            device.TryGetFeatureValue(CommonUsages.secondaryButton, out output[1]);

            if (output[0] == true || output[1] == true)
            {
                return true;
            }
        }
        return false;
    }
    private IEnumerator Cooldown(float cooldownTime)
    {
        cooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }


}
