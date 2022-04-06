using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    private IngredientLists ingredientLists;

    [SerializeField] private int maxCount;
    [SerializeField] private bool spawnAtStart = true;
    
    [SerializeField] private GameObject ingredientType;

    [SerializeField] private bool spawnWhenEmpty = false;
    private bool currentlyActive = false;

    private void Start()
    {

        ingredientLists = GameObject.FindGameObjectWithTag("CookingManager").GetComponent<IngredientLists>();
        if (spawnAtStart)
        {
            SpawnIngredient();
        }
    }

    private void Update()
    {
        if (spawnWhenEmpty == true)
        {
            SpawnIngredient();
        } 
    }



    public void SpawnIngredient()
    {
        if (transform.childCount < 1)
        {
            if (ingredientLists.CheckNumber(ingredientType) > maxCount - 1)
            {
                print("Already too many " + ingredientType);
            }
            else
            {
                GameObject toInstantiate = Instantiate(ingredientType, transform.position, transform.rotation, transform);

                ingredientLists.AddIngredient(toInstantiate);
            }
        }
    }


}
