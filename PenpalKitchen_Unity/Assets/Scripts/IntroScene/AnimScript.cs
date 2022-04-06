using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{
    [SerializeField] private Animator[] _animator;
    private IntroManagerRough _introManager;
    private OutroManager _outroManager;

    [SerializeField] private int totalClips;
   [SerializeField] private int currentClip;

    private bool sceneFinished = false;

    private bool cooldown = false;

    private NextFrame_rough nextRough;
    private bool boolLock;

    public bool outro = false;

    /// <summary>
    /// Attached to each scene in the Intro
    /// 
    /// Activate InteractPress() to manually iterate through the scenes with the pencil
    /// When all scenes have been seen, playContinuous activates, making the scene automatically play
    /// totalClips should be based off number of clips in _animator
    /// </summary>

    private void Start()
    {
        nextRough = FindObjectOfType<NextFrame_rough>();

        if (!outro)
        {
            _introManager = GameObject.FindGameObjectWithTag("IntroManager").GetComponent<IntroManagerRough>();
        } else
        {
            _outroManager = FindObjectOfType<OutroManager>();
        }
    }

    private void Update()
    {
            /* if (Input.GetKeyDown("w"))
             {
                 InteractPress();
             }*/
            if (nextRough.outputBool == true && boolLock == false)
            {
                boolLock = true;
                InteractPress();
            }

            foreach (Animator animator in _animator)
            {
                animator.SetInteger("currentClip", currentClip);
            }

            if ((currentClip > totalClips) && sceneFinished == false)
            {
                StartCoroutine(FinishScene());
            }
    }


    public void InteractPress()
    {
        if (cooldown == false)
        {
            Debug.Log("clip iteration");
            currentClip++;
            StartCoroutine(CoolDown());
            NextFrame_rough.instance.PlayAudio();
        }
    }
    private IEnumerator CoolDown()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        cooldown = false;
        boolLock = false;
    }


    private IEnumerator FinishScene()
    {
        yield return new WaitForSeconds(1.5f);

        Debug.Log("finish");

        sceneFinished = true;
        if (!outro)
        {
            _introManager.IterateScene();
        } else
        {
            _outroManager.IterateScene();
        }
    }


}
