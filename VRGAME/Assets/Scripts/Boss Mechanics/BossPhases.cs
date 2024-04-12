using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossPhases : MonoBehaviour
{
    public float lookSpeed = 0.3f;
    public float phaseSwitchMaxTime = 20.0f;
    public Boolean phase = true;
    public float attackMaxTime = 3.0f;
    public GameObject missleSpawner;
    public GameObject missle;
    public GameObject droneSpawner;
    public GameObject drone;
    public GameObject beamAttack1;
    public GameObject beamAttack2;
    public GameObject flameAttack1;
    public GameObject flameAttack2;
    public float timer1;
    public float timer2;
    private int currentAttack;

    // Start is called before the first frame update
    void Start()
    {
        timer1 = phaseSwitchMaxTime;
        timer2 = attackMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Camera.main.gameObject.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), lookSpeed * Time.deltaTime);

        timer1 -= Time.deltaTime;
        
        //phase switch
        if (timer1 <= 0)
        {
            phase = !phase;
            timer1 = phaseSwitchMaxTime;
        }

        //attack
        Boolean secondHalf = GetComponent<Health>().health > GetComponent<Health>().maxHealth / 2;
        timer2 -= Time.deltaTime;
        if (phase)
        {
            if (timer2 <= 0)
            {
                timer2 = attackMaxTime;
                DisableAttack();
                
                if (secondHalf)
                {
                    currentAttack = UnityEngine.Random.Range(0, 3);
                }
                else
                {
                    currentAttack = UnityEngine.Random.Range(0, 2);
                }

                if(currentAttack == 0)
                {
                    GameObject obj = Instantiate(missle, missleSpawner.transform.position, Quaternion.LookRotation(direction));
                    obj.GetComponent<Homing>().target = Camera.main.gameObject;
                    obj.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
                }
                else if(currentAttack == 1)
                {
                    if(secondHalf)
                    {
                        flameAttack1.SetActive(true);
                    }
                    else
                    {
                        flameAttack2.SetActive(true);
                    }

                }
                else
                {
                    //do the fire ball attack
                }
                
            }
        }
        else
        {
            if (timer2 <= 0)
            {
                timer2 = attackMaxTime;
                DisableAttack();

                if (secondHalf) { 
                    currentAttack = UnityEngine.Random.Range(0, 3);
                }else{
                    currentAttack = UnityEngine.Random.Range(0, 2);
                }

                if (currentAttack == 0)
                {
                    GameObject obj = Instantiate(drone, droneSpawner.transform.position, Quaternion.LookRotation(direction));
                    obj.GetComponent<Homing>().target = Camera.main.gameObject;
                    obj.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
                }
                else if (currentAttack == 1)
                {
                    if (secondHalf)
                    {
                        beamAttack1.SetActive(true);
                    }
                    else
                    {
                        beamAttack2.SetActive(true);
                    }
                }
                else
                {
                    //do lightning strike attack
                }

            }
        }
    }

    private void DisableAttack()
    {
        flameAttack1.SetActive(false);
        flameAttack2.SetActive(false);
        beamAttack1.SetActive(false);
        beamAttack2.SetActive(false);
    }
}
