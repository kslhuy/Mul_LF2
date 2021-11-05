using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;


public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] StatsHealthSysteme mana;
    private bool deed;
     

    private void Start() {
        mana.ManaBarUI += GetManaNormalized;
    }

    public void GetManaNormalized(float manaNormalized){
        barImage.fillAmount = manaNormalized;
    }

    private void LateUpdate() {
        // ManaRegen();
    }


    

        
    
    
}
