using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private string defaultText;
    void Start()
    {
        GameManager.Instance.pointsChanged += UpdateUI;
        defaultText = tmp.text;
    }

    public void UpdateUI()
    {
        tmp.color = GameManager.Instance.currentLevelData.colorSchemeSO.colorB;
        tmp.text = defaultText + GameManager.Instance.levelNumber;
    }
}
