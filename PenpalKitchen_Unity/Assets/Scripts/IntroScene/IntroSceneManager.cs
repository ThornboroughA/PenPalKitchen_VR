using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class IntroSceneManager : MonoBehaviour
{
 
    public enum SceneStatus : int { OneBedroom, TwoKitchen , ThreeStreet }

    private SceneStatus currentScene;

    [SerializeField] private MeshRenderer alphaTest;

    [SerializeField] [Range(0.0f, 1.0f)] private float alphaValue;


    [SerializeField] private GameObject[] comicPanel;
    [SerializeField] private MeshRenderer[] comicFades;

    private int currentItemNumber; //the number on the current scene that's been selected -- when hits certain amount, toggles next scene
    private int currentSceneNumItems;


    private void Update()
    {
        alphaTest.material.color = new Color(1f,1f,1f,alphaValue);
    }



    public void UserGrabbed()
    {
        //user has interacted with collider
        //this should probably be on the indiviual grabbable item

        //IntroSceneManager.currentItemNumber += 1;
    }

    private void SceneManage()
    {
        if (currentItemNumber == currentSceneNumItems)
        {
            //SceneSwitchStart();
        }
    }

}




