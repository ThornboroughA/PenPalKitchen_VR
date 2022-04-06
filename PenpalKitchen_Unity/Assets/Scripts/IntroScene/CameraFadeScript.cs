using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeScript : MonoBehaviour
{

    [SerializeField] private GameObject canvas;

    [SerializeField] private Transform[] tooClosePoints;


    private void Start()
    {
        canvas.SetActive(true);
    }








}
