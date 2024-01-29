using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHParticle : MonoBehaviour
{
    private ParticleSystem ps;

    public List<Sprite> sprites;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private Sprite GetSprite()
    {
        int index = UnityEngine.Random.Range(0, sprites.Count);

        return sprites[index];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ps.textureSheetAnimation.SetSprite(0, GetSprite());
        }
    }
}
