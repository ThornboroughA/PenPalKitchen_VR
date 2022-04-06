using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPresence : MonoBehaviour
{

    [SerializeField] private InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;


    [SerializeField] private GameObject handModelPrefab;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

   // public bool primaryButtonValue;


    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);


     

        /*foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }*/
        Debug.Log(devices.Count);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            if (handModelPrefab)
            {
                spawnedHandModel = Instantiate(handModelPrefab, transform);
                handAnimator = spawnedHandModel.GetComponent<Animator>();

            }
            else
            {
                Debug.LogError("Did not find hand model for controller");
            }
        }

        

    }
    private void Update()
    {
        UpdateHandAnimation();

       // targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
      //  print(primaryButtonValue);

    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }/* else
        {
            handAnimator.SetFloat("Trigger", 0);
        }*/

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }/*
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }*/

    }

 



}
