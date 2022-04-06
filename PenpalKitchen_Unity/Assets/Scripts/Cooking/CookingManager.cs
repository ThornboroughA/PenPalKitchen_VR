using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BaseIngredients
{
    Null,
    Spinach,
    Egg,
    Carrot,
    Cucumber,
    Rice,
    Danmuji,
    Gim,
    FinishedGimbap
}

public class CookingManager : MonoBehaviour
{
    public List<GameObject> activeIngredientsList = new List<GameObject>();

    public List<BaseIngredients> neededIngredients;

    public static BaseIngredients currentIngredient;

    public GameObject destroyPrefab;

    #region Singleton
    public static CookingManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of CookingManager found!");
            return;
        }
        instance = this;
    }
    #endregion

}
