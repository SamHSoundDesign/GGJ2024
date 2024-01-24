using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;

    private List<CardSO> cardsInDeck = new List<CardSO>();
    private List<int> cardsInHand = new List<int>();
    private List<int> availableCardIndexs = new List<int>();

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
        CardManager.Instance.durationCardAcitve = true;

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
        CardManager.Instance.durationCardAcitve = false;

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
        GameManager.Instance.levelChanged += SetupLevel;
        GameManager.Instance.startLevel += StartLevel;
    }
    public void SetupLevel()
    {
        cardsInDeck.Clear();
        cardsInDeck = GameManager.Instance.currentLevelData.availableCardSOs;
    }
    private void StartLevel()
    {
        DealStartingCards();
    }

    private void DealStartingCards()
    {
        LevelDataSO levelDataSO = GameManager.Instance.currentLevelData;
        bool isRandom = levelDataSO.randomiseCardOrder;

        availableCardIndexs.Clear();

        for (int i = 0; i < cardsInDeck.Count; i++)
        {
            availableCardIndexs.Add(i);
        }

        for (int i = 0; i < levelDataSO.startingCardsAmount; i++)
        {
            DealNextCard(isRandom);
        }
    }

    private void DealNextCard(bool random)
    {
        int index;

        if (random)
        {
            index = UnityEngine.Random.Range(0, availableCardIndexs.Count);
        }
        else
        {
            index = 0;
        }

        Card newCard = SpawnCard(index);

        cardsInHand.Add(availableCardIndexs[index]);
        availableCardIndexs.RemoveAt(index);

        if(newCard == null)
        {
            Debug.Log("New card is null");
            return;
        }
            

    }

    public Card SpawnCard(int index)
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
        GameObject prefab = cardsInDeck[availableCardIndexs[index]].prefab;

        GameObject go = Instantiate(prefab, worldPos, Quaternion.identity, cardParent);

        Card card = go.GetComponent<Card>();

        if(durationCardAcitve)
        {
            card.Lock();
        }


        return card;
    }


}
