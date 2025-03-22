using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPointLight;
    [SerializeField] private Transform attackPointHeavy1;
    [SerializeField] private Transform attackPointHeavy2;
    [SerializeField] private float attackRangeLight;
    [SerializeField] private float attackRangeHeavy1;
    [SerializeField] private float attackRangeHeavy2;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask bossLayer;

    [SerializeField] private Animator animator;
    private PlayerStatus playerStatus;
    private bool IsAttacking;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LightAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HeavyAttack();
        }
    }

    void LightAttack()
    {
        IsAttacking = true;
        animator.SetBool("LightAttack", true);
    }

    void HeavyAttack()
    {
        IsAttacking = true;
        animator.SetBool("HeavyAttack", true);
    }

    public void ReturnToIdle()
    {
        IsAttacking = false;
        animator.SetBool("LightAttack", false);
        animator.SetBool("HeavyAttack", false);
    }

    void ApplyLightAttackDamage()
    {
        ApplyDamage(10, attackPointLight, attackRangeLight);
    }

    void ApplyHeavyAttackDamage1()
    {
        ApplyDamage(15, attackPointHeavy1, attackRangeHeavy1);
    }

    void ApplyHeavyAttackDamage2()
    {
        ApplyDamage(15, attackPointHeavy2, attackRangeHeavy2);
    }

    void ApplyDamage(int baseDamage, Transform attackPoint, float attackRange)
    {
        int finalDamage = baseDamage + (int)playerStatus.baseDamage;

        // Attaque les ennemis
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossStatus>().TakeDamage(finalDamage);
        }

        // Attaque le boss
        Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bossLayer);
        foreach (Collider2D boss in hitBoss)
        {
            boss.GetComponent<BossStatus>().BossHealth -= finalDamage;
            Debug.Log("Boss HP: " + boss.GetComponent<BossStatus>().BossHealth);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointLight == null || attackPointHeavy1 == null || attackPointHeavy2 == null) return;

        // Dessiner les zones d'attaque pour Light et Heavy Attack
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPointLight.position, attackRangeLight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointHeavy1.position, attackRangeHeavy1);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPointHeavy2.position, attackRangeHeavy2);
    }
}
