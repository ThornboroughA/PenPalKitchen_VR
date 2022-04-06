using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public XRNode inputSource;
    private Vector2 inputAxis;
    private XRRig rig;


    private CharacterController character;

    private void Start()
    {
        rig = GetComponent<XRRig>();
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }


    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);
    }

}
