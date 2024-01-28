using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    string defaultText;
    private TextMeshProUGUI tmp;
    private void Awake()
    {
        GameManager.Instance.otterKilled += UpdateUI;
        GameManager.Instance.startLevel += UpdateUI;
    }

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        defaultText = tmp.text;
    }

    public void UpdateUI()
    {
        if(tmp == null)
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }
        tmp.color = GameManager.Instance.currentLevelData.colorSchemeSO.colorB;
        tmp.text = defaultText + GameManager.Instance.killCount;
    }

}
