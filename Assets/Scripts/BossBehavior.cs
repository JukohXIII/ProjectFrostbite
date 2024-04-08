using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        if(target!=){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            Vector2 direction = target.position - transform.position;
            transform.up = direction.normalized;
        }
    }
}
