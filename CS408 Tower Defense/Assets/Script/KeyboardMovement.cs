﻿using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    public float mSpeed = 10;
    

    void Update()
    {
        bool doing = this.GetComponent<CreateTower>().doing;
        if (!doing) { 
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * mSpeed;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * mSpeed;
        
            Vector3 position = this.transform.position;
            position.x += x;
            position.z += z;
            this.transform.position = position;
            this.GetComponentInChildren<Animation>().Play("Walk");
        }
    }

}