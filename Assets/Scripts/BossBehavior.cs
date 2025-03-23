using System;
using System.Collections;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.7f;
    [SerializeField] private float dashCooldown = 15f;

    private SpriteRenderer bossSpriteRenderer;
    private bool animationFinished = false;
    private bool isFacingRight = false;
    private bool isDead;
    private bool isDashing = false;
    private bool canDash = true; // Pour éviter que le boss dash en boucle

    void Start()
    {
        animator.applyRootMotion = false;
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        isDead = gameObject.GetComponent<BossStatus>().isDead;
        StartCoroutine(PlayAnimationAndWait());
    }

    IEnumerator PlayAnimationAndWait()
    {
        animator.Play("Taunt");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animationFinished = true;
    }

    void Update()
    {
        if (animationFinished && !isDashing)
        {
            MoveTowardsPlayerXAxis();
            if (canDash && Vector2.Distance(transform.position, target.transform.position) < 15f && Vector2.Distance(transform.position, target.transform.position) > 10f)
            {
                StartCoroutine(Dash());
                Debug.Log("POST DASH");
                Debug.Log(transform.position);
            }
        }
        isDead = gameObject.GetComponent<BossStatus>().isDead;
    }

    private void MoveTowardsPlayerXAxis()
    {
        if (target != null && !isDead && !isDashing)
        {
            animator.SetFloat("speed", 1);
            Vector3 targetPosition = new(target.position.x, transform.position.y, transform.position.z);
            transform.position = new Vector3(
                Vector3.MoveTowards(transform.position,
                targetPosition, speed * Time.deltaTime).x,
                transform.position.y, transform.position.z
            );

            if (Mathf.Approximately(target.position.x, transform.position.x))
            {
                animator.SetFloat("speed", 0);
            }

            if (target.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (target.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }
        }
    }

    private IEnumerator Dash()
    {
        if (!isDead)
        {
            animator.SetFloat("speed", 0);
            animator.SetBool("isDashing", true);
            isDashing = true;
            canDash = false;

            float initialTargetX = target.position.x;
            float timer = 0f;
            Vector3 dashDirection = (initialTargetX > transform.position.x) ? Vector3.right : Vector3.left;

            Debug.Log("AVANT");
            Debug.Log(transform.position);
            while (timer <= dashDuration)
            {
                transform.position += dashSpeed * Time.deltaTime * dashDirection;
                timer += Time.deltaTime;
                yield return null;
            }
            Debug.Log("APRES");
            Debug.Log(transform.position);
            animator.SetBool("isDashing", false);
            isDashing = false;

            animator.SetBool("", false);

        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        isFacingRight = !isFacingRight;
    }
}
