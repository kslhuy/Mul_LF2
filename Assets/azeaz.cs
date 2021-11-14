using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class azeaz : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AttackButton m_AttackButton;
    void Start()
    {
        m_AttackButton.AttackAction += linhtiunh;
    }

    private void linhtiunh()
    {
        Debug.Log("Linh tinh");
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
