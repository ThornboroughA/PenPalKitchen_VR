using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TempSceneChange : MonoBehaviour
{

    public XRNode InputSource;
    public bool outputBool = false;
    private bool isLoading = false;

    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(InputSource);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out outputBool);

        if (outputBool && !isLoading)
        {
            isLoading = true;
            SceneManager.LoadScene("KitchenScene", LoadSceneMode.Single);
        }
    }


}
