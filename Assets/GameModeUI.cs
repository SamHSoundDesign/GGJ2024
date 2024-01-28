using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameModeUI : MonoBehaviour
{
    public bool colorChange = false;
    public ColorOption colorOption = ColorOption.b;
    private void Start()
    {
        GameManager.Instance.startLevel += Activate;
        GameManager.Instance.levelComplete += Deactivate;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);

        if(colorChange)
        {
            Color color = GetColor(colorOption);

            TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                
                tmp.color = color;
            }

            Image image = GetComponent<Image>();

            if(image != null)
            {
                image.color = color;
            }
        }
    }

    public Color GetColor(ColorOption colorOption)
    {
        Color color;
        switch (colorOption)
        {
            case ColorOption.a:
                color = GameManager.Instance.currentLevelData.colorSchemeSO.colorA;
                break;
            case ColorOption.b:
                color = GameManager.Instance.currentLevelData.colorSchemeSO.colorB;
                break;
            case ColorOption.c:
                color = GameManager.Instance.currentLevelData.colorSchemeSO.colorC;
                break;
            default:
                color = GameManager.Instance.currentLevelData.colorSchemeSO.colorB;
                break;
        }

        return color;
    }
}
