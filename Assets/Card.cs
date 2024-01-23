using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardID = "BASE CARD";
    public float duration = 3f;
    public bool isPermanent = false;

    public virtual void ActivateCard()
    {
        Debug.Log(cardID + "Card played : " + cardID + " Well done you.");

        if (isPermanent == false)
        {
            StartCoroutine(DeactiveEffect(duration));
        }
    }

    public virtual void DeativateEffects()
    {
        Debug.Log(cardID + " Card played. Well done you.");
    }

    private void OnMouseDown()
    {
        ActivateCard();
    }

    protected IEnumerator DeactiveEffect(float wait)
    {
        yield return new WaitForSeconds(wait);
        DeativateEffects();
    }
}
