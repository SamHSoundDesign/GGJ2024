using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamageIndicator : MonoBehaviour
{
    public float zRot;
    public float yRot;

    public ParticleSystem ps;

    private void Start()
    {


    }

    private void OnEnable()
    {
        ps.Play();
        //Vector3 michaelVec = new Vector3(0, Random.Range(yRot - 30, yRot + 30), Random.Range(zRot - 30, zRot + 30));

        //ParticleSystem.RotationOverLifetimeModule mod = ps.rotationOverLifetime;

        //mod.y = michaelVec.y;
        //mod.z = michaelVec.z;

        //ps.Play();
    }
}
