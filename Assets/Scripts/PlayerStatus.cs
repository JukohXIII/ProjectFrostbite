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
    [SerializeField] private Animator animator;
    [SerializeField] private bool isDead;
    [SerializeField] private double xpAmount;
    private double xpNeedToLvlUp;
        
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 100;
        playerLevel = 1;
        baseDamage = 50;
        staminaQuantity = 100;
        manaQuantity = 50;
        isDead = false;
        xpNeedToLvlUp = playerLevel * 400;
        xpAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetCharacterState();
        if (xpAmount >= xpNeedToLvlUp)
        {
            xpAmount = 0;
            playerLevel++;
        }
    }
    
    void GetCharacterState(){
        if (healthPoints <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }

    int GetPlayerLevel()
    {
        return playerLevel;
    }
}
