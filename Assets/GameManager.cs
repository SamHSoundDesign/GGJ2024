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
    public event Action gamePaused;
    public event Action levelComplete;
    public event Action levelChanged;

    public int levelNumber = 0;
    public List<LevelDataSO> levelDatas;
    public LevelDataSO currentLevelData;

    //Spawns

    public float respawnRate;
    public float lifeSpan = 0.5f;
    private float defaultSpeed;
    private float defaultLifeSpan = 3f;

    private float nextRespawn;

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
        InitialiseDefaultValues();
    }

    public void StartGameEffect(GameEffect gameEffect, bool isPermenent)
    {
        Debug.Log("START GAME EFFECT CALLED - NO LOGIC DADDED. GameEffect = " + gameEffect.ToString());
    }

    private void InitialiseDefaultValues()
    {
        defaultLifeSpan = lifeSpan;
        defaultSpeed = respawnRate;
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
            if(gamestate == Gamestate.Menu)
            {
                StartGame();
            }
            else
            {
                gamestate = Gamestate.Paused;
                gamePaused?.Invoke();
            }
        }

        switch (gamestate)
        {
            case Gamestate.Running:
                if (Time.time > nextRespawn)
                {
                    GridPoint gridPoint = GameGrid.Instance.GetRandomGridPoint();
                    gridPoint.Activate(targetPrefab, lifeSpan);
                    SetNextSpawnTime(respawnRate);
                }
                break;
            case Gamestate.Paused:
                break;
            case Gamestate.Menu:
                break;
        }
    }

    public void StartGame()
    {
        SetNextSpawnTime(respawnRate);
        gamestate = Gamestate.Running;
        SetLevelNumber();
    }

    private void SetLevelNumber()
    {
        levelNumber++;

        for (int i = 0; i < levelDatas.Count; i++)
        {
            if(levelDatas[i].levelNumber == levelNumber)
            {
                currentLevelData = levelDatas[i];
                levelChanged?.Invoke();

            }
        }

        startLevel?.Invoke();
    }

    public void SetNextSpawnTime(float delay)
    {
        nextRespawn = Time.time + delay;
    }
}

    public enum Gamestate
{
    Running,
    Paused,
    Menu
}

public enum GameEffect
{
    None,
    Blurred
}