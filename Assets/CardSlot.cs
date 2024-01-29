using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool hasCard = true;

    public void SetHasCard(bool hasCard)
    {
        this.hasCard = hasCard;
        gameObject.SetActive(hasCard);
    }

}
