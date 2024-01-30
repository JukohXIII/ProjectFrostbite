using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int healthPoints;
    [SerializeField] private int playerLevel;
    [SerializeField] private double baseDamage;
    [SerializeField] private double staminaQuantity;
    [SerializeField] private double manaQuantity;
    
    
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 100;
        playerLevel = 1;
        baseDamage = 50;
        staminaQuantity = 100;
        manaQuantity = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
