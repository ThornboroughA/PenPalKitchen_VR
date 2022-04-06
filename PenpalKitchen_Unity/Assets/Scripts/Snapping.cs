using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Snapping : MonoBehaviour
{
    /*[SerializeField] private Color col1;
    [SerializeField] private Color col2;*/

    [SerializeField] private Transform snapPoint;
    [SerializeField] private GameObject[] acceptedObjects; //things the hotplate will lock on

    private GameObject player;
    public GameObject activeObject;
    private SnapBack snapBack;

    [SerializeField] private float maxDist = 0.13f;

    public bool transmutationSurface = false;

    public static bool activePan = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (snapPoint == null)
        {
            snapPoint = transform;
        }
    }
    private void Update()
    {
        if (activeObject != null)
        {
            float dist = Vector3.Distance(activeObject.transform.position, snapPoint.position);

            if (dist > maxDist)
            {
                StartCoroutine(ExitDelay());
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (activeObject == null && activePan == false)
        {
            foreach (GameObject acceptedObject in acceptedObjects)
            {
                if (other.gameObject == acceptedObject)
                {
                    player.GetComponent<PlayerManager>().HandsDrop(other.gameObject.transform);

                    StartCoroutine(Snap(other));
                }
            }
        }
    }
    private IEnumerator Snap(Collider other)
    {
        yield return new WaitForFixedUpdate();

        activeObject = other.gameObject;
        if (activeObject != null)
        {
            /*if (activeObject.GetComponent<XRGrabInteractable>())
            {
                Destroy(activeObject.GetComponent<XRGrabInteractable>());
            }*/
            activePan = true;
        } else
        {
            activePan = false;
        }
        
        

        //tell object's SnapBack to not operate
        if (activeObject.GetComponent<SnapBack>())
        {
            snapBack = activeObject.GetComponent<SnapBack>();
            snapBack.inSnapPoint = true;
        }
        
        other.transform.position = snapPoint.position;

        //
        if (other.gameObject.GetComponent<IngredientTransmute>())
        {
            other.transform.parent = transform;
            other.gameObject.GetComponent<IngredientTransmute>().ReplaceSelf();
        }
    }

    private IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(1f);
        float dist2 = Vector3.Distance(activeObject.transform.position, snapPoint.position);
        if (dist2 > maxDist)
        {
            if (activeObject.GetComponent<SnapBack>())
            {
                snapBack.inSnapPoint = false;
                snapBack = null;
            }
            activeObject = null;
            activePan = false;
        }
    }
}
