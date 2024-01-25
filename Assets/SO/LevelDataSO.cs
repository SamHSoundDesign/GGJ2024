using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelDataSO : ScriptableObject
{
    public int levelNumber = 0;
    public List<CardSO> availableCardSOs;
    public int startingCardsAmount = 3;
    public bool randomiseCardOrder = false;
    public int totalKills = 0;
    public int reachHighMultiplier = 0;
    public float levelTimeLimit = 30f;

}
