using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownClock : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string prefix = "Time rem : ";

    public bool isActive = false;


    private void Start()
    {
        GameManager.Instance.startLevel += StartCountdown;
        GameManager.Instance.levelFailed += HideCountdown;

        tmp = GetComponent<TextMeshProUGUI>();
    }
    public void HideCountdown()
    {
        tmp.text = "";
        isActive = false;

    }
    private void Update()
    {
        if(isActive)
        {
            SetUI((GameManager.Instance.levelEndTime - Time.time));
        }
    }

    public void StartCountdown()
    {
        SetUI(GameManager.Instance.levelEndTime - Time.time);
        isActive = true;
    }

    public void SetUI(float timeRemaining)
    {
        tmp.text = prefix + timeRemaining.ToString("F2");
    }
}
