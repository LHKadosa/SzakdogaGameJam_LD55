using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMultishot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletMiniPrefab;

    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

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
            other.gameObject.GetComponent<HealthSummon>().TakeDamage(bulletDamage);

            GameObject bulletObj = Instantiate(bulletMiniPrefab, transform.position, Quaternion.identity);
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();

            Transform tempTransform = transform;
            tempTransform.position = new Vector3(10,1,0);

            bulletScript.SetTarget(tempTransform);

            Destroy(gameObject);
        }
    }
}
