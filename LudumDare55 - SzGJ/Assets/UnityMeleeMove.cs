using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMeleeMove : MonoBehaviour
{
    [Header("Melee unit's statistics")]
    public float speed = 1f;
    private Vector2 direction; //majd a sprite elforgatásához
    private GameObject[] AllTargets;
    private GameObject ClosestTarget;


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

    private void OnDrawGizmosSelected()
    {
        if (ClosestTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, ClosestTarget.transform.position);
        }
    }
}
