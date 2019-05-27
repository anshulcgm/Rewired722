﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleave : MonoBehaviour
{
    public float cooldown;

    private GameObject camera;

    private AnimationController anim;
    DateTime start;


    Rigidbody rigidbody;
    Stats stats;
    TargetCenterScreen tcs;

    private bool buffActive;
    private bool cast;

    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<AnimationController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        rigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<Stats>();
        tcs = GetComponent<TargetCenterScreen>();
        soundManager = GetComponent<SoundManager>();

        cooldown = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && cooldown == 0)      //place key, any key can be pressed.
        {
            cast = false; //ability not yet cast
            start = DateTime.Now;
            Debug.Log("reee");
            
            

            //put any setup code here, before the ability is actually cast



        }

        if ((DateTime.Now - start).TotalSeconds < 1 && !cast)
        {
            anim.StartOverlayAnim("Circle_strike_2", 0.5f, 1f); //this tells the animator to play the right animation
            soundManager.playOneShot("Cleave");

            //temporary damage dealer
            Collider[] hits = Physics.OverlapSphere(transform.position, 10);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.tag == "Enemy")
                {
                    stats.dealDamage(hit.gameObject, 600);

                }
            }


            cooldown = 240;                          //placeholder time, divide by 60 for cooldown in seconds
            cast = true;

        }


        if (cooldown > 0) //counts down for the cooldown
        {
            cooldown--;
        }
    }
}
