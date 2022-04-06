using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

    private Transform originalRot;
    private Coroutine coroutine;

    private Vector3 startRotation;

    private enum rotationAxis { x, y, z}
    [SerializeField] private rotationAxis thisAxis = rotationAxis.z;

    void Start()
    {
        startRotation = transform.localEulerAngles;
    }


    public void Selected()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    public void Deselected()
    {
        coroutine = StartCoroutine(CloseAgain());
    }

    private IEnumerator CloseAgain()
    {
        yield return new WaitForSeconds(3f);

        float duration = 2f;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {

            Vector3 currentRotation = transform.localEulerAngles;
            if (thisAxis == rotationAxis.z)
            {
                currentRotation.z = Mathf.Lerp(currentRotation.z, startRotation.z, timeElapsed / duration);
            } else if (thisAxis == rotationAxis.y)
            {
                currentRotation.y = Mathf.Lerp(currentRotation.y, 359.5f, timeElapsed / duration);

            } if (thisAxis == rotationAxis.x)
            {
                currentRotation.x = Mathf.Lerp(currentRotation.x, startRotation.x, timeElapsed / duration);
            }

            transform.localEulerAngles = currentRotation;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }



}
