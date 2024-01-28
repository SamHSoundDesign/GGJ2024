using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CardPrefab : MonoBehaviour
{
    public CardSO cardSO;

    [SerializeField] private TextMeshPro title_tmp;
    [SerializeField] private TextMeshPro description_tmp;
    [SerializeField] private SpriteRenderer icon;

    private void Start()
    {
        
    }

    public void Setup(CardSO cardSO)
    {
        this.cardSO = cardSO;
        title_tmp.text = cardSO.cardID;
        description_tmp.text = cardSO.description;
        icon.sprite = cardSO.sprite;
    }
}
