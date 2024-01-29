using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool hasCard = false;

    public void SetHasCard(bool hasCard)
    {
        this.hasCard = hasCard;
        gameObject.SetActive(hasCard);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

}
