using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimbapFinal : MonoBehaviour
{


    void Start()
    {
        WinScript.instance.UpdateAnimation(6);
        WinScript.instance.ShowPutHere();
    }

}
