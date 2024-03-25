using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossPhases : MonoBehaviour
{
    public float lookSpeed = 0.3f;
    public float phaseSwitchTimer = 10.0f;
    public Boolean phase = true;
    public float missleLaunchTimer = 3.0f;
    public GameObject missleSpawner;
    public GameObject missle;
    public float timer1;
    public float timer2;

    // Start is called before the first frame update
    void Start()
    {
        timer1 = phaseSwitchTimer;
        timer2 = missleLaunchTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Camera.main.gameObject.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), lookSpeed * Time.deltaTime);

        timer1 -= Time.deltaTime;
        
        if (timer1 <= 0)
        {
            phase = !phase;
            timer1 = phaseSwitchTimer;
        }

        if(phase)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                phase = !phase;
                timer2 = missleLaunchTimer;

                GameObject obj = Instantiate(missle, missleSpawner.transform.position, Quaternion.LookRotation(direction));
                obj.GetComponent<Homing>().target = Camera.main.gameObject;
                obj.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
            }
        }
    }
}
