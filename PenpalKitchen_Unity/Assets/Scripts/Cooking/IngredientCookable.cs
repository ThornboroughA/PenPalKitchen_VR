using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCookable : MonoBehaviour
{
    public int cookTime = 5;
    public int currentCookAmount = 0;

    private bool isCooked = false;

    private void Update()
    {
        if (currentCookAmount >= cookTime)
        {
            if (isCooked == false)
            {
                if (GetComponent<IngredientTransmute>())
                {
                    GetComponent<IngredientTransmute>().ReplaceSelf();
                }
                else
                {
                    Debug.LogError("Add IngredientTransmute to " + gameObject);
                }
                isCooked = true;
            }
        }
    }
}
