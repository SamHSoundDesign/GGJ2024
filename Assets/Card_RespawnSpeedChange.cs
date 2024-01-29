using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_RespawnSpeedChange : Card
{
    public float speedIncreaseAmount = 1.5f;

    private void Start()
    {
        ActivateCard();
    }

    public override void ActivateCard()
    {
        float newSpeed = GameManager.Instance.respawnRate * speedIncreaseAmount;
        GameManager.Instance.SetNewRespawnRate(newSpeed, isPermanent);

        base.ActivateCard();
    }

    public override void DeativateEffects()
    {
        GameManager.Instance.DefaultSpeed();
        base.DeativateEffects();
    }
}
