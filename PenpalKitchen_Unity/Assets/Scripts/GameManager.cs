using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject faderCanvas;
    public Image faderImg;


    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found!");
            return;
        }
        instance = this;
    }
    #endregion


    public void NewSceneTransition(int timeToTransition)
    {
        StartCoroutine(TransitionScene(timeToTransition));
    }

    private IEnumerator TransitionScene(int timeToTransition)
    {
        yield return new WaitForSeconds(timeToTransition);

        StartCoroutine(FadeOut("KitchenScene"));

       // SceneManager.LoadScene("KitchenScene", LoadSceneMode.Single);

    }


    public void OutroSceneTransition(int timeToTransition)
    {
        StartCoroutine(TransitionEnd(timeToTransition));
    }

    private IEnumerator TransitionEnd(int timeToTransition)
    {
        //yield return new WaitForSeconds(timeToTransition);
        

        StartCoroutine(FadeOut("OutroScene"));
        yield return null;
        //SceneManager.LoadScene("OutroScene", LoadSceneMode.Single);
    }

    private IEnumerator FadeOut(string sceneName)
    {
        faderCanvas.SetActive(true);
        faderImg.canvasRenderer.SetAlpha(0);
        faderImg.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1);

        //SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName);

    }

}
