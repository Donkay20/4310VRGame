using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthPH : MonoBehaviour
{
    public Slider slider; // Assign your UI Slider in the inspector
    public float decreaseAmount = 0.1f; // The amount by which the slider value will decrease

    void Update()
    {
        
    }

    public void Damage(float amount)
    {
        slider.value -= amount * Time.deltaTime;
    }
}
