using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;


    private float speed = 5f;
    private bool isMoving = false;
    private SpriteRenderer bossSpriteRenderer;
    void Start(){
        bossSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayerXAxis();

    }

    private void MoveTowardsPlayerXAxis(){
        if(target != null){
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, targetPosition,speed*Time.deltaTime).x,
            transform.position.y, transform.position.z);
        }
        while(transform.position.x != target.position.x){
            isMoving = true;
        }
        isMoving=false;
    }
    
}
