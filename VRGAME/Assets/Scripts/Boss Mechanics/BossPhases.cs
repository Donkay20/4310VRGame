
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPhases : MonoBehaviour
{
    public readonly float LOOK_SPEED = 0.3f; //speed for boss to look at player
    public readonly float ATTACK_COOLDOWN = 10f; //time btwn each attack before halfhealth
    public readonly float ATTACK_COOLDOWN_2 = 7f; //time btwn each attack after halfhealth
    public GameObject missleSpawner, missle;
    public GameObject droneSpawner, drone;
    public GameObject beamAttack1, beamAttack2, flameAttack1, flameAttack2;
    private Vector3 direction;
    public float attackTimer; //timer for attacks
    void Start() {
        attackTimer = ATTACK_COOLDOWN;
    }

    void Update() {
        // make boss look at player
        direction = Camera.main.gameObject.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), LOOK_SPEED * Time.deltaTime);

        attackTimer -= Time.deltaTime;

        if (attackTimer < 0) {
            DisableAttack();
            Attack();
        }
    }

    private void Attack() {

        Slider bossHP = GameObject.FindWithTag("EnemyHealth").GetComponent<Slider>();
        bool secondHalf = !(bossHP.value > (bossHP.maxValue / 2f));

        if(!secondHalf)
            attackTimer = ATTACK_COOLDOWN;
        else
            attackTimer = ATTACK_COOLDOWN_2;
        
        switch (Random.Range(0,4)) {
            case 0: //Fire | Missle Barrage
                StartCoroutine(MissleBarrage());
                Debug.Log("Flame Missle Barrage Activated.");
                break;
            case 1: //Fire | Flamethrower
                if (!secondHalf) {
                    flameAttack1.SetActive(true);
                    Debug.Log("Flamethrower attack activated.");
                } else {
                    flameAttack2.SetActive(true);
                    Debug.Log("Enhanced Flamethrower attack activated.");
                }
                break;
            case 2: //Electric | Drone Barrage
                GameObject eleDrone = Instantiate(drone, droneSpawner.transform.position, Quaternion.LookRotation(direction));
                eleDrone.GetComponent<Homing>().target = Camera.main.gameObject;
                eleDrone.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
                Debug.Log("Electric Drone Barrage activated.");
                break;
            case 3: //Electric | Electric Beam
                if (!secondHalf) {
                    beamAttack1.SetActive(true);
                    Debug.Log("Electric Beam attack activated.");
                } else {
                    beamAttack2.SetActive(true);
                    Debug.Log("Enhanced Electric Beam attack activated.");
                }
                break;
        }
    }

    private IEnumerator MissleBarrage() {
        int numberOfMissles = Random.Range(2,5);
        while (numberOfMissles > 0) {
            GameObject fireMissle = Instantiate(missle, missleSpawner.transform.position, Quaternion.LookRotation(direction));
            fireMissle.GetComponent<Homing>().target = Camera.main.gameObject;
            fireMissle.GetComponent<DistanceDetection>().target = Camera.main.gameObject;
            numberOfMissles--;
            yield return new WaitForSeconds(3);
        }
    }

    private void DisableAttack() {
        StopAllCoroutines();
        flameAttack1.SetActive(false);
        flameAttack2.SetActive(false);
        beamAttack1.SetActive(false);
        beamAttack2.SetActive(false);
    }
}