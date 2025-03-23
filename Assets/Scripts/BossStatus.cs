using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public float bossHealth;
    public bool isDead = false;
    [SerializeField] private Animator animator;

    SpriteRenderer spriteRenderer;
    Color spriteRendererColor;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRendererColor = spriteRenderer.color;
    }

    void Update()
    {
        if (bossHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        StartCoroutine(DamageFlashEffect());
    }

    private void Die()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }

    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }

    private IEnumerator DamageFlashEffect()
    {
        spriteRenderer.color = new Color(255f / 255f, 180f / 255f, 180f / 255f, 1f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = spriteRendererColor;
    }
}