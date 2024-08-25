using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceDetection1 : MonoBehaviour
{
    public GameObject target;
    public float hp = 10.0f;
    private PlayerStats playerStatsManager;
    public ParticleSystem hitEffect;
    private bool hasDamagedPlayer = false;

    public float bulletDamage = 0.1f;
    public float laserDamage = 20.0f;

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
        if (hp <= 0f)
        {
            Destroy(this.gameObject, 0.2f);
            SceneManager.LoadScene("BossArena");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (hitEffect != null) hitEffect.Play();
            hp -= bulletDamage;
        }
        if (other.gameObject.tag == "LaserBullet")
        {
            if (hitEffect != null) hitEffect.Play();
            hp -= laserDamage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
