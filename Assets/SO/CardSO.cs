using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "ScriptableObjects/CardSO", order = 1)]
public class CardSO : ScriptableObject
{
    public string cardID = "DefaultCardID";
    public string description = "Insert Description here";
    public GameObject prefab;
    public Sprite sprite;
}


