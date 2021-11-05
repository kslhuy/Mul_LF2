using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{
    public event Action<float> HealthBarUI;
    public event Action DeadEvent;
    public int HEALTH_MAX{get ; private set;} 
    public int currentHealth;
    public int healthRegenAmount {get ; private set;} 
    float healthNormalized;
    public Health(int HealthMAX, int currentHealth , int healthRegenAmount){
        this.currentHealth = currentHealth;
        this.healthRegenAmount = healthRegenAmount;
        HEALTH_MAX = HealthMAX;
    }


    public void SpendHealth(int amount){
        
        currentHealth -= amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0f) {
            DeadEvent?.Invoke();
        }
        healthNormalized =  ((float)((float)currentHealth/HEALTH_MAX));
        HealthBarUI?.Invoke(healthNormalized);
    }


}
