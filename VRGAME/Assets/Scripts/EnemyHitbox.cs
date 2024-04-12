using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public EnemyHealthPH enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyHealth.DamageByBullet();
        }
        if (other.gameObject.tag == "LaserBullet")
        {
            enemyHealth.DamageByLaser();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            
        }
    }
}
