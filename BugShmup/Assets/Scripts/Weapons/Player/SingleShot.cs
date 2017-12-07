using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : Weapon
{
    public override void Awake()
    {
        base.Awake();
        audioSource.clip = AudioManager.Audios.PlayerShoot;
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (audioSource.clip == null)
        {
            audioSource.clip = AudioManager.Audios.PlayerShoot;
        }

        base.Update();
    }
}