using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializableField] private int healthPoints;
    [SerializableField] private int playerLevel;
    [SerializableField] private double baseDamage;
    [SerializableField] private double staminaQuantity;
    [SerializableField] private double manaQuantity;
    
    
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
