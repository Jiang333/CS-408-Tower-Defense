﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour {

    public GameObject[] Tower;
    private int selection = 0;

    public GameObject spawnZonesRoot;
    public GameObject[] towerSpawnZones;

    public float downTime, upTime, pressTime = 0;
    public float countDown = 1f;
    public bool doing = false;

    void Start()
    {
        Debug.Log(towerSpawnZones.Length + "haha");
    }

    // Update is called once per frame
    void Update() {
        if (towerSpawnZones.Length == 0)
            towerSpawnZones = GameObject.FindGameObjectsWithTag("TowerSpawnZone");
        
        // Build
        PlayerStatus progress = this.GetComponent<PlayerStatus>();
        if (!LevelManager.Instance.canBuild)
        {
            return;
        }
        

        if (LevelManager.Instance.GetGold() < Tower[selection].GetComponent<TowerStatus>().cost[0])
        {
            return;
        }

        if (Input.GetKeyDown("space") == true) {
            doing = true;
            downTime = Time.time;
        }

        if (Input.GetKey("space")) {
            if (downTime + countDown <= Time.time && doing == true && progress != null)
            {
                progress.DoAction(25);
                downTime = Time.time;
            }

        }
        if (Input.GetKeyUp("space") == true) {
            doing = false;
            downTime = 0;
            progress.ResetProgress();
        }

        float shortestDistance = Mathf.Infinity;
        GameObject bestSpot = null;


        if (progress.currentProgress >= 100 && doing == true) {
            doing = false;
            progress.currentProgress = 0;
            Vector3 spawnPosition = transform.position + (transform.forward * 1.5f);

            foreach (GameObject towerZone in towerSpawnZones)
            {
          
                if (!towerZone.GetComponent<ZoneBuildable>().zoneTaken)
                {
                    float distanceToZone = Vector3.Distance(spawnPosition, towerZone.transform.position);
                    if (distanceToZone < shortestDistance)
                    {
                        shortestDistance = distanceToZone;
                        bestSpot = towerZone;                       
                    }
                }
                
            }
            spawnPosition.x = bestSpot.transform.position.x;
            spawnPosition.z = bestSpot.transform.position.z;
            spawnPosition.y = 1.4f;

            LevelManager.Instance.SetGold(-Tower[selection].GetComponent<TowerStatus>().cost[0]);

            GameObject newTower = Instantiate(Tower[selection], spawnPosition, new Quaternion(0, 180, 0, 0));
            bestSpot.GetComponent<ZoneBuildable>().zoneTaken = true;
        }


    }

}
