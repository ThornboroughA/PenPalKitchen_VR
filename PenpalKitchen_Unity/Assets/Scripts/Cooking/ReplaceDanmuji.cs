using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceDanmuji : MonoBehaviour
{
    [SerializeField] private GameObject toReplaceWith = null;

    public void ReplaceSelf()
    {
        
        Instantiate(toReplaceWith, transform.position, transform.rotation);
        CleanUpManager.instance.AddToTrash(gameObject);

    }

}
