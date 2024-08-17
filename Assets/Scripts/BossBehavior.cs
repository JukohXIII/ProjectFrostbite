using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.VersionControl;
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
        // Gets the sprite renderer of the boss
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimationAndWait());
    }

    // Routine for the Taunt animator to play ONCE
    IEnumerator PlayAnimationAndWait(){
        animator.Play("Taunt");
        //Wait for the end of the animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animationFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If taunt animation is finished
        if(animationFinished){
            MoveTowardsPlayerXAxis();
        }
    }

    private void MoveTowardsPlayerXAxis(){
        // If the target is loaded
        if(target != null){
            // Set the animation param "speed" to 1 to start the animation
            animator.SetFloat("speed",1);
            // Gets the target position on X axis and create a new vector
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            // Makes the boss move to the created previously created position with a new vector
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, targetPosition,speed*Time.deltaTime).x,
            transform.position.y, transform.position.z);

            // If the boss' position is quite the same as the player's position, sets the animation param "speed" to 0 to stop the moving animation
            // and start the idle animation
            if(Mathf.Approximately(target.position.x, transform.position.x)){
                animator.SetFloat("speed", 0);
            }

            // If the boss is on the player's right
            if(target.position.x < transform.position.x){
                // If it is facing right
                if(isFacingRight){
                    // Reverses the boss' sprite orientation
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                    isFacingRight = false;
                }
            // If the boss is on the player's left
            }else if(target.position.x > transform.position.x){
                // If the boss is not facing right
                if(!isFacingRight){
                    // Reverses the boss' sprite orientation
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                    isFacingRight = true;
                }  
            }else{} 
        }
    }
}
