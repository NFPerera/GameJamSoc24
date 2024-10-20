using System.Collections;
using System.Collections.Generic;
using Main.Scripts.TowerDefenseGame._Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthGauge : MonoBehaviour
{
    public Slider healthSlider; 
    public float maxHealth = 100f;
    [SerializeField] GameManager sourceHealth; 
    private float currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = sourceHealth.GetLifePoints(); 
        UpdateHealthGauge(); 
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = sourceHealth.GetLifePoints(); 
        UpdateHealthGauge(); 
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