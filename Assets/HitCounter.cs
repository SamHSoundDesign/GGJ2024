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
    }

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        defaultText = tmp.text;
    }

    public void UpdateUI()
    {
        tmp.text = defaultText + GameManager.Instance.killCount;
    }
}
