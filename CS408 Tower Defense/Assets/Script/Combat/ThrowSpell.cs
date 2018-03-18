﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpell : BaseSpell {

    private float hitLength;
    private Transform hitOrigin;
    private LayerMask targetMask;
    private int level;
    public GameObject bulletPrefab;
    public Transform target;


    public ThrowSpell()
    {
        cooldown = 13f - level;
        hitLength = 8.0f;
    }

    void Start () {
        bulletPrefab = GameObject.Find("RockProjectile");
        Debug.Log(bulletPrefab.name);
        level = 1;
        lastCast = Time.time - cooldown;
        hitOrigin = this.transform;
        targetMask = LayerMask.GetMask("Enemy");
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float lowestHealth = 9999999999999;
        GameObject bestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            float enemyHealth = enemy.GetComponent<EnemyStatus>().currentHealth;
            if (enemyHealth < lowestHealth)
            {
                lowestHealth = enemyHealth;
                bestEnemy = enemy;
            }

        }

        if (bestEnemy != null)
        {
            target = bestEnemy.transform;
        }
    }

    public override void Action()
    {
        cooldown = 13f - level;
        UpdateTarget();
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate(
            bulletPrefab,
            target.position + new Vector3(0,7,0), target.rotation);

            // Add velocity to the bullet
             bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * -45;

            Destroy(bullet, 5.0f);

    }
}
