using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HotplateScript : MonoBehaviour
{
    
    [SerializeField] private Transform hotplateSnap;
    [SerializeField] private GameObject panSnap;
    [SerializeField] private Transform dial;

    [SerializeField] private bool isOn = false;

    [SerializeField] private bool activePan = false;
    [SerializeField] private float range = 2f;
    [SerializeField] private GameObject activeIngredient;

    [SerializeField] private GameObject flame;

    [Tooltip("What ingredients snap to. Must have 0 children.")] public Transform ingSnapTo;


    private AudioSource audioSource;
    [SerializeField] AudioClip[] stoveSounds = null;

    private Coroutine cookingRoutine;
    private Coroutine cookingAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (dial.transform.rotation.eulerAngles.z < 230)
        {
            if (isOn == false)
            {
                cookingAudio = StartCoroutine(CookingSounds());
            }
            isOn = true;
        } else
        {
            isOn = false;
        }

        if (isOn)
        {
            CheckRange();

            flame.SetActive(true);
        } else
        {
            flame.SetActive(false);
        }
    }

    private IEnumerator CookingSounds()
    {
        audioSource.clip = stoveSounds[0];
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0.4f;
        audioSource.clip = stoveSounds[1];
        audioSource.loop = true;
        bool audioPlaying = false;

        while (isOn == true)
        {
            if (audioPlaying == false)
            {
                audioSource.Play();
                audioPlaying = true;
            }
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 1f;
        audioSource.loop = false;

    }

    private IEnumerator CookOverTime(GameObject ingredient)
    {
        Ingredient _ingredient = ingredient.GetComponent<Ingredient>();

        while (_ingredient.lifespan > 0)
        {
            _ingredient.lifespan--;
            yield return new WaitForSeconds(1f);
        }

        _ingredient.TransmuteTo(UtensilTypes.Hotplate, ingSnapTo);
        activeIngredient = null;
    }

    /// <summary>
    /// INGREDIENTS MANAGEMENT
    /// If pan in range, adds ingredients (that need hotplate) and starts them cooking
    /// if leaves range, clears ingredients
    /// </summary>
    private void CheckRange()
    {
        float dist = Vector3.Distance(hotplateSnap.position, panSnap.transform.position);

        if (dist < range)
        {
            activePan = true;
            if (activeIngredient == null)
            {
                AddIngredients();
            }
        }
        else
        {
            activePan = false;
            if (activeIngredient != null)
            {
                activeIngredient = null;
                StopCoroutine(cookingRoutine);
            }
        }
    }

    private void AddIngredients()
    {
        
        //adjust this back into a for loop for multiple ingredients
        if (panSnap.transform.childCount > 0)
        {
            GameObject panSnapChild = panSnap.transform.GetChild(0).gameObject;

            if (panSnapChild.GetComponent<Ingredient>().neededUtensil == UtensilTypes.Hotplate)
            {
                activeIngredient = panSnapChild;
                cookingRoutine = StartCoroutine(CookOverTime(panSnapChild));
            }
        }

    }



}
