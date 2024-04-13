using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySuicideBomber : MonoBehaviour
{
    [Header("Bomber unit's statistics")]
    public float speedMax = 6.5f;
    public int damage = 300;
    public float SetSpeedBackInSeconds = 5f;
    private Vector2 direction; //majd a sprite elforgatásához
    private GameObject[] AllTargets;
    private GameObject ClosestTarget;

    public List<GameObject> currentCollisions = new List<GameObject>();

    private float speed;

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
            transform.position = Vector2.MoveTowards(transform.position, ClosestTarget.transform.position, speed * Time.deltaTime);
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
    public void SlowDown()
    {
        speed = speedMax / 4; //Valami idözitövel vissza lehetne majd állítani
        StartCoroutine(setSpeedBack());
    }
    IEnumerator setSpeedBack()
    {
        while (true)
        {
            yield return new WaitForSeconds(SetSpeedBackInSeconds);
            speed = speedMax;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (ClosestTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, ClosestTarget.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Tower")
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(5, 5);
            currentCollisions.Add(other.gameObject);
            Debug.Log(currentCollisions.Count);
            StartCoroutine(BeforeBoom(other));
        }
    }
    IEnumerator BeforeBoom(Collision2D other)
    {
        while (true)
        {
            if (ClosestTarget != null)
            {
                gameObject.GetComponent<HealthSummon>().hitPoints = 5000;
                yield return new WaitForSeconds(0.5f);
                foreach (GameObject item in currentCollisions)
                {
                    item.gameObject.GetComponent<HealthTower>().TakeDamage(damage);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
