using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public OtterSO otterASO;
    public OtterSO otterBSO;
    public OtterSO otterCSO;
    public Gamestate gamestate = Gamestate.Menu;
    private GameEffect gameEffect = GameEffect.None;

    public event Action startLevel;
    public event Action countdownStart;
    public event Action startGame;
    public event Action gamePaused;
    public event Action gameUnPaused;
    public event Action levelComplete;
    public event Action deactivateLevelComplete;
    public event Action pointsChanged;
    public event Action levelFailed;

    public float volumeModifier = -0.3f;

    public int levelNumber = 0;
    public List<LevelDataSO> levelDatas;
    public LevelDataSO currentLevelData;
    public event Action otterHit;
    public event Action otterKilled;

    public float levelStartTime;
    public float levelEndTime;

    public int damagePerClick = 2;

    public float gameIntroSoundLenth = 3f;

    //Particles
    public GameObject otterDeath_particle;
    public GameObject hundredParticle; 
    public GameObject hundredFiftyParticle;
    public GameObject twoHundredParticle;

    public float respawnRate;
    public float lifeSpan = 0.5f;
    private float defaultSpeed;
    private float defaultLifeSpan = 3f;

    private float nextRespawn;
    public int targetStartingHealth = 4;

    public int killCount = 0;
    public int multiplierCount = 1;

    private bool b_timerExpired;

    // AudioRefs
    [Header("Audio BISHHHHH")]
    [SerializeField] private AudioSource uiAudioSource;
    
    [SerializeField] private AudioSource otterAudioSource;
    [SerializeField] private AudioSource countDownClockAudioSource;


    [SerializeField] private AudioAsset ui_HitSFX;
    [SerializeField] private AudioAsset ui_ButtonSFX;
    [SerializeField] private AudioAsset otterHitSFX;
    [SerializeField] private AudioAsset otterKilledSFX;
    [SerializeField] private AudioAsset otterMissedClickSFX;
    [SerializeField] private AudioAsset levelWinSFX;
    [SerializeField] private AudioAsset levelLossSFX;
    [SerializeField] private AudioAsset cardPlayedSFX;
    [SerializeField] private AudioAsset cardExpiredSFX;
    [SerializeField] private AudioAsset fiveSecondTimerSFX;

    [SerializeField] private GameObject ingameUI;
    public int points = 0;

    [SerializeField] private int getCardMultiplierRequierd = 3;
    

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
        gamePaused += PauseGame;
        gameUnPaused += UnPauseGame;
        levelComplete += OnLevelComplete;
        //levelComplete -= CardExpiredSFX;
        levelComplete += StopCountdownSFX;
        levelFailed += StopCountdownSFX;
        //levelFailed -= CardExpiredSFX;

        CardManager.Instance.cardPlayed += PlayCardPlayedSFX;
        ActiveCard.Instance.cardExpired += CardExpiredSFX;
        CountdownClock.Instance.fiveSecondCountDown += FiveSecondTimer;

    }

    public void StopCountdownSFX()
    {
        
        countDownClockAudioSource.Stop();
    }
    public void FiveSecondTimer()
    {
        fiveSecondTimerSFX.PlayAudioClip(countDownClockAudioSource);
    }

    private void PlayCardPlayedSFX()
    {
        cardPlayedSFX.PlayAudioClip(uiAudioSource);
    }

    private void CardExpiredSFX()
    {
        if (!b_timerExpired)
        { 
            cardExpiredSFX.PlayAudioClip(uiAudioSource); 
        }
        
        
    }
    public void ContinueToNextLevel()
    {
        //SetupLevelData();
        deactivateLevelComplete?.Invoke();
        //SetNextSpawnTime(respawnRate);
        StartGameWithDelay();
        ingameUI.SetActive(true);
        countdownStart?.Invoke();
    }

    public void RestartLevel()
    {
        //SetupLevelData();
        deactivateLevelComplete?.Invoke();
        //SetNextSpawnTime(respawnRate);
        StartGameWithDelay();
        levelNumber--;
        ingameUI.SetActive(true);
        countdownStart?.Invoke();


    }

    public void StartGameWithDelay()
    {
        StartCoroutine(StartGameDelay(gameIntroSoundLenth));
    }
    public void OnPlayGame()
    {

    }
    private void OnLevelComplete()
    {
        gamestate = Gamestate.LevelComplete;
        b_timerExpired = true;
        levelWinSFX.PlayAudioClip(uiAudioSource);
        
    }

    public void UIButtonSFX()
    {
        ui_ButtonSFX.PlayAudioClip(uiAudioSource);
    }
    public void OtterHit()
    {
        // As it is one hit kills, this is just for audio

        ui_HitSFX.PlayAudioClip(uiAudioSource);
        otterHitSFX.PlayAudioClip(otterAudioSource);
    }
    public void OtterKilled(DamageMultipliers damageMultiplier)
    {
        killCount++;

        if(otterKilledSFX != null)
        {
            otterKilledSFX.PlayAudioClip(otterAudioSource);
        }
        else
        {
            Debug.Log("No otterKilled SFX on gamemanager");
        }
        int points = 0;

        switch (damageMultiplier)
        {
            case DamageMultipliers.hundred:
                points = 100;
                break;
            case DamageMultipliers.hundredfifty:
                points = 150;
                break;
            case DamageMultipliers.twohundred:
                points = 200;
                break;
        }

        UpdatePoints(points);

        if(HasWon())
        {
            levelComplete?.Invoke();
        }

        otterKilled?.Invoke();
    }
    private void UpdatePoints(int points)
    {
        this.points += (points * multiplierCount);
        pointsChanged?.Invoke();
    }
    private bool HasWon()
    {
        if(points >= currentLevelData.points)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void UnPauseGame()
    {
        gamestate = Gamestate.Running;
        Time.timeScale = 1;
    }
    private void PauseGame()
    {
        gamestate = Gamestate.Paused;
        Time.timeScale = 0;
    }
    public void StartGameEffect(GameEffect gameEffect, bool isPermenent)
    {
        Debug.Log("START GAME EFFECT CALLED - NO LOGIC DADDED. GameEffect = " + gameEffect.ToString());
    }
    private void InitialiseDefaultValues(LevelDataSO levelDataSO)
    {
        killCount = 0;
        defaultLifeSpan = levelDataSO.lifeSpan;
        lifeSpan = defaultLifeSpan;
        defaultSpeed = levelDataSO.respawnRate;
        respawnRate = defaultSpeed;
        points = 0;
        multiplierCount = 1;

    }
    public void InitialiseCardEffects()
    {
        respawnRate = defaultSpeed;
        lifeSpan = defaultLifeSpan;
    }
    public void DefaultSpeed()
    {
        respawnRate = defaultSpeed;
    }
    public bool SetNewRespawnRate(float newSpeed, bool isPermanent)
    {
        respawnRate = newSpeed;
        SetNextSpawnTime(respawnRate);

        if(isPermanent)
        {
            defaultSpeed = nextRespawn;
        }

        return true;
    }
    
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            countdownStart?.Invoke();
            StartCoroutine(StartGameDelay(gameIntroSoundLenth));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            switch (gamestate)
            {
                case Gamestate.Running:
                    gamePaused?.Invoke();
                    break;
                case Gamestate.Paused:
                    gameUnPaused?.Invoke();
                    break;
                case Gamestate.Menu:
                    break;
            }
        }

        Debug.Log(respawnRate);
        if(gamestate == Gamestate.Running)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                CardManager.Instance.PlayCard(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CardManager.Instance.PlayCard(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CardManager.Instance.PlayCard(2);
            }
        }

        switch (gamestate)
        {
            case Gamestate.Running:

                if (Time.time > levelEndTime)
                {
                    b_timerExpired = true;
                    // FAILED
                    levelFailed?.Invoke();
                    levelLossSFX.PlayAudioClip(uiAudioSource);
                    ActiveCard.Instance.cardExpired -= CardExpiredSFX;
                    gamestate = Gamestate.LevelComplete;
                }

                if (HasWon())
                {
                    b_timerExpired = true;
                    levelComplete?.Invoke();
                }

                if (Time.time > nextRespawn)
                {
                    GridPoint gridPoint = GameGrid.Instance.GetRandomGridPoint();

                    if (gridPoint != null)
                    {
                        if (gridPoint.isActive == false && gridPoint.isCoolingDown == false)
                        {
                            OtterSO otterSO;

                            int i = currentLevelData.chanceOtterA + currentLevelData.chanceOtterB + currentLevelData.chanceOtterC;

                            int random = UnityEngine.Random.Range(1, i + 1);

                            if(random <= currentLevelData.chanceOtterA)
                            {
                                otterSO = otterASO;
                            }
                            else if (random <= currentLevelData.chanceOtterA + currentLevelData.chanceOtterB)
                            {
                                otterSO = otterBSO;

                            }
                            else
                            {
                                otterSO = otterCSO;
                            }

                            gridPoint.Activate(otterSO, lifeSpan);
                        }
                    }

                    SetNextSpawnTime(respawnRate);
                }


                break;
            case Gamestate.Paused:
                break;
            case Gamestate.Menu:
                break;
            case Gamestate.LevelComplete:
                break;
        }
    }
    public void StartGame()
    {
        gamestate = Gamestate.Running;
        SetupLevelData();
        SetNextSpawnTime(respawnRate);
        startLevel?.Invoke();
        ActiveCard.Instance.cardExpired += CardExpiredSFX;
        b_timerExpired = false;
    }
    private void SetupLevelData()
    {
        levelNumber++;

        for (int i = 0; i < levelDatas.Count; i++)
        {
            if(levelDatas[i].levelNumber == levelNumber)
            {
                currentLevelData = levelDatas[i];
                pointsChanged?.Invoke();
            }
        }

        levelStartTime = Time.time;
        levelEndTime = levelStartTime + currentLevelData.levelTimeLimit;
        InitialiseDefaultValues(currentLevelData);

    }
    public void SetNextSpawnTime(float delay)
    {
        nextRespawn = Time.time + delay;
    }
    IEnumerator StartGameDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartGame();
    }

}

public enum Gamestate
{
    Running,
    Paused,
    Menu,
    LevelComplete,
}

public enum GameEffect
{
    None,
    Blurred
}