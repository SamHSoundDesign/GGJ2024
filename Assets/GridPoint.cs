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

    public void Activate(GameObject targetPrefab, float duration)
    {
        isActive = true;
        SpawnTarget(targetPrefab);
        SetDeactivateTimer(duration);
    }
    private void SetDeactivateTimer(float duration)
    {
        stopTime = Time.time + duration;
    }
    private void Update()
    {
        if (isActive == false && isCoolingDown == false)
            return;

        if(Time.time > stopTime)
        {
            Deactivate();
        }

        if(Time.time > cooldownTime && isCoolingDown)
        {
            isCoolingDown = false;
        }
    }
    private void SpawnTarget(GameObject targetPrefab)
    {
        if(target != null)
        {
            DestroyTarget();
        }

        GameObject newTarget = Instantiate(targetPrefab, transform.position, transform.rotation, transform);
        target = newTarget;
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
        Vector3 targetPos = target.transform.position;
        Instantiate(GameManager.Instance.otterDeath_particle, targetPos, Quaternion.identity, transform);
        GameManager.Instance.OtterKilled();
        Deactivate();
    }
}
