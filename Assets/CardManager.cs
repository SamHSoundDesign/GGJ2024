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
    public event Action cardPlayed;

    private bool carDealTimerStarted = false;
    private float cardRefreshRate = 10;
    private float cardRefreshTime;

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
    
    public void ResetManager()
    {
        carDealTimerStarted = false;
    }
    private void Update()
    {
        if(GameManager.Instance.gamestate == Gamestate.Running)
        {
            if (carDealTimerStarted == false)
            {
                cardRefreshTime = Time.time + cardRefreshRate;
                carDealTimerStarted = true;
                

            }
            else
            {
                ReplenishACard();
            }
        }
    }

    private void ReplenishACard()
    {
        if (Time.time > cardRefreshTime)
        {
            for (int i = 0; i < cardSlots.Count; i++)
            {
                if (cardSlots[i].hasCard == false)
                {
                    cardSlots[i].SetHasCard(true);
                    cardRefreshTime = Time.time + cardRefreshRate;
                    break;
                }
            }
        }
    }

    public void LockAllNonUsableCards()
    {
        durationCardAcitve = true;

    }
    public void UnLockAllNonUsableCards()
    {
        durationCardAcitve = false;
        cardRefreshTime = Time.time + cardRefreshRate;

        GameManager.Instance.InitialiseCardEffects();
        ActiveCard.Instance.DeactivateActiveCard();

    }

    public void Start()
    {
        GameManager.Instance.pointsChanged += SetupLevel;   
        GameManager.Instance.startLevel += StartLevel;

        //gameObject.SetActive(false);

    }
    public void SetupLevel()
    {
        ResetManager();
    }
    private void StartLevel()
    {
        ResetManager();
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
            cardPlayed?.Invoke();
        }


    }
}
