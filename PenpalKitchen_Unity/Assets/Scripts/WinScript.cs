using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinScript : MonoBehaviour
{
    [SerializeField] private Animator[] dabinAnimation;

    private bool panelsActive = true;
    [SerializeField] private GameObject[] panels1;
    [SerializeField] private GameObject[] panels2;

    [SerializeField] private GameObject putHereSprite;

    private AudioSource audioSource;

    #region Singleton
    public static WinScript instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of WinScript found!");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateAnimation(int frame)
    {
        if (panelsActive == true)
        {
            foreach (GameObject panels in panels1)
            {
                panels.SetActive(false);
            }
            panelsActive = false;
        }

        foreach (Animator animator in dabinAnimation)
        {
            animator.SetInteger("currentSprite", frame);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Ingredient")
        {
            if (other.gameObject.GetComponent<Ingredient>().baseIngredient == BaseIngredients.FinishedGimbap)
            {

                StartCoroutine(WinGame(other.gameObject));

            }

        }

    }
    public void ShowPutHere()
    {
        putHereSprite.SetActive(true);
    }

    private IEnumerator WinGame(GameObject gimbap)
    {
        
        

        yield return new WaitForSeconds(2f);

        gimbap.SetActive(false);

        /*
        panels2[0].SetActive(true);
        panels2[1].SetActive(true);
        */


        panels1[0].SetActive(false);
        panels1[1].SetActive(false);

        gameObject.SetActive(false);

        
        GameManager.instance.OutroSceneTransition(2);
    }

}
