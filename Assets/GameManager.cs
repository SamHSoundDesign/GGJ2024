using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject targetPrefab;
    private Gamestate gamestate = Gamestate.Menu;
    private GameEffect gameEffect = GameEffect.None;

    public event Action startLevel;
    public event Action startGame;
    public event Action gamePaused;
    public event Action gameUnPaused;
    public event Action levelComplete;
    public event Action deactivateLevelComplete;
    public event Action pointsChanged;
    public event Action levelFailed;

    public int levelNumber = 0;
    public List<LevelDataSO> levelDatas;
    public LevelDataSO currentLevelData;
    public event Action otterHit;
    public event Action otterKilled;

    public float levelStartTime;
    public float levelEndTime;

    public int damagePerClick = 2;

    public float gameIntroSoundLenth = 3f;

    //Spawns
    public GameObject otterDeath_particle;

    public float respawnRate;
    public float lifeSpan = 0.5f;
    private float defaultSpeed;
    private float defaultLifeSpan = 3f;

    private float nextRespawn;
    public int targetStartingHealth = 4;

    public int killCount = 0;
    public int multiplierCount = 1;

    // AudioRefs
    [Header("Audio BISHHHHH")]
    [SerializeField] private AudioSource uiAudioSource;
    
    [SerializeField] private AudioSource otterAudioSource;

    [SerializeField] private AudioAsset ui_HitSFX;
    [SerializeField] private AudioAsset otterHitSFX;
    [SerializeField] private AudioAsset otterKilledSFX;
    [SerializeField] private AudioAsset otterMissedClickSFX;
    [SerializeField] private AudioAsset levelWinSFX;
    [SerializeField] private AudioAsset levelLossSFX;
    private int points = 0;
    [SerializeField] private int hitPointValue = 50;
    [SerializeField] private int killPointValue = 100;

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

        StartCoroutine(StartGameDelay(gameIntroSoundLenth));
    }

    public void ContinueToNextLevel()
    {
        //SetupLevelData();
        deactivateLevelComplete?.Invoke();
        //SetNextSpawnTime(respawnRate);
        StartCoroutine(StartGameDelay(gameIntroSoundLenth));


    }
    private void OnLevelComplete()
    {
        gamestate = Gamestate.LevelComplete;

        
        
    }
    public void OtterHit()
    {
        ui_HitSFX.PlayAudioClip(uiAudioSource);
        otterHitSFX.PlayAudioClip(otterAudioSource);

        UpdatePoints(1);

    }
    public void OtterKilled()
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

        UpdatePoints(2);

        if(HasWon())
        {
            levelComplete?.Invoke();
        }

        otterKilled?.Invoke();
    }
    private void UpdatePoints(int hitCount)
    {
        if(hitCount == 1)
        {
            points += hitPointValue * multiplierCount;
        }
        else if(hitCount == 2)
        {
            points += killPointValue * multiplierCount;
        }

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
    public void DefaultSpeed()
    {
        respawnRate = defaultSpeed;
    }
    public bool SetNewRespawnRate(float newSpeed, bool isPermanent)
    {
        if(respawnRate > newSpeed && isPermanent == false)
        {
            return false;
        }

        float timeUntilScheduledSpawn = nextRespawn - Time.time;

        float delta = newSpeed - respawnRate;
        timeUntilScheduledSpawn -= delta;
        respawnRate = newSpeed;

        SetNextSpawnTime(timeUntilScheduledSpawn);

        if(isPermanent)
        {
            defaultSpeed = nextRespawn;
        }

        return true;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
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

        switch (gamestate)
        {
            case Gamestate.Running:

                if (Time.time > levelEndTime)
                {
                    // FAILED
                    levelFailed?.Invoke();
                }

                if (HasWon())
                {
                    levelComplete?.Invoke();
                }

                if (Time.time > nextRespawn)
                {
                    GridPoint gridPoint = GameGrid.Instance.GetRandomGridPoint();

                    if (gridPoint != null)
                    {
                        if (gridPoint.isActive == false && gridPoint.isCoolingDown == false)
                        {
                            gridPoint.Activate(targetPrefab, lifeSpan);
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