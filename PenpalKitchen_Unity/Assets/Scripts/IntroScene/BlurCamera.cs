using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurCamera : MonoBehaviour
{

    [SerializeField] PostProcessVolume volume = null;
    [SerializeField] private float timeToBlur = 2f;

    private DepthOfField depthOfField = null;


    private void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
    }


    public void StartBlur()
    {
        StartCoroutine(EnableDoF(timeToBlur));
    }

    private IEnumerator EnableDoF(float lerpDuration)
    {
        yield return null;

        print("activate");
        depthOfField.enabled.value = true;

        float timeElapsed = 0;

        float fl = 0f;

        while (timeElapsed < lerpDuration)
        {
            fl = Mathf.Lerp(0, 300, timeElapsed / lerpDuration);
            depthOfField.focalLength.value = fl;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        
    }

}
