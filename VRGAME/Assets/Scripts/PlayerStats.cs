using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets; 

public class PlayerStats : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject endUI;

    [Header("Fuel System Settings")]
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fuelConsumptionRate = 1f; // Fuel consumed per second
    [SerializeField] private float fuelDropFrequency = 1f; // How often fuel level drops
    [SerializeField] private DynamicMoveProvider moveProvider; // reference to the movement script of XR toolkit
    private float currentFuel;
    private float fuelTimer;
    [SerializeField] private Slider fuelSlider;

    [Header("Bullet Magazine Settings")]
    [SerializeField] private int maxBullets = 30;
    public int currentBullets;
    [SerializeField] private Text bulletsText;
    [SerializeField] private Text bulletsTextSide1;
    [SerializeField] private Text bulletsTextSide2;

    void Start()
    {
        currentHealth = maxHealth;
        currentFuel = maxFuel;
        currentBullets = maxBullets;
        InitializeSliders();
        UpdateBulletsText();
    }
    private void InitializeSliders()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (fuelSlider != null)
        {
            fuelSlider.maxValue = maxFuel;
            fuelSlider.value = currentFuel;
        }
    }

    void Update()
    {
        UpdateFuel();
        if (moveProvider != null)
        {
            moveProvider.enabled = currentFuel > 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Damage(10); // Simulate taking 10 damage
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Heal(10); // Simulate healing 10 health
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Refuel(); // Refuel to maximum fuel
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FireBullet(); // Simulate firing a bullet
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Reload(); // Reload the gun
        }
        if(currentHealth <= 0f)
        {
            Time.timeScale = 0;
            endUI.SetActive(true);
        }
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        if (healthSlider != null) healthSlider.value = currentHealth;
        Debug.Log("Current Health: " + currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.value = currentHealth;
        Debug.Log("Current Health: " + currentHealth);
    }

    private void UpdateFuel()
    {
        fuelTimer += Time.deltaTime;
        if (fuelTimer >= fuelDropFrequency)
        {
            currentFuel -= fuelConsumptionRate;
            fuelTimer = 0;
            if (currentFuel < 0) currentFuel = 0;
            if (fuelSlider != null) fuelSlider.value = currentFuel;
            Debug.Log("Current Fuel: " + currentFuel);
        }
    }

    public void Refuel()
    {
        currentFuel = maxFuel;
        if (fuelSlider != null) fuelSlider.value = currentFuel;
        Debug.Log("Fuel tank refueled to " + currentFuel);
    }

    public void FireBullet()
    {
        if (currentBullets > 0)
        {
            currentBullets--;
            UpdateBulletsText();
            Debug.Log("Fired a bullet. Bullets left: " + currentBullets);
        }
        else
        {
            Debug.Log("No bullets left to fire!");
        }
    }

    public void Reload()
    {
        currentBullets = maxBullets;
        UpdateBulletsText();
        Debug.Log("Magazine reloaded. Bullets available: " + currentBullets);
    }

    private void UpdateBulletsText()
    {
        if (bulletsText != null)
        {
            bulletsText.text = "" + currentBullets;
        }
        if (bulletsTextSide1 != null)
        {
            bulletsTextSide1.text = "" + currentBullets;
        }
        if (bulletsTextSide2 != null)
        {
            bulletsTextSide2.text = "" + currentBullets;
        }
    }

}
