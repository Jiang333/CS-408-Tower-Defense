﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : MonoBehaviour {

    protected float cooldown;
    protected float lastCast;

    public virtual void Cast()
    {
        if (!Requirement())
        {
            return;
        }

        Action();
        lastCast = Time.time;
    }

    public virtual bool Requirement()
    {
        if (Time.time - lastCast <= cooldown)
        {
            return false;
        }
        return true;
    }
    
    public virtual void Action()
    {
        Debug.Log("Casting " + this.ToString());
    }
}
