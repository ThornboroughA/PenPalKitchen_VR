using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookManager : MonoBehaviour
{

    #region Singleton
    public static RecipeBookManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of RecipeBookManager found!");
            return;
        }
        instance = this;
    }
    #endregion

    public BaseIngredients currentIngredient;

    [SerializeField] private Sprite[] recipePages = null;

    [SerializeField] private SpriteRenderer cookbookRenderer = null;

    private void Update()
    {

        print(currentIngredient);
        

        switch(currentIngredient)
        {
            case (BaseIngredients.Carrot):

                cookbookRenderer.sprite = recipePages[0];

                break;
            case (BaseIngredients.Spinach):

                cookbookRenderer.sprite = recipePages[1];

                break;
            case (BaseIngredients.Egg):

                cookbookRenderer.sprite = recipePages[2];

                break;
            case (BaseIngredients.Cucumber):

                cookbookRenderer.sprite = recipePages[3];

                break;
            case (BaseIngredients.Danmuji):

                cookbookRenderer.sprite = recipePages[4];

                break;
            case (BaseIngredients.Null):

                cookbookRenderer.sprite = recipePages[5];

                break;
            default:
                cookbookRenderer.sprite = recipePages[0];
                break;
        }

    }

}
