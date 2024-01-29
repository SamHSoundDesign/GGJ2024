using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCard : MonoBehaviour
{
    public static ActiveCard Instance;

    public Image icon;
    public TextMeshProUGUI title_tmp;
    public TextMeshProUGUI description_tmp;
    public Card activeCard;
    public event Action cardExpired;

    private bool setupComplete = false;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DeactivateActiveCard();
    }

    public void SetupActiveCard(CardSO cardSO)
    {
        icon.sprite = cardSO.sprite;
        title_tmp.text = cardSO.cardID;
        description_tmp.text = cardSO.description;

        ActivateCard();
    }

    public void DeactivateActiveCard()
    {
        if (setupComplete)
        {
            cardExpired?.Invoke();
        }

        gameObject.SetActive(false);
        setupComplete = true;

    }

    public void ActivateCard()
    {
        gameObject.SetActive(true);

    }
}
