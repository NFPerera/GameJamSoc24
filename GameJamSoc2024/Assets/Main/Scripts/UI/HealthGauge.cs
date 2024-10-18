using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGauge : MonoBehaviour
{
    public Slider healthSlider; 
    public float maxHealth = 100f;
    private float currentHealth; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthGauge(); 
    }

    // Update is called once per frame
    void Update()
    {
        // For testing purposes, decrease health over time
        if (currentHealth > 0)
        {
            currentHealth -= Time.deltaTime * 10; // Decrease health by 10 units per second
            UpdateHealthGauge(); // Update the health gauge
        }
    }

    // Method to update the health gauge
    void UpdateHealthGauge()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    // Method to set health (can be called from other scripts)
    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth); 
        UpdateHealthGauge();
    }
}