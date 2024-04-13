using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSummon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

    private float DestroySelfInSeconds = 5f;

    public void Start()
    {
        StartCoroutine(DestroyBulletAfterSeconds()); //�nmegsemmis�t�
    }
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
        if (other.collider.gameObject.tag == "Tower")
        {
            other.gameObject.GetComponent<HealthTower>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBulletAfterSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(DestroySelfInSeconds);
            Destroy(gameObject);
            //Debug.Log("Bullet destroyed becouse it did not hit anything.");
        }
    }
}
