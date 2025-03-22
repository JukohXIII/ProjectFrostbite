using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public float BossHealth = 100f;
    private bool isDead = false;
    [SerializeField] private Animator animator;

    void Update()
    {
        if (BossHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    { 
        BossHealth -= damage;
        Debug.Log("Boss takes " + damage + " damage. Remaining HP: " + BossHealth);
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Boss Dead");
        
        if (animator != null)
        {
            animator.SetTrigger("Dying");
        }

        // Désactive le boss après un délai
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}