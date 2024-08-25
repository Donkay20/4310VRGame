using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets; 

public class PlayerStats : MonoBehaviour
{
    [Header("Player Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image healthSlider;
    [SerializeField] private GameObject endUI;

    [Header("Fuel System Settings")]
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float fuelConsumptionRate = 1f; // Fuel consumed per second
    [SerializeField] private float fuelDropFrequency = 1f; // How often fuel level drops
    [SerializeField] private DynamicMoveProvider moveProvider; // reference to the movement script of XR toolkit
    private float currentFuel;
    private float fuelTimer;
    [SerializeField] private Image fuelSlider;

    [Header("Bullet Magazine Settings")]
    [SerializeField] private int maxBullets = 30;
    public int currentBullets;
    [SerializeField] private Text bulletsText;
    [SerializeField] private Text bulletsTextSide1;
    [SerializeField] private Text bulletsTextSide2;
    [SerializeField] private Text bulletsTextMax;

    [Header("Level System Settings")]
    [SerializeField] private Text levelText;
    private int currentLevel;
    [SerializeField] private Animator levelTextAnim;
    [SerializeField] private TMP_Text upgradeText;

    [Header("Stun Settings")]
    [SerializeField] private GameObject stunScreen;
    [SerializeField] private TMP_Text stunText;
    private bool stunned;
    private int savedBullets;
    public int stunCounter;


    void Start()
    {
        currentLevel = 1;
        currentHealth = maxHealth;
        currentFuel = maxFuel;
        currentBullets = maxBullets;
        stunned = false;
        stunCounter = 0;
        InitializeSliders();
        UpdateBulletsText();
    }
    private void InitializeSliders()
    {
        if (healthSlider != null)
        {
            healthSlider.fillAmount = 1.0f;
        }

        if (fuelSlider != null)
        {
            fuelSlider.fillAmount = 1.0f;
        }
    }

    void Update()
    {
        UpdateFuel();
        if (moveProvider != null)
        {
            moveProvider.enabled = currentFuel > 0;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Damage(10); // Simulate taking 10 damage
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Heal(10); // Simulate healing 10 health
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Refuel(); // Refuel to maximum fuel
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            FireBullet(); // Simulate firing a bullet
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Reload(); // Reload the gun
        }*/
        if (stunned)
        {
            moveProvider.enabled = false;
            CheckStunInput(); // take out of stun if this is the case
        }
        if(currentHealth <= 0f)
        {
            Time.timeScale = 0;
            endUI.SetActive(true);
        }
    }

    public void Stun()
    {
        stunned = true;
        stunScreen.SetActive(true);
        stunText.text = "SYSTEM CONTROL ERROR\r\nRESET NEEDED\r\n(5 PUMPS NEEDED)\r\n";
        savedBullets = currentBullets;
        currentBullets = 0;
        stunCounter = 0;
    }

    public void CheckStunInput()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            stunCounter++;
            stunText.text = "SYSTEM CONTROL ERROR\r\nRESET NEEDED\r\n(" + (5 - stunCounter) + " PUMPS NEEDED)\r\n";
            if(stunCounter >= 5)
            {
                moveProvider.enabled = true;
                currentBullets = savedBullets;
                stunned = false;
                stunScreen.SetActive(false);
            }
        }
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;
        if (healthSlider != null)
        {
            healthSlider.fillAmount = currentHealth / maxHealth;
            Color healthColor = healthSlider.color;
            healthColor.g = currentHealth / maxHealth;
            healthColor.b = currentHealth / maxHealth;
            healthSlider.color = healthColor;
        }
        Debug.Log("Current Health: " + currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.fillAmount = currentHealth / maxHealth;
            Color healthColor = healthSlider.color;
            healthColor.g = currentHealth / maxHealth;
            healthColor.b = currentHealth / maxHealth;
            healthSlider.color = healthColor;
        }
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
            if (fuelSlider != null)
            {
                fuelSlider.fillAmount = currentFuel / maxFuel;
                Color fuelColor = fuelSlider.color;
                fuelColor.g = currentFuel / maxFuel;
                fuelColor.b = currentFuel / maxFuel;
                fuelSlider.color = fuelColor;
            }
            Debug.Log("Current Fuel: " + currentFuel);
        }
    }

    public void Refuel()
    {
        currentFuel = maxFuel;
        if (fuelSlider != null)
        {
            fuelSlider.fillAmount = currentFuel / maxFuel;
            Color fuelColor = fuelSlider.color;
            fuelColor.g = currentFuel / maxFuel;
            fuelColor.b = currentFuel / maxFuel;
            fuelSlider.color = fuelColor;
        }
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

    public void LevelUp()
    {
        //levelTextAnim.Play("FadeIn");
        currentLevel += 1;
        levelText.text = "Level " + currentLevel;
        Debug.Log("Leveled up. Current level: " + currentLevel);

        if (currentLevel == 2 || currentLevel == 5 || currentLevel == 8 || currentLevel == 11)
        {
            int change = (int)(maxBullets * 0.1);
            maxBullets += change;
            currentBullets += change;
            UpdateBulletsText();

            upgradeText.text += "+10% MAX BULLETS\r\n";
            Debug.Log("LEVEL UP: +10% MAX BULLETS ");
        }
        else if (currentLevel == 3 || currentLevel == 6 || currentLevel == 9 || currentLevel == 12)
        {
            int change = (int)(maxHealth * 0.1);
            maxHealth += change;
            currentHealth += change;
            if (healthSlider != null) healthSlider.fillAmount = currentHealth / maxHealth;

            upgradeText.text += "+10% MAX HEALTH\r\n";
            Debug.Log("LEVEL UP: +10% MAX HEALTH ");
        }
        else if (currentLevel == 4 || currentLevel == 7 || currentLevel == 10 || currentLevel == 13)
        {
            int change = (int)(maxFuel * 0.1);
            maxFuel += change;
            currentFuel += change;
            if (fuelSlider != null) fuelSlider.fillAmount = currentFuel / maxFuel;

            upgradeText.text += "+10% MAX FUEL\r\n";
            Debug.Log("LEVEL UP: +10% MAX FUEL ");
        }

        //levelTextAnim.Play("FadeOut");
    }

    private void UpdateBulletsText()
    {
        if (!stunned)
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
            if (bulletsTextMax != null)
            {
                bulletsTextMax.text = "/" + maxBullets;
            }
        }
    }

}
