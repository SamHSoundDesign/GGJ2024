using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private List<CardSO> cardsInDeck = new List<CardSO>();
    private List<Card> cardsInHand = new List<Card>();

    public Transform cardParent;

    public List<Transform> cardSlots = new List<Transform>();
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

        for (int i = 0; i < cardSlots.Count; i++)
        {
            Card card = cardSlots[i].gameObject.GetComponent<Card>();

            if(card != null)
            {
                card.Lock();
            }
        }

    }
    public void UnLockAllNonUsableCards()
    {
        durationCardAcitve = false;

        for (int i = 0; i < cardSlots.Count; i++)
        {
            Card card = cardSlots[i].gameObject.GetComponent<Card>();

            if (card != null)
            {
                card.UnLock();
            }
        }
    }

    public void Start()
    {
        GameManager.Instance.pointsChanged += SetupLevel;
        GameManager.Instance.startLevel += StartLevel;

    }
    public void SetupLevel()
    {
        cardsInDeck = GameManager.Instance.currentLevelData.availableCardSOs;
    }
    private void StartLevel()
    {
        foreach (Card card in cardsInHand)
        {
            card.Destroy();
        }

        cardsInHand.Clear();
        cardsInHand = new List<Card>();

        DealStartingCards();
    }

    private void DealStartingCards()
    {
        LevelDataSO levelDataSO = GameManager.Instance.currentLevelData;
        bool isRandom = levelDataSO.randomiseCardOrder;

        for (int i = 0; i < levelDataSO.startingCardsAmount; i++)
        {
            DealNextCard(isRandom, levelDataSO); ;
        }
    }

    private void DealNextCard(bool random, LevelDataSO levelDataSO)
    {
        int index;

        index = UnityEngine.Random.Range(0, levelDataSO.availableCardSOs.Count);
        Card newCard = SpawnCard(levelDataSO.availableCardSOs[index]);

        if (newCard == null)
        {
            Debug.Log("New card is null");
            return;
        }  

    }

    public Card SpawnCard(CardSO cardSO)
    {
        if(cardsInDeck.Count == 0)
        {
            Debug.Log("Not enough cards in the deck. Currently the deck has " + cardsInDeck.Count + " cards in it");
            return null;
        }

        if(cardsInHand.Count >= cardSlots.Count)
        {
            Debug.Log("Hand is currently full");
            return null;
        }

        int slot = cardsInHand.Count;
        Vector3 worldPos = cardSlots[slot].position;
        GameObject prefab = cardSO.prefab;

        GameObject go = Instantiate(prefab, worldPos, Quaternion.identity, cardParent);
        go.GetComponent<CardPrefab>().Setup(cardSO);
        Card card = go.GetComponent<Card>();
        cardsInHand.Add(card);


        if(durationCardAcitve)
        {
            card.Lock();
        }


        return card;
    }


}
