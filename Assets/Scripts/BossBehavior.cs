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
        if(target != null){
            Vector2 direction = (target.position - transform.position).normalized;
            direction.y = 0f;

            transform.position += (Vector3)direction * speed * Time.deltaTime;
            
            if(direction != Vector2.zero){
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
