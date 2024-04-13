using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoom : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int boomRange = 5;

    private Transform target;

    public void SetTarget(Transform target_)
    {
        target = target_;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Summon")
        {
            GameObject[] AllTargets = GameObject.FindGameObjectsWithTag("Summon");

            foreach (GameObject currentTartget in AllTargets)
            {
                float distanceToTarget = Vector3.Distance(currentTartget.transform.position, transform.position);
                if (distanceToTarget <= boomRange)
                {
                    currentTartget.gameObject.GetComponent<HealthSummon>().TakeDamage(bulletDamage);
                }
            }

            Destroy(gameObject);
        }
    }
}
