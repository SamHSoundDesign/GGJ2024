using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    private string defaultText;
    void Start()
    {
        GameManager.Instance.startLevel += UpdateUI;
        GameManager.Instance.pointsChanged += UpdateUI;
        defaultText = tmp.text;
    }

    public void UpdateUI()
    {
        tmp.color = GameManager.Instance.currentLevelData.colorSchemeSO.colorB;
        tmp.text = defaultText + (GameManager.Instance.currentLevelData.points - GameManager.Instance.points);
    }
}
