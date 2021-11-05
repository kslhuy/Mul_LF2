using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
    public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDropHandler
    {

        static readonly Vector3 k_DownScale = new Vector3(0.95f, 0.95f, 0.95f);

        public event Action<TypeSkills> classAttackComboEvent;

        public event Action AttackAction;        
        private bool Up;
        private bool Def;
        private bool Down;


        private void Awake() {
            var slotUpButton = GameObject.FindGameObjectWithTag("UpSlotUI").GetComponent<UpSlotButton>();            
            var slotDownButton = GameObject.FindGameObjectWithTag("DownSlotUI").GetComponent<DownSlotButton>();       
            
            var defenseButton = GameObject.FindGameObjectWithTag("DefenseUI").GetComponent<DefenseButton>();            

            slotUpButton.upSlotEvent += SetSlotEvent;
            slotDownButton.downSlotEvent += SetSlotEvent;
            defenseButton.ResetComboEvent += SetSlotEvent;

        }

        private void SetSlotEvent(bool defEvent ,bool downEvent , bool upEvent)
        {
            Down = downEvent;
            Def = defEvent;
            Up = upEvent;
        }



        public void OnPointerUp(PointerEventData eventData)
        {
            // SendValueToControl(0.0f);
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // SendValueToControl(1.0f);
            AttackAction?.Invoke();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (Up && Def) 
            {
                classAttackComboEvent?.Invoke(TypeSkills.DefUpAttack);
                Def = false;
                Up = false;
            }

            else if (Down && Def) 
            {
                classAttackComboEvent?.Invoke(TypeSkills.DefDownAttack);
                Def = false;
                Down = false;
            }
        }


    }

}
