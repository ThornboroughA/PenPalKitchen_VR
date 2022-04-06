using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientTransmute : MonoBehaviour
{
    private AudioSource audioSource;


    [SerializeField] private GameObject toBecome;

    private void Start()
    {
        if (GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void ReplaceSelf()
    {
        GameObject newIng = Instantiate(toBecome, transform.parent);

        if (audioSource)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (transform.parent.gameObject.GetComponent<Snapping>().transmutationSurface == true)
        {
            transform.parent.gameObject.GetComponent<Snapping>().activeObject = gameObject;
        }

        transform.position = new Vector3(100f, 100f, 100f);
        StartCoroutine(DestroySelf());

    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(5f);
        Destroy(transform.gameObject);
    }

}
