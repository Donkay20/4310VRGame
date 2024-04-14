using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float shootDelay = 2f;
    [Range(0, 30000), SerializeField] private float bulletSpeed;
    [Space, SerializeField] private AudioSource audiosource;
    [SerializeField] private Text cooldownText;
    [SerializeField] private Text cooldownText2;
    [SerializeField] private Text cooldownText3;

    [SerializeField] private Image battery1;
    [SerializeField] private Image battery2;
    [SerializeField] private Image battery3;
    [SerializeField] private Image battery4;
    [SerializeField] private Text statusText;

    public float energy = 0f;  // Current energy level of the gun as a percentage

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChargeEnergy(25f);  // Charge energy by 25% when C is pressed
        }

        if (Input.GetKeyDown(KeyCode.Space) && energy >= 100f)
        {
            Shoot();  // Shoot when space is pressed and energy is 100%
        }

        UpdateBatteryDisplay();  // Update the UI display of energy
        UpdateStatusText();  // Update the status text
    }

    public void Shoot()
    {
        if (energy >= 100f)
        {
            Vector3 spawnPosition = bulletPosition.position + bulletPosition.forward * 10;
            var bulletPrefab = Instantiate(bullet, spawnPosition, bulletPosition.rotation);
            var bulletRb = bulletPrefab.GetComponent<Rigidbody>();
            bulletRb.AddForce(bulletPrefab.transform.forward * bulletSpeed);
            Destroy(bulletPrefab, 0.5f);

            GunShotAudio();
            energy = 0f;  // Reset energy after shooting
            UpdateBatteryDisplay();
        }
        else
        {
            Debug.Log("not enough energy");
        }
    }

    public void ChargeEnergy(float amount)
    {
        energy += amount;
        if (energy > 100f)
            energy = 100f;  // Cap energy at 100%
    }

    private void UpdateBatteryDisplay()
    {
        battery1.enabled = energy >= 25f;
        battery2.enabled = energy >= 50f;
        battery3.enabled = energy >= 75f;
        battery4.enabled = energy >= 100f;
    }

    private void UpdateStatusText()
    {
        if (statusText != null)
            statusText.text = energy >= 100f ? "Ready to Shoot!" : $"Energy: {energy}%";
    }

    private void GunShotAudio()
    {
        audiosource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audiosource.Play();
    }
}
