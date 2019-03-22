﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Anshul Ahluwalia 
public class RandomEnemySpawn: MonoBehaviour {

    private GameObject planet;
    public GameObject enemyToInstanstiate;
    public GameObject testPlayer;

    public int enemiesPerSpawn = 5;
    public int numberOfSwarms = 1;
    private Vector3 planetCenter;
    private float planetRadius;
   

    private List<Vector3> enemyInstantiationPoints; 
    public void Start()
    {
        InstantiationNearPlayer();
    }

    public Vector3 GetRandomInstantiationPointOnSphere()
    {
        return (Random.onUnitSphere * planetRadius + planetCenter);
    }
    public void randomInstantiation()
    {
        planet = GameObject.FindGameObjectWithTag("planet");
        enemyInstantiationPoints = new List<Vector3>();
        //Debug.Log("Planet transform is at " + planet.transform.position);
        planetCenter = planet.transform.position;
        planetRadius = (planet.transform.localScale.x) / 2; //Can be any variable as all axes will have same local transform
        for (int i = 0; i < numberOfSwarms; i++)
        {
            Vector3 instantiationPoint = GetRandomInstantiationPointOnSphere();
            while (enemyInstantiationPoints.Contains(instantiationPoint)) //Checks to make sure point isn't already in the list
            {
                instantiationPoint = GetRandomInstantiationPointOnSphere();
            }
            enemyInstantiationPoints.Add(instantiationPoint);
        }
        ObjectUpdate o = new ObjectUpdate();
        foreach (Vector3 point in enemyInstantiationPoints)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                Vector3 randomInstanPoint = Random.insideUnitSphere * 30.0f + point; //Instantiates enemies within 30 meter radius of original instantiation Point 
                GameObject enemy = (GameObject)Instantiate(enemyToInstanstiate, randomInstanPoint, Quaternion.identity);
                Enemy e = new Enemy(EnemyType.FlyingKamikaze, 50, 10, enemy);
                Enemy.enemyList.Add(e);
                InstantiationRequest instanRequest = new InstantiationRequest("KamikaziBird", randomInstanPoint, Quaternion.identity);
                o.AddInstantiationRequest(instanRequest);

            }
        }
        ObjectHandler.Update(o, this.gameObject);
    }
    public void InstantiationNearPlayer()
    {
        enemyInstantiationPoints = new List<Vector3>();
        for(int i = 0; i < numberOfSwarms; i++)
        {
            Vector3 instantiationPoint = testPlayer.transform.position + Random.insideUnitSphere * 100f + new Vector3(500f, 200f, 500f);
            enemyInstantiationPoints.Add(instantiationPoint);
        }
        foreach(Vector3 point in enemyInstantiationPoints)
        {
            for(int i = 0; i < enemiesPerSpawn; i++)
            {
                Vector3 randomPoint = Random.insideUnitSphere * 15.0f + point;
                GameObject enemy = (GameObject)Instantiate(enemyToInstanstiate, randomPoint, Quaternion.identity);
                enemy.transform.LookAt(testPlayer.transform);
            }
        }
    }
}
