using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script snaps objects (e.g. pans) back to their home points
/// It also destroys disposable objects (e.g. ingredients) if thrown out of bounds
/// </summary>
public class SnapBack : MonoBehaviour
{

    public bool inSnapPoint = false;


    [SerializeField] private Transform snapHome;
    private Rigidbody rigidBody;


    private Coroutine lerpRoutine;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void PickUp()
    {
        if (lerpRoutine != null)
        {
            StopCoroutine(lerpRoutine);
        }
        //rigidBody.useGravity = true;
        rigidBody.constraints = RigidbodyConstraints.None;
    }

    public void StartSnapBack()
    {
        if (inSnapPoint == false)
        {
            lerpRoutine = StartCoroutine(LerpBack());
        }
    }


    private IEnumerator LerpBack()
    {

        yield return new WaitForSeconds(3f);

        if (inSnapPoint == true)
        {

            yield break;
        }

        float duration = 3f;
        float timeElapsed = 0f;

        //rigidBody.useGravity = false;

        while (timeElapsed < duration)
        {
                
            transform.position = Vector3.Lerp(transform.position, snapHome.position, timeElapsed / duration);
            transform.rotation = Quaternion.Lerp(transform.rotation, snapHome.rotation, timeElapsed / duration);

            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;

            timeElapsed += Time.deltaTime;
            yield return null;
        }


    }



}
