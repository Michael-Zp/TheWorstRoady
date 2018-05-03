using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AttackType AttackType;
    public float respawnTIme;

    private float tmpRespawnTime;
    private bool dead = false;


    public void Die()
    {
        this.tmpRespawnTime = this.respawnTIme;
        this.dead = true;
        GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.3f);
    }

    private void Update()
    {
        if (this.Dead)
        {
            tmpRespawnTime -= Time.deltaTime;
            if (tmpRespawnTime <= 0.0f)
            {
                respawn();
            }
        }
    }

    private void respawn()
    {
        this.dead = false;
        GetComponent<Renderer>().material.color = new Color(1, 0, 1, 1);
    }
    public bool Dead
    {
        get
        {
            return dead;
        }

        set
        {
            dead = value;
        }
    }
}
