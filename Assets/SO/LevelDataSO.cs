using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelDataSO : ScriptableObject
{
    public int levelNumber = 0;
    public int startingCardsAmount = 3;
    public bool randomiseCardOrder = false;
    public int points = 1000;
    public float levelTimeLimit = 30f;
    public float respawnRate = 0.5f;
    public float lifeSpan = 3f;
    public ColorSchemeSO colorSchemeSO;
    public AudioClip levelMusic;
}
