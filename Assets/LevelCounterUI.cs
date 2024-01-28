using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private string defaultText;
    void Start()
    {
        GameManager.Instance.levelChanged += UpdateUI;
        defaultText = tmp.text;
    }

    public void UpdateUI()
    {
        tmp.text = defaultText + GameManager.Instance.levelNumber;
    }
}
