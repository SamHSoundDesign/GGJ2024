using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health;

    public bool isInvicible = false;

    GridPoint gp;

    private void Start()
    {
        health = GameManager.Instance.targetStartingHealth;
    }
    private void OnMouseDown()
    {
        if(isInvicible == false)
        { 
            DoDamange(GameManager.Instance.damagePerClick);
        }
    }

    public void DoDamange(int amount)
    {
        health -= amount;
        GameManager.Instance.OtterHit();

        if(health <= 0)
        {
            if(gp == null)
            {
                gp = GetComponentInParent<GridPoint>();
            }
            
            gp.Kill();
        }

    }


}
