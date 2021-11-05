using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image barHealth;
    
    private void Awake()  {
        StatsHealthSysteme health = GetComponentInParent<StatsHealthSysteme>();
        // entity.ModifierHealth += HandleModifyeHealth;
        health.HealthBarUI += GetHealthNormalized;
    }
    

    public void GetHealthNormalized(float percent){
        barHealth.fillAmount = percent;
    }



}
