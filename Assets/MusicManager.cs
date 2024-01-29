using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource countdownSource;
    [SerializeField] private AudioClip musicMainMenu;
    [SerializeField] private AudioClip countDownClip;
    [SerializeField] private AudioClip gameLoopClip1;
    [SerializeField] private AudioClip gameLoopClip2;

    private float defaultVol;
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.startTimer += StartTimer;
        GameManager.Instance.countdownStart += CountdownStart;
        GameManager.Instance.startLevel += StartLevel;
        GameManager.Instance.levelComplete += LevelComplete;
        GameManager.Instance.levelFailed += LevelFailed;

        defaultVol = musicSource.volume;
        musicSource.clip = musicMainMenu;
        musicSource.volume = defaultVol + GameManager.Instance.volumeModifier;
        musicSource.Play();
    }

    private void StartTimer()
    {
         
    }

    private void CountdownStart()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        countdownSource.volume = defaultVol + GameManager.Instance.volumeModifier;
        countdownSource.clip = countDownClip;
        countdownSource.Play();
    }


    private void StartLevel()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        musicSource.volume = defaultVol + GameManager.Instance.volumeModifier;
        musicSource.clip = GameManager.Instance.currentLevelData.levelMusic;
        musicSource.Play();
    }

    private void LevelComplete()
    {
        musicSource.Stop();
    }

    private void LevelFailed()
    {
        musicSource.Stop();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
