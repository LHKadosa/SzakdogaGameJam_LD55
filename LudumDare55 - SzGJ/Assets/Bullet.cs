using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 5f;

    private Transform target;

    public void SetTarget(Transform target_){
        target = target_;
    }

    private void FixedUpdate(){
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ///TODO: Take Health form enemy
        Destroy(gameObject);
    }
}
