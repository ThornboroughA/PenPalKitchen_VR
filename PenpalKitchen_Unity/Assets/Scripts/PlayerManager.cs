using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private HandsDrop[] hands;

    private float[] objDist = { 0, 0 };

  
    public void HandsDrop(Transform dropObject)
    {

       // print("would have been HandsDrop here");
       
        for (int i = 0; i < hands.Length; i++)
        {
            objDist[i] = hands[i].ObjectDistance(dropObject);
            //Debug.Log("HandsDrop on " + dropObject + " at distance of " + objDist[i]);
        }

        if (objDist[0] < objDist[1])
        {
            hands[0].DropSelection(dropObject);
        } else
        {
            hands[1].DropSelection(dropObject);
        }
    }

}