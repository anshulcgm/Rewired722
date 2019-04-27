﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//IMPORTANT MESSAGE: All entities will have a three Stats, baseStats, multiplierStats, flatStats.
//Base stats will only go up as one levels up
//The stat multipliers and flat are for changing stats in case one gets debuffed
//A 30% damage debuff would be enemy.multiplierStats.attack = 0.7
//Flat adjuster is for arbitrary flat value buffs, for example player.flatState.speed - 5.
//stats are calculated within each entity with something like baseStats * multipler + flat or maybe (base + flat) * multiplier


//For the following stat arrays, [0] is the base, [1] is the multiplier, and [2] is the flat
public enum statModifier { Base, Multiplier, Flat };
public enum stats { HealthRegen, ManaRegen, Speed, Attack, AttackSpeed, Defense };

public class Stats : MonoBehaviour
{
    public int regenTimer = 0;
    //go with this for now, we may need maxHealth as a separate variable

    public float maxHealth;
    public float health;
    public float maxMana;
    public float mana;


    //Might be easier to use two enums and have a 2D array



    public float[,] allStats = new float[6, 3]; //first column is base stats, second multiplier, third flat. Each 

    /*
    public float[] healthRegen = new float[2];

    public float[] manaRegen= new float[2];

    public float[] speed = new float[2];

    public float[] attack = new float[2];

    public float[] attackSpead = new float[2];

    public float[] defense = new float[2];
    */

    /*
    public int[] statusAilments = new int[x] where x is the number of ailments
    //it's a int array because this also tracks duration
    
    public enum statusAilments {a, b, c, d, e, ....

    update function counts down, see below

    */

    public float getFinal(int statType) //
    {
        return allStats[statType, (int)statModifier.Base] * allStats[statType, (int)statModifier.Multiplier] + allStats[statType, (int)statModifier.Flat];
        // Stat[0] * Stat[1] + Stat[2] same thing
    }

    public void Start()
    {
        //file reading, assign the numbers to the variables

    }

    public void Update()
    {
        //Regen should be every half second
        //Health Regen


        //Mana Regen
        regenTimer++;
        if (regenTimer == 30) //half second
        {
            //the regen stuff
            regenTimer = 0;
        }


        if (health == 0)
        {
            //die
        }

    }
    ///////////////
    //INTERACTIONS

    public void takeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth); //might be moved to update
    }

    public void heal(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    //postive and negative may be separated
    public void addStatus(int status, int duration)
    {
        //statusAilment[status] += duration;
    }

}
