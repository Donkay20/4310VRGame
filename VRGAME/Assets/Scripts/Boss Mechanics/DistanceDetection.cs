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

    // This script sets what happens when object reaches the target
    // Mode 0: on impact destroy and decrease player health
    // Mode 1: after a certain distance away, stop moving and start shooting
    // Start is called before the first frame update

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= stopDistance)
            {
                if (mode == 0)
                {
                    // decrease target hp needed   
                    Destroy(this.gameObject, speed);
                    if (GetComponent<Homing>().isActiveAndEnabled)
                    {
                        GetComponent<Homing>().gameObject.SetActive(false);
                        Camera.main.gameObject.GetComponent<Health>().health -= damage;
                    }
                }
                else if (mode == 1)
                {
                    // start shooting
                    if (GetComponent<Homing>().isActiveAndEnabled)
                    {
                        GetComponent<Homing>().gameObject.SetActive(false);
                        Camera.main.gameObject.GetComponent<Health>().health -= damage; //only temporary
                    }
                }
            }
            else
            {
                GetComponent<Homing>().enabled = true;
            }
        }
    }
}
