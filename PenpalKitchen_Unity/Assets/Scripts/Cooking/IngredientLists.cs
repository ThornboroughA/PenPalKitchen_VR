using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientLists : MonoBehaviour
{

    public List<GameObject> activeEggs = new List<GameObject>();
    public List<GameObject> activeCarrots = new List<GameObject>();
    public List<GameObject> activeSpinach = new List<GameObject>();
    public List<GameObject> activeDanmuji = new List<GameObject>();
    public List<GameObject> activeCucumber = new List<GameObject>();
    public List<GameObject> activeGim = new List<GameObject>();

    public List<GameObject> activeRice = new List<GameObject>();

    
    public int CheckNumber(GameObject ingToAdd)
    {
        BaseIngredients ingredientType = ingToAdd.GetComponent<Ingredient>().baseIngredient;
        int count = 0;

        switch (ingredientType)
        {
            case BaseIngredients.Egg:
                count = activeEggs.Count;
                break;
            case BaseIngredients.Carrot:
                count = activeCarrots.Count;
                break;
            case BaseIngredients.Spinach:
                count = activeSpinach.Count;
                break;
            case BaseIngredients.Danmuji:
                count = activeDanmuji.Count;
                break;
            case BaseIngredients.Cucumber:
                count = activeCucumber.Count;
                break;
            case BaseIngredients.Rice:
                count = activeRice.Count;
                break;
            case BaseIngredients.Gim:
                count = activeGim.Count;
                break;
        }
        return count;
    }
    
    public void AddIngredient(GameObject ingToAdd)
    {
        BaseIngredients ingredientType = ingToAdd.GetComponent<Ingredient>().baseIngredient;

        switch(ingredientType)
        {
            case BaseIngredients.Egg:
                activeEggs.Add(ingToAdd);
                break;
            case BaseIngredients.Carrot:
                activeCarrots.Add(ingToAdd);
                break;
            case BaseIngredients.Spinach:
                activeSpinach.Add(ingToAdd);
                break;
            case BaseIngredients.Danmuji:
                activeDanmuji.Add(ingToAdd);
                break;
            case BaseIngredients.Cucumber:
                activeCucumber.Add(ingToAdd);
                break;
            case BaseIngredients.Rice:
                activeRice.Add(ingToAdd);
                break;
            case BaseIngredients.Gim:
                activeGim.Add(ingToAdd);
                break;
            default:
                
                break;
        }
    }
    public void RemoveIngredient(GameObject ingToRemove)
    {
        BaseIngredients ingredientType = ingToRemove.GetComponent<Ingredient>().baseIngredient;

        switch (ingredientType)
        {
            case BaseIngredients.Egg:
                activeEggs.Remove(ingToRemove);
                break;
            case BaseIngredients.Carrot:
                activeCarrots.Remove(ingToRemove);
                break;
            case BaseIngredients.Spinach:
                activeSpinach.Remove(ingToRemove);
                break;
            case BaseIngredients.Danmuji:
                activeDanmuji.Remove(ingToRemove);
                break;
            case BaseIngredients.Cucumber:
                activeCucumber.Remove(ingToRemove);
                break;
            case BaseIngredients.Rice:
                activeRice.Remove(ingToRemove);
                break;
            case BaseIngredients.Gim:
                activeGim.Remove(ingToRemove);
                break;
        }
    }



}
