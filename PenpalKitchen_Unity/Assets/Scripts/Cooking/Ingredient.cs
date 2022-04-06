using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public BaseIngredients baseIngredient;
    public GameObject nextState;

    public UtensilTypes neededUtensil;

    [Tooltip("Int num hits / ticks of timer before ingredient changes.")]
    public int lifespan;
    private bool transmuteCooldown = false;

    private Coroutine disposeRoutine;
    [HideInInspector] public bool outOfBounds = false;

    private AudioSource audioSource;
    [SerializeField] string audioClip = null;

    private IngredientLists ingredientLists;

    private void Start()
    {
        ingredientLists = GameObject.FindGameObjectWithTag("CookingManager").GetComponent<IngredientLists>();
        audioSource = GetComponent<AudioSource>();

    }

    public void TransmuteTo(UtensilTypes inputTool, Transform utensilTransform)
    {
        //change nextState and neededUtensil to arrays so it can go to either state, for e.g. gimbap/bibimbap rice
        if (inputTool == neededUtensil && transmuteCooldown == false)
        {
            TransmuteIngredient(utensilTransform);
        }
    }

    private void TransmuteIngredient(Transform utensilTransform)
    {
        GameObject nextPhase = null;
        StartCoroutine(Cooldown());

        switch(neededUtensil)
        {
            case UtensilTypes.Knife:
                audioClip = "chop";

                lifespan--;
                if (lifespan <= 0)
                {
                    nextPhase = Instantiate(nextState, transform.position, Quaternion.identity);
                } else
                {
                    return;
                }
                break;
            case UtensilTypes.Pan:
                if (baseIngredient != BaseIngredients.Egg)
                {
                    audioClip = "fryPan";
                } else
                {
                    audioClip = "eggBreak";
                }

                nextPhase = Instantiate(nextState, utensilTransform.position, utensilTransform.rotation, utensilTransform);                
                break;
            case UtensilTypes.Hotplate:
                audioClip = "fryPan";

                nextPhase = Instantiate(nextState, utensilTransform.position, utensilTransform.rotation, utensilTransform);
                break;
            case UtensilTypes.Spatula:
                audioClip = "dragCancel";

                nextPhase = Instantiate(nextState, transform.position, transform.rotation);

                nextPhase.GetComponent<Ingredient>().TempDeactStart(nextPhase);
                var rand = Random.onUnitSphere;
                rand.y = Mathf.Abs(rand.y);
                nextPhase.GetComponent<Rigidbody>().velocity = rand * 1.5f;
                break;
            case UtensilTypes.StartBal:
                audioClip = "chop";

                nextPhase = Instantiate(nextState, utensilTransform.position, utensilTransform.rotation, utensilTransform);
                nextPhase.transform.parent = utensilTransform.parent;
                utensilTransform.position += new Vector3(0.03f, 0.005f, 0.04f); //moves the transform so rice is in right place
                utensilTransform.parent.gameObject.GetComponent<Utensil>().thisUtensil = UtensilTypes.Bal;
                break;
            case UtensilTypes.Bal:
                audioClip = "chop";

                CookBookNew.instance.SetStartBool();
                nextPhase = Instantiate(nextState, utensilTransform.position, utensilTransform.rotation, utensilTransform);
                break;
            case UtensilTypes.GimbapRice:
                audioClip = "gimbapPut";

                nextPhase = Instantiate(nextState, utensilTransform.position, utensilTransform.rotation, utensilTransform);
                break;
            case UtensilTypes.Null:
                Debug.LogError("UtensilType null?");
                break;
        }
        PlayAudio();
        DestroySelf();
        ingredientLists.AddIngredient(nextPhase);
    }

    private void PlayAudio()
    {
        if (audioSource)
        {
            if (audioClip != null)
            {
                audioSource.clip = AudioLists.instance.GiveRandomClip(audioClip);
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }

    private IEnumerator Cooldown()
    {
        transmuteCooldown = true;
        yield return new WaitForSeconds(0.5f);
        transmuteCooldown = false;
    }

    public void TempDeactStart(GameObject toPass)
    {
        StartCoroutine(TempDeactivate(toPass));
    }
    private IEnumerator TempDeactivate(GameObject input)
    {
        input.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForFixedUpdate();
        input.GetComponent<BoxCollider>().enabled = true;
    }

    /// <summary>
    /// Dispose of ingredient if out of bounds
    /// If picked up while in disposal process, cancel
    /// </summary>
    public void Dropped()
    {
        if (disposeRoutine == null)
        {
            disposeRoutine = StartCoroutine(DisposeOfIng());
        }
    }
    public void PickedUp()
    {
        UpdateGlobalIngredient();
        PlaySoundFromList("selectionSounds");

        outOfBounds = false;
        if (disposeRoutine != null)
        {
            StopCoroutine(disposeRoutine);
            disposeRoutine = null;
        }
    }
    private IEnumerator DisposeOfIng()
    {

        GameObject particles = null;

        yield return new WaitForSeconds(5f);

        if (outOfBounds == true)
        {
            particles = Instantiate(CookingManager.instance.destroyPrefab, transform.position, CookingManager.instance.destroyPrefab.transform.rotation);

            DestroySound();
            DestroySelf();
        }
        yield return new WaitForSeconds(2f);

        Destroy(particles);
    }
    private void DestroySound()
    {
        AudioClip toPlay = AudioLists.instance.GiveRandomClip("miscSounds");
        audioSource.PlayOneShot(toPlay);
    }


    public void DestroySelf()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().HandsDrop(gameObject.transform);
        ingredientLists.RemoveIngredient(gameObject);
        //run destroy animation
        //StartCoroutine(DestroyDelay());  
        MoveToTrash();
    }
    /*private IEnumerator DestroyDelay()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);   
        Destroy(gameObject);
    }*/
    private void MoveToTrash()
    {

        CleanUpManager.instance.AddToTrash(gameObject);

        /*
        transform.parent = CleanUpManager.instance.gameObject.transform;
        transform.position = CleanUpManager.instance.gameObject.transform.position;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().useGravity = false;
        }*/
    }

    private void UpdateGlobalIngredient()
    {
        /*CookingManager.currentIngredient = baseIngredient;
        print(CookingManager.currentIngredient);*/

        CookBookNew.instance.currentIngredient = baseIngredient;
    }

    public void PlaySoundFromList(string soundType)
    {
        float originalPitch = audioSource.pitch;

        audioSource.pitch = 2.5f;
        audioSource.PlayOneShot(AudioLists.instance.GiveRandomClip(soundType));
        audioSource.pitch = originalPitch;
    }


}
