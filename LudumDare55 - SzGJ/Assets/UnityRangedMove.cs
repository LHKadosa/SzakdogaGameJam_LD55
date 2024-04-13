using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityRangedMove : MonoBehaviour
{
    [Header("Ranged unit's statistics")]
    [SerializeField] private GameObject bulletPrefab;
    public float speed = 0.75f;
    public int damage = 25;
    private Vector2 direction; //majd a sprite elforgatásához
    private GameObject[] AllTargets;
    private GameObject ClosestTarget;

    [SerializeField] private float targetingRange = 0.1f;
    [SerializeField] private float bulletsPerSecond = 1f;
    private float timeUntilFire;


    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), speed * Time.deltaTime);
            return;
        }
        FindClosest();
        if (ClosestTarget != null)
        {
            float distanceFormTarget = Vector2.Distance(ClosestTarget.transform.position, transform.position);
            if (distanceFormTarget > targetingRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, ClosestTarget.transform.position, speed * Time.deltaTime);
            }
            else if (distanceFormTarget <= targetingRange && bulletsPerSecond < Time.time)
            {
                timeUntilFire += Time.deltaTime;
                if (timeUntilFire >= 1f / bulletsPerSecond)
                {
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
        }
    }

    private void FindClosest()
    {
        float ClosestEnemyDistance = Mathf.Infinity;
        ClosestTarget = null;
        AllTargets = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject currentTartget in AllTargets)
        {
            float distanceToTarget = (currentTartget.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToTarget < ClosestEnemyDistance)
            {
                ClosestEnemyDistance = distanceToTarget;
                ClosestTarget = currentTartget;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletSummon bulletScript = bulletObj.GetComponent<BulletSummon>();
        bulletScript.SetTarget(ClosestTarget.transform);
    }

    private void OnDrawGizmosSelected()
    {
        if (ClosestTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, ClosestTarget.transform.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, targetingRange);
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Tower")
        {
            other.gameObject.GetComponent<HealthTower>().TakeDamage(damage);
        }
    }
    */
}
