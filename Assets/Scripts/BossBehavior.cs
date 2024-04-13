using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 5f;
    private SpriteRenderer bossSpriteRenderer;
    private bool animationFinished = false;
    private bool isFacingRight = false;
    
    void Start(){
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimationAndWait());
    }

    IEnumerator PlayAnimationAndWait(){
        animator.Play("Taunt");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animationFinished = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(animationFinished){
            MoveTowardsPlayerXAxis();
        }
    }

    private void MoveTowardsPlayerXAxis(){
        if(target != null){
            animator.SetFloat("speed",1);
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, targetPosition,speed*Time.deltaTime).x,
            transform.position.y, transform.position.z);

            if(Mathf.Approximately(target.position.x, transform.position.x)){
            animator.SetFloat("speed", 0);
            }

            if(target.position.x < transform.position.x){
                if(isFacingRight){
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                    isFacingRight = false;
                }
            }else if(target.position.x > transform.position.x){
                if(!isFacingRight){
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                    isFacingRight = true;
                }
                
            }else{} 
        }
    }
}
