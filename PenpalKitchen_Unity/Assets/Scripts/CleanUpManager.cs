using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpManager : MonoBehaviour
{

    #region Singleton
    public static CleanUpManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CleanUpManager found!");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject nextToTrash = null;


    public void AddToTrash(GameObject objectToTrash)
    {
        if (nextToTrash != null)
        {
            Destroy(nextToTrash);
        }

        objectToTrash.transform.parent = transform;
        objectToTrash.transform.position = transform.position;

        if (objectToTrash.GetComponent<Rigidbody>())
        {
            objectToTrash.GetComponent<Rigidbody>().useGravity = false;
        }

        nextToTrash = objectToTrash;
    }


}
