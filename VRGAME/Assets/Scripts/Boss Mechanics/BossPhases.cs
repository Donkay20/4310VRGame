using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        timer2 -= Time.deltaTime;

        if (timer1 <= 0)
        {
            //phase switch
            phase = !phase;
            timer1 = phaseSwitchMaxTime;
            timer2 = attackMaxTime;
        }
        else
        {
            //attack
            Slider bossHP = GameObject.FindWithTag("EnemyHealth").GetComponent<Slider>();
            Boolean secondHalf = !(bossHP.value > (bossHP.maxValue / 2f));
            //Debug.Log(bossHP.value + "/" + (bossHP.maxValue / 2f));
            if (phase)
            {
                if (timer2 <= 0)
                {
                    timer2 = attackMaxTime;
                    DisableAttack();

                    if (secondHalf)
                    {
                        currentAttack = UnityEngine.Random.Range(0, 3);
                        Debug.Log("2nd Half:" + currentAttack);
                    }
                    else
                    {
                        currentAttack = UnityEngine.Random.Range(0, 2);
                        Debug.Log("1st Half:" + currentAttack);
                    }

                    if (currentAttack == 0)
                    {
                        GameObject obj = Instantiate(missle, missleSpawner.transform.position, Quaternion.LookRotation(direction));
                        obj.GetComponent<Homing>().target = Camera.main.gameObject;
                        obj.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
                        Debug.Log("Fire Missile");
                    }
                    else if (currentAttack == 1)
                    {
                        if (secondHalf)
                        {
                            flameAttack2.SetActive(true);
                        }
                        else
                        {
                            flameAttack1.SetActive(true);
                        }
                        Debug.Log("Fire Thrower");

                    }
                    else
                    {
                        Debug.Log("Fire Balls");
                    }

                }
            }
            else
            {
                if (timer2 <= 0)
                {
                    timer2 = attackMaxTime;
                    DisableAttack();

                    if (secondHalf)
                    {
                        currentAttack = UnityEngine.Random.Range(0, 3);
                        Debug.Log("2nd Half:" + currentAttack);
                    }
                    else
                    {
                        currentAttack = UnityEngine.Random.Range(0, 2);
                        Debug.Log("1st Half:" + currentAttack);
                    }

                    if (currentAttack == 0)
                    {
                        GameObject obj = Instantiate(drone, droneSpawner.transform.position, Quaternion.LookRotation(direction));
                        obj.GetComponent<Homing>().target = Camera.main.gameObject;
                        obj.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
                        Debug.Log("Electric Drone");
                    }
                    else if (currentAttack == 1)
                    {
                        if (secondHalf)
                        {
                            beamAttack2.SetActive(true);
                        }
                        else
                        {
                            beamAttack1.SetActive(true);
                        }
                        Debug.Log("Electric Beam");
                    }
                    else
                    {
                        //do lightning strike attack
                        Debug.Log("Electric Strikes");
                    }

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
