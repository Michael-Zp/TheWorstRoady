using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public AttackType AttackType;
    public float respawnTime;
    public GameObject Blood;

    private float tmpRespawnTime;
    private bool dead = false;
    private Color _originalColor;

    private void Start()
    {
        _originalColor = GetComponent<Renderer>().material.color;
    }

    public void Die()
    {
        this.tmpRespawnTime = this.respawnTime;
        this.dead = true;
        GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.3f);
        Blood.SetActive(true);
        StartCoroutine(StopBlood());
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
        GetComponent<Renderer>().material.color = _originalColor;
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

    private IEnumerator StopBlood()
    {
        yield return new WaitForSeconds(0.4f);

        Blood.SetActive(false);
    }
}
