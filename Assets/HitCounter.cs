using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    string defaultText;
    private TextMeshProUGUI tmp;
    private int hits = 0;
    private void Awake()
    {
        UIManager.Instance.hitAction += UpdateUI;
    }

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        defaultText = tmp.text;
    }

    public void UpdateUI()
    {
        hits += 1;

        tmp.text = defaultText + hits;
    }
}
