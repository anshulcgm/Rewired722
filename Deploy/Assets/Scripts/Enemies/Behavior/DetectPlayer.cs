﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Toma Itagaki
public class DetectPlayer : MonoBehaviour {

    public GameObject player;

    private Vector3 playerPosition;
    private Vector3 enemyPosition;
     
    private Vector3 direction;
    private float distance;

    public void Start() {
        playerPosition = player.transform.position;
        enemyPosition = transform.position;
    }

    public void findDistance()
    {
        this.distance = Vector3.Distance(playerPosition, enemyPosition);
        this.direction = (enemyPosition - playerPosition).normalized;

        if (distance < 5)
        {
            move(direction);
        }
    }

    public void move(Vector3 direction)
    {
        //move funct
    }
}
