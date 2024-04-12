using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDetection : MonoBehaviour
{
    public int mode = 0;
    public GameObject target;
    public float damage = 20;
    public float speed = 0.1f;
    public float stopDistance = 1.0f;
    public float hp = 0.5f;

    // This script sets what happens when object reaches the target
    // Mode 0: on impact destroy and decrease player health
    // Mode 1: after a certain distance away, stop moving and start shooting

    void Start()
    {
    }

    void Update()
    {
        float distance = 3000;
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            distance = direction.magnitude;
        }
        

        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
        else if (distance <= stopDistance)
        {
            if (mode == 0)
            {
                // decrease target hp needed   
                Destroy(this.gameObject, speed);
                if (GetComponent<Homing>().isActiveAndEnabled)
                {
                    GetComponent<Homing>().gameObject.SetActive(false);
                }
            }
            else if (mode == 1)
            {
                // start shooting
                if (GetComponent<Homing>().enabled)
                {
                    GetComponent<Homing>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hp -= 0.1f;
        }
        if (other.gameObject.tag == "LaserBullet")
        {
            hp -= 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {

        }
    }
}
