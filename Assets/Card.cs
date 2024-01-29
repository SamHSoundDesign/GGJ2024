using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string cardID = "BASE CARD";
    public float duration = 3f;
    public bool isPermanent = false;

    protected bool debug = false;

    private bool isLocked = false;

    public virtual void ActivateCard()
    {
        Debug.Log(cardID + "Card played : " + cardID + " Well done you.");

        if (isPermanent == false)
        {
            CardManager.Instance.LockAllNonUsableCards();
            StartCoroutine(DeactiveEffect(duration));
        }
    }
    public virtual void Lock()
    {
        isLocked = true;
    }
    public virtual void UnLock()
    {
        isLocked = false;
    }
    public virtual void DeativateEffects()
    {
        CardManager.Instance.UnLockAllNonUsableCards();

        ActiveCard.Instance.activeCard = null;
        
        if (debug)
            Debug.Log(cardID + " Card played. Well done you.");
        
        Destroy(gameObject);
        
    }

    private void OnMouseDown()
    {
        if (isLocked == false)
        {
            //ActivateCard();
            return;
        }
    }

    protected IEnumerator DeactiveEffect(float wait)
    {
        yield return new WaitForSeconds(wait);
        DeativateEffects();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
