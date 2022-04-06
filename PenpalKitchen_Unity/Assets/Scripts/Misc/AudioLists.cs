using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLists : MonoBehaviour
{
    [SerializeField] private AudioClip[] miscSounds, selectionSounds, gimbapPut;
    [SerializeField] private AudioClip[] chop, dingDong, dragCancel, effect, fryPan, ingComplete, paper, toolSelect, wooshTransition, eggBreak;

    #region Singleton
    public static AudioLists instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of AudioLists found!");
            return;
        }
        instance = this;
    }
    #endregion

    public AudioClip GiveRandomClip(string clipType)
    {
        //there has got to be a better way of doing this

        AudioClip clip = null;

        switch (clipType)
        {
            case "chop":
                clip = chop[Random.Range(0, chop.Length)];
                break;
            case "dingDong":
                clip = dingDong[Random.Range(0, dingDong.Length)];
                break;
            case "dragCancel":
                clip = dragCancel[Random.Range(0, dragCancel.Length)];
                break;
            case "effect":
                clip = effect[Random.Range(0, effect.Length)];
                break;
            case "fryPan":
                clip = fryPan[Random.Range(0, fryPan.Length)];
                break;
            case "ingComplete":
                clip = ingComplete[Random.Range(0, ingComplete.Length)];
                break;
            case "paper":
                clip = paper[Random.Range(0, paper.Length)];
                break;
            case "toolSelect":
                clip = toolSelect[Random.Range(0, toolSelect.Length)];
                break;
            case "wooshTransition":
                clip = wooshTransition[Random.Range(0, wooshTransition.Length)];
                break;
            case "miscSounds":
                clip = miscSounds[Random.Range(0, miscSounds.Length)];
                break;
            case "eggBreak":
                clip = eggBreak[Random.Range(0, eggBreak.Length)];
                break;
            case "selectionSounds":
                clip = selectionSounds[Random.Range(0, selectionSounds.Length)];
                break;
            case "gimbapPut":
                clip = gimbapPut[Random.Range(0, gimbapPut.Length)];
                break;
            case "null":
                break;
            default:
                Debug.LogError(clipType + " string is incorrect; matches no AudioClip");
                break;
        }
        return clip;
    }
    public AudioClip GiveClip(string clipType, int clipValue)
    {
        //there has got to be a better way of doing this

        AudioClip clip = null;

        switch (clipType)
        {
            case "chop":
                clip = chop[clipValue];
                break;
            case "dingDong":
                clip = dingDong[clipValue];
                break;
            case "dragCancel":
                clip = dragCancel[clipValue];
                break;
            case "effect":
                clip = effect[clipValue];
                break;
            case "fryPan":
                clip = fryPan[clipValue];
                break;
            case "ingComplete":
                clip = ingComplete[clipValue];
                break;
            case "paper":
                clip = paper[clipValue];
                break;
            case "toolSelect":
                clip = toolSelect[clipValue];
                break;
            case "wooshTransition":
                clip = wooshTransition[clipValue];
                break;
            case "miscSounds":
                clip = miscSounds[clipValue];
                break;
            default:
                Debug.LogError(clipType + " string is incorrect; matches no AudioClip");
                break;
        }
        return clip;
    }


}
