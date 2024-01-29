using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{
    public bool isActive = false;
    public bool isCoolingDown = false;

    public GameObject target;
    private float stopTime;
    private float cooldownDuration = 1f;
    private float cooldownTime;

    private DamageMultipliers damageMultiplier = DamageMultipliers.hundred;

    public void Activate(OtterSO otterSO, float duration)
    {
        isActive = true;
        SpawnTarget(otterSO);

        float deactivateDuration = duration * otterSO.lifeTimeMultiplier;
        SetDeactivateTimer(deactivateDuration);
    }
    private void SetDeactivateTimer(float duration)
    {
        stopTime = Time.time + duration;
    }
    private void Update()
    {
        if (isActive == false && isCoolingDown == false)
            return;

        if(Time.time > stopTime && isActive)
        {
            Deactivate();
        }

        if(Time.time > cooldownTime && isCoolingDown)
        {
            isCoolingDown = false;
        }
    }
    private void SpawnTarget(OtterSO otterSO)
    {
        if(target != null)
        {
            DestroyTarget();
        }

        GameObject newTargetGO = Instantiate(otterSO.prefab, transform.position, transform.rotation, transform);
        damageMultiplier = otterSO.damageMultiplier;
        target = newTargetGO;
    }
    private void DestroyTarget()
    {
        Destroy(target);
        target = null;
    }
    public void Deactivate()
    {
        isActive = false;
        DestroyTarget();
        isCoolingDown = true;
        cooldownTime = Time.time + cooldownDuration;
    }
    public void Kill()
    {
        GameObject prefab;
        
        switch (damageMultiplier)
        {
            case DamageMultipliers.hundred:
                prefab = GameManager.Instance.hundredParticle;
                break;
            case DamageMultipliers.hundredfifty:
                prefab = GameManager.Instance.hundredFiftyParticle;
                break;
            case DamageMultipliers.twohundred:
                prefab = GameManager.Instance.twoHundredParticle;   
                break;
            default:
                prefab = GameManager.Instance.hundredParticle;
                break;
        }

        Vector3 targetPos = target.transform.position;
        targetPos.z -= 0.1f;

        Instantiate(prefab, targetPos, Quaternion.identity, transform);
        
        targetPos.z -= 0.1f;
        Instantiate(GameManager.Instance.otterDeath_particle, targetPos, Quaternion.identity, transform);
        
        Deactivate();
        GameManager.Instance.OtterKilled(damageMultiplier);
    }

    public void DeactivateLevelEnd()
    {
        DestroyTarget();
        isActive = false;
        isCoolingDown = false;
    }
}
