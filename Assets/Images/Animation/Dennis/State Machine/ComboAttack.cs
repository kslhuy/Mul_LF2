using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboAttackType", menuName = "ComboAttack/Type")]
public class ComboAttack : ScriptableObject
{
    [SerializeField] List<KeyPress> NeedKeyPress ; //the List and order of the Moves
    [SerializeField] TypeSkills typeCombo; //The kind of the move
    [SerializeField] CharacterTypeEnum characterType;
    

    [Tooltip("Could be damage, could be healing, or other things. This is a base, nominal value that will get modified by game logic when the action takes effect")]
    public int damageAmount;

    [Tooltip("How much it costs in Mana to play this Action")]
    public int ManaCost;

    [Tooltip("How far the Action performer can be from the Target")]
    public float Range;

    [Tooltip("Duration in seconds that this Action takes to play")]
    public float DurationSeconds;

    // [SerializeField] int ComboPriorty = 0; //the more complicated the move the higher the Priorty

    // Not use with mobile
    public bool isTheSame(List<KeyPress> playerKeyCodes) //Check if we can perform this move from the entered keys
    {
        int comboIndex = 0;

        for (int i = 0; i < playerKeyCodes.Count; i++)
        {
            if (playerKeyCodes[i] == NeedKeyPress[comboIndex])
            {
                comboIndex++;
                if (comboIndex == NeedKeyPress.Count) //The end of the Combo List
                    return true;
            }
            else
                comboIndex = 0;
        }
        return false;

    }
    
    public TypeSkills GetTypeOfCombo(){
        return typeCombo;
    }
}
