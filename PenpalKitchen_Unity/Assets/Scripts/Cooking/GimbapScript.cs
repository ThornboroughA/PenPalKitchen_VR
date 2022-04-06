using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimbapScript : MonoBehaviour
{

    [SerializeField] private GameObject XRHandle;
    [SerializeField] private GameObject nextPhase;
    [SerializeField] private Transform snapObject;

    private int numIngs = 5;
    private int diffChecker = 0;

    private bool hasFired = false;
    
    private void Update()
    {

        int childCount = snapObject.childCount;
        if (childCount != diffChecker)
        {
            diffChecker = childCount;
            print("NumIngs: " + numIngs + ", needed: " + childCount);

            WinScript.instance.UpdateAnimation(childCount);
        }

        
        if (childCount == numIngs)
        {
            if (hasFired == false)
            {
                XRHandle.SetActive(true);
                CookBookNew.instance.SetEndBool();
                hasFired = true;
            }
        }
    }




    public void StartNext()
    {
        StartCoroutine(InstantiateNext());
    }

    private IEnumerator InstantiateNext()
    { 
        yield return new WaitForSeconds(1f);

        Instantiate(nextPhase, transform.position, transform.rotation);

        transform.parent.parent.position = new Vector3(500f, 500f, 500f);

        //Destroy(transform.parent.parent.gameObject);
    }

}
