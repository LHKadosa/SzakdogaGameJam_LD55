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
    [SerializeField] private float boomRange = 5;
    [SerializeField] private float angle;

    private Transform target;
    private float DestroySelfInSeconds = 5f;

    public void Start()
    {
        StartCoroutine(DestroyBulletAfterSeconds()); //önmegsemmisítõ
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
        ChangeSpriteOnBullet();
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
    private void ChangeSpriteOnBullet()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime);
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
