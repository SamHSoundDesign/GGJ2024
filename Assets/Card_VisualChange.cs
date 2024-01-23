using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_VisualChange : Card
{
    public GameEffect gameEffect = GameEffect.None;
    public override void ActivateCard()
    {
        GameManager.Instance.StartGameEffect(gameEffect, isPermanent);

        base.ActivateCard();
    }


    public override void DeativateEffects()
    {
        base.ActivateCard();
    }
}
