using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownClock : MonoBehaviour
{
    public static CountdownClock Instance;
    public TextMeshProUGUI tmp;
    public string prefix = "Time rem : ";

    public bool isActive = false;
    private bool fiveSecondTimerActivated;

    public event Action fiveSecondCountDown;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
        fiveSecondTimerActivated = false;
        SetUI(GameManager.Instance.levelEndTime - Time.time);
        isActive = true;
    }

    public void SetUI(float timeRemaining)
    {

        if (timeRemaining < 5f && fiveSecondTimerActivated == false)
        {
            fiveSecondCountDown?.Invoke();
            fiveSecondTimerActivated = true;
        }
        tmp.text = prefix + timeRemaining.ToString("F1");
    }
}
