﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Toma Itagaki
public class ShlyEnemy : Enemy {

    private int aggregateNum; //number of shlies that are aggregate
    private float bullChargeSpeed; //shly speed towards players
	private float speedDebuff; //the proportion of AOE speed debuff when player is under "sprain" effect
    private float pelletDamage;
    private float bullChargeDamage;

    public int minSprainShlyCount = 12;
    public float shlySprainRange = 10;
    private bool sprainActive;
    public static List<ShlyEnemy> shlyList = new List<ShlyEnemy>();

    public ShlyEnemy(float hp, int ms, float defense, int aNum, float bCS, float sD, GameObject rf, float pelletDamage, float bullChargeDamage) : base(hp, ms, defense, rf)
    {
        aggregateNum = aNum; //aggregate shlies come as 12 initially
        bullChargeSpeed = bCS;
        speedDebuff = sD;
        this.pelletDamage = pelletDamage;
        this.bullChargeDamage = bullChargeDamage;
    }



    public void pelletDrop(float radius) //basic attack where shly drops pellets
    {
        Vector3 playerPos;

        //checks whether objects within radius x are tagged players
        Collider[] hitColliders = Physics.OverlapSphere(referenceObject.transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Player"))
            {
                playerPos = hitColliders[i].gameObject.transform.position;
                /*
                Enter in unity logic of dropping pellet                
                */

            }
            i++;
        }
    }


    public void sprain(float x) //enemies within a radius of x will slow down
    {
        int i = 0;
        int shlyCount = 0;
        foreach(ShlyEnemy shly in shlyList)
        {
            if( referenceObject.transform.position.x - shly.referenceObject.transform.position.x < shlySprainRange &&
                referenceObject.transform.position.y - shly.referenceObject.transform.position.y < shlySprainRange &&
                referenceObject.transform.position.z - shly.referenceObject.transform.position.z < shlySprainRange)
            {
                shlyCount++;
            }
        }

        if (shlyCount >= minSprainShlyCount)
        {
            sprainActive = true;
        }
        else
        {
            sprainActive = false;
        }

        if (sprainActive)
        {
            Collider[] hitColliders = Physics.OverlapSphere(referenceObject.transform.position, shlySprainRange);
            int j = 0;
            if (hitColliders[j].CompareTag("Player"))
            {
                //initiate sprain behavior 
                //Include player debuff
                //hitColliders[i].gameObject.GetComponent<StatManager>().playerMultiplySpeed(speedDebuff);
            }
        }

    }


    public void bullCharge(float radius) //knockback attack when aggregated
    {
        Vector3 playerPos;

        if(aggregateNum >= 6) //must be aggregate shly (one more more)
        {
            //checks whether objects within radius x are tagged players
            //if so, shly will charge at the player and attack
            Collider[] hitColliders = Physics.OverlapSphere(referenceObject.transform.position, radius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Player"))
                {
                    playerPos = hitColliders[i].gameObject.transform.position;
                    /*
                    UNity Logic?    
                    
                    */
                    float speed = 1; //arbitrary number
                    float step = speed * Time.deltaTime;

                    referenceObject.transform.position = Vector3.MoveTowards(referenceObject.transform.position, playerPos, step);
                    //above logic will move the aggregate shly towards the player

                    //damage dealt
                    //Include damage dealt for player
                    //hitColliders[i].gameObject.GetComponent<StatManager>().health - gameObject.GetComponent<StatManager>().attack * aggregateNum;
                    //damage dealt by bull charge attack will be the aggregate number times the shlies attack

                    /*
                    Unity logic for player knockback 
                    */
                }
                i++;
            }
        }
    }

    //mutators
    public void adjustDamage(float multiplier)
    {
        pelletDamage *= multiplier;
        bullChargeDamage *= multiplier;
    }

    //public accessors
	public int getAggregateNum() {
		return aggregateNum;
	}

	public float getBullChargeSpeed() {
		return bullChargeSpeed;
	}

	public float getSpeedDebuff() {
		return speedDebuff;
	}

    public float getPelletDamage()
    {
        return pelletDamage;
    }
    
    public float getBullChargeDamage()
    {
        return bullChargeDamage;
    }
}
