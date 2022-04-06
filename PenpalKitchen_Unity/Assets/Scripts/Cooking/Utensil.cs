using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UtensilTypes { Null, Knife, Pan, Hotplate, Spatula, Plate, Bowl, Bal, StartBal, GimbapRice }

public class Utensil : MonoBehaviour
{


    private enum InteractionType { collision, trigger, timer, gimbap }
    [SerializeField] private InteractionType interactionType = InteractionType.collision;

    public UtensilTypes thisUtensil;

    [SerializeField] private bool disableColliderOnHit;

    [Tooltip("What ingredients snap to. Must have 0 children.")]public Transform ingSnapTo;


    //For detecting which hand it's in
    [SerializeField] private HandsDrop[] hands;
    private float[] objDist = { 0, 0 };

    private bool cooldown = false;

    private void Start()
    {
        foreach (HandsDrop hand in hands)
        {
            if (hand == null)
            {
                Debug.LogError("HandsDrop missing in Utensil.cs of " + gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cooldown == false)
        {
            StartCoroutine(CoolDownTimer());

            if (interactionType == InteractionType.collision && ingSnapTo.childCount == 0)
            {
                if (collision.gameObject.tag == "Ingredient")
                {
                    Ingredient collIng = collision.gameObject.GetComponent<Ingredient>();

                    collIng.TransmuteTo(thisUtensil, ingSnapTo);

                }
            }
            else if (interactionType == InteractionType.gimbap)
            {

                if (collision.gameObject.tag == "Ingredient")
                {
                    Ingredient collIng = collision.gameObject.GetComponent<Ingredient>();

                    for (int i = 0; i < ingSnapTo.childCount; i++)
                    {
                        if (collIng.baseIngredient == ingSnapTo.GetChild(i).gameObject.GetComponent<Ingredient>().baseIngredient)
                        {
                            return;
                        }
                        else
                        {
                            collIng.TransmuteTo(thisUtensil, ingSnapTo);
                        }
                    }
                    if (ingSnapTo.childCount == 0)
                    {
                        collIng.TransmuteTo(thisUtensil, ingSnapTo);
                    }
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (cooldown == false)
        {
            StartCoroutine(CoolDownTimer());

            if (interactionType == InteractionType.trigger && ingSnapTo.childCount == 0)
            {
                if (other.gameObject.tag == "Ingredient")
                {
                    Ingredient trigIng = other.gameObject.GetComponent<Ingredient>();

                    trigIng.TransmuteTo(thisUtensil, ingSnapTo);
                }
            }
        }
    }

    public void UtensilInHand()
    {
        //print(objDist[0] + " objDist, " + hands[0].ObjectDistance(transform));
        objDist[0] = hands[0].ObjectDistance(transform);
        objDist[1] = hands[1].ObjectDistance(transform);

       
        if (objDist[0] < objDist[1])
        {
            hands[0].hasUtensil = !hands[0].hasUtensil;
        }
        else
        {
            hands[1].hasUtensil = !hands[1].hasUtensil;
        }
    }

    private IEnumerator CoolDownTimer()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.15f);
        cooldown = false;
    }

}
