﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    Vector3 direction;
    [SerializeField] float moveSpeed;

    // Use this for initialization
    protected override void Start()
    {
        direction = new Vector3(0,0,1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
