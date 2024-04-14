using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float shootDelay = 10f;
    [Range(0, 30000), SerializeField] private float bulletSpeed;
    [Space, SerializeField] private AudioSource audiosourceShoot;
    [Space, SerializeField] private AudioSource audiosourceEmpty;
    private PlayerStats playerStatsManager;
    private float lastShot;

    void Start()
    {
        playerStatsManager = GameObject.FindGameObjectWithTag("PlayerStatsManager").GetComponent<PlayerStats>();
    }
    public void PistolShoot1()
    {
        if (playerStatsManager != null)
        {
            if (playerStatsManager.currentBullets > 0f)
            {
                playerStatsManager.FireBullet();

                if (lastShot > Time.time)
                    return;

                lastShot = Time.time + shootDelay;
                GunShotAudio();
                var bulletPrefab = Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
                var bulletRb = bulletPrefab.GetComponent<Rigidbody>();
                var direction = bulletPrefab.transform.TransformDirection(Vector3.forward);
                bulletRb.AddForce(direction * bulletSpeed);
                Destroy(bulletPrefab, 2f);
            }
            else
            {
                audiosourceEmpty.Play();
                Debug.Log("no bullets");
            }
        }
    }

    private void GunShotAudio()
    {
        var random = UnityEngine.Random.Range(0.8f, 1.2f);
        audiosourceShoot.pitch = random;
        audiosourceShoot.Play();
    }
}