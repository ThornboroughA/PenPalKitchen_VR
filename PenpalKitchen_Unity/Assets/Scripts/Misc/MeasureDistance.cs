using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{

    [SerializeField] private Transform rulerPointA;
    [SerializeField] private Transform rulerPointB;

    private void Start()
    {
        float distA = Vector3.Distance(rulerPointA.position, transform.position);
        float distB = Vector3.Distance(rulerPointB.position, transform.position);

        Debug.Log("Ruler A distance is: " + distA + ", Ruler B distance is " + distB);
    }


}
