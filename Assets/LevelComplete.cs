using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.levelComplete += ActivateLevelComplete;
    }

    private void ActivateLevelComplete()
    {
        gameObject.SetActive(true);
    }
}
