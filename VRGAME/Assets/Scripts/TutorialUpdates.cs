using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TutorialUpdates : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStatsManager;
    [SerializeField] private GunShoot laserEnergyScript;
    public GameObject popUp1;
    public GameObject popUp2;
    public GameObject popUp3;
    public bool popUp1Triggered = false;
    public bool popUp2Triggered = false;
    public bool popUp3Triggered;

    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = GameObject.FindGameObjectWithTag("PlayerStatsManager").GetComponent<PlayerStats>();
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            popUp1.SetActive(false);
            popUp3.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (gameObject.name == "TutorialHitbox1" && other.gameObject.name == "Gun" && !popUp1Triggered)
        { 
            popUp1.SetActive(true);
            popUp1Triggered = true;
        }
        if (gameObject.name == "TutorialHitbox2" && other.gameObject.name == "Gun" && !popUp2Triggered)
        {
            popUp2.SetActive(true);
            playerStatsManager.Damage(50f);
            popUp2Triggered = true;
            laserEnergyScript.ChargeEnergy(75f);
        }
    }
}
