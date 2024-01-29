using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailed : MonoBehaviour
{
    private Vector3 startingPos;
    [SerializeField] private GameObject subParent;
    private void Start()
    {
        startingPos = gameObject.transform.position;
        GameManager.Instance.levelFailed += ActivateLevelComplete;
        GameManager.Instance.deactivateLevelComplete += DeactivateLevelComplete;
        subParent.SetActive(false);
    }

    private void ActivateLevelComplete()
    {
        subParent.SetActive(true);
        //LeanTween.move(gameObject, Vector3.zero, 0.2f);
    }

    private void DeactivateLevelComplete()
    {
        subParent.SetActive(false);

        //LeanTween.move(gameObject, startingPos, 0.2f);

    }
}
