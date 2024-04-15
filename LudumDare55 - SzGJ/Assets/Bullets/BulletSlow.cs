using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float bulletSpeed = 5f;

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
            if (other.gameObject.GetComponent<UnityMeleeMove>() != null) other.gameObject.GetComponent<UnityMeleeMove>().SlowDown();
            if (other.gameObject.GetComponent<UnityRangedMove>() != null) other.gameObject.GetComponent<UnityRangedMove>().SlowDown();
            if (other.gameObject.GetComponent<UnitySuicideBomber>() != null) other.gameObject.GetComponent<UnitySuicideBomber>().SlowDown();
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
