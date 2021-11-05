using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHealthSysteme : MonoBehaviour{
    
    // Health 
    public event Action<float> HealthBarUI;
    public event Action DeadEvent;
    private int HEALTH_MAX;
    public int currentHealth{get ; private set;} 
    public int healthRegenAmount {get ; private set;} 
    float healthNormalized;

    // Mana
    public event Action<float> ManaBarUI;
    private int MANA_MAX;
    public int currentMana{get ; private set;}
    public int manaRegenAmount {get ; private set;} 

    float manaNormalized;
    private float _timer;

    // Mana and Health 
    [SerializeField] PlayerData HealthData;
    private void Start() {
        HEALTH_MAX = HealthData.healthMAX;
        MANA_MAX = HealthData.manaMAX;

        currentHealth = HealthData.healthMAX;
        healthRegenAmount  = HealthData.healthRegen;
        

        currentMana = HealthData.manaMAX;
        manaRegenAmount = HealthData.manaRegen;

        Debug.Log(currentMana);
    }
    public void SpendMana(int amount){
        if (currentMana >= amount){
            currentMana -= amount;
        }
        manaNormalized =  ((float)((float)currentMana/MANA_MAX));
        ManaBarUI?.Invoke(manaNormalized);
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

    public void ManaRegen(){
        _timer = Time.time;
        if ( currentMana < MANA_MAX){
            currentMana += (int)(manaRegenAmount*Time.deltaTime);
            SpendMana(0);
        }
    }

    private void HealthRegen(){
        if ( currentHealth < HEALTH_MAX){
            currentHealth += (int)(healthRegenAmount*Time.deltaTime);
            SpendHealth(0);
        }
    }
    
    private void LateUpdate() {
        if ( Time.time - _timer > 1f && currentMana <= MANA_MAX){
            ManaRegen();
        }
    }


}
