using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetection : MonoBehaviour
{
    public int mode = 0;
    public GameObject target;
    public float damage = 20f;
    public float speed = 0.1f;
    public float stopDistance = 1.0f;
    public float hp = 0.5f;
    private PlayerStats playerStatsManager;
    public ParticleSystem hitEffect;
    private bool hasDamagedPlayer = false;

    // This script sets what happens when object reaches the target
    // Mode 0: on impact destroy and decrease player health
    // Mode 1: after a certain distance away, stop moving and start shooting

    void Start()
    {
        playerStatsManager = GameObject.FindGameObjectWithTag("PlayerStatsManager").GetComponent<PlayerStats>();
        //target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = 3000;
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            distance = direction.magnitude;
        }
        else
        {
            Vector3 direction = Camera.main.gameObject.transform.position - transform.position;
            distance = direction.magnitude;
        }

        if (distance <= stopDistance*1.2)
        {
            if (hitEffect != null) hitEffect.Play(); //play fx early to show player is in danger
        }

        if (hp <= 0f)
        {
            Destroy(this.gameObject, 0.2f);
        }
        else if (distance <= stopDistance)
        {
            if (mode == 0)
            {
                if (!hasDamagedPlayer)
                {
                    // damage player
                    playerStatsManager.Damage(damage);
                    hasDamagedPlayer = true;
                }
                Destroy(this.gameObject, 0.2f);
            }
            // else if (mode == 1)
            // {
            //     // start shooting
            //     if (GetComponent<Homing>().enabled)
            //     {
            //         GetComponent<Homing>().enabled = false;
            //     }
            // }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (hitEffect != null) hitEffect.Play();
            hp -= 0.1f;
        }
        if (other.gameObject.tag == "LaserBullet")
        {
            if (hitEffect != null) hitEffect.Play();
            hp -= 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
