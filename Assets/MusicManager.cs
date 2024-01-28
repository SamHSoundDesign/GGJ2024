using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicMainMenu;
    [SerializeField] private AudioClip gameLoopClip1;
    [SerializeField] private AudioClip gameLoopClip2;
    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.startTimer += StartTimer;
    
        GameManager.Instance.startLevel += StartLevel;
        GameManager.Instance.levelComplete += LevelComplete;
        GameManager.Instance.levelFailed += LevelFailed;

        musicSource.clip = musicMainMenu;
        musicSource.Play();
    }

    private void StartTimer()
    {
         
    }


    private void StartLevel()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
            

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
