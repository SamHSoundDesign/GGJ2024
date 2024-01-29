using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public List<CardSO> cardsInDeck;
    public List<CardSlot> cardSlots = new List<CardSlot>();
    public bool durationCardAcitve;

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
    public void LockAllNonUsableCards()
    {
        durationCardAcitve = true;

    }
    public void UnLockAllNonUsableCards()
    {
        durationCardAcitve = false;

        GameManager.Instance.InitialiseCardEffects();
        ActiveCard.Instance.DeactivateActiveCard();
    }

    public void Start()
    {
        GameManager.Instance.pointsChanged += SetupLevel;
        GameManager.Instance.startLevel += StartLevel;

    }
    public void SetupLevel()
    {

    }
    private void StartLevel()
    {
        for (int i = 0; i < GameManager.Instance.currentLevelData.startingCardsAmount; i++)
        {
            cardSlots[i].SetHasCard(true);
        }
    }

    public void PlayCard(int cardSlot)
    {
        if(cardSlot < 0 || cardSlot > 3)
        {
            return;
        }

        if(cardSlots[cardSlot].hasCard && durationCardAcitve == false)
        {
            cardSlots[cardSlot].SetHasCard(false);

            int random = UnityEngine.Random.Range(0, cardsInDeck.Count);
            ActiveCard.Instance.SetupActiveCard(cardsInDeck[random]);
            
            GameObject playedCarGOd = Instantiate(cardsInDeck[random].prefab, Vector3.zero, Quaternion.identity, transform);
            Card playedCard = playedCarGOd.GetComponent<Card>();
            playedCard.ActivateCard();
            ActiveCard.Instance.activeCard = playedCard;
        }

    }
}
