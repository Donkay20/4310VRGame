using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthPH : MonoBehaviour
{
    public Slider slider; // Assign your UI Slider in the inspector
    public float decreaseAmountbyBullet = 0.5f; // damage by bullet
    public float decreaseAmountbyLaser = 25f; // damage by laser
    [SerializeField] private GameObject endUI;
    private void Start()
    {
        InitializeHealth();
    }

    void Update()
    {
        if(slider.value <= 1f)
        {
            Time.timeScale = 0;
            endUI.SetActive(true);
        }
    }

    public void Damage(float amount)
    {
        slider.value -= amount * Time.deltaTime;
    }

    public void DamageByBullet()
    {
        slider.value -= decreaseAmountbyBullet;
    }
    public void DamageByLaser()
    {
        slider.value -= decreaseAmountbyLaser;
    }
    private void InitializeHealth()
    {
        slider.maxValue = 100f; // Set the maximum health
        slider.value = slider.maxValue; // Start with full health
    }
}
