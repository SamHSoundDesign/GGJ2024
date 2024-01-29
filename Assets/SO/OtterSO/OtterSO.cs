using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OtterSO", menuName = "ScriptableObjects/OtterSO", order = 1)]
public class OtterSO : ScriptableObject
{
    public GameObject prefab;
    public float lifeTimeMultiplier;
    public DamageMultipliers damageMultiplier = DamageMultipliers.hundred;
}

public enum DamageMultipliers
{
    hundred,
    hundredfifty,
    twohundred
}
