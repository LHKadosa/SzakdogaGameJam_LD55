using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bulletsPerSecond = 1f;

    GameObject ClosestTarget;
    private float timeUntilFire;


    private void Update(){
        FindClosest(); ///TODO: Nem jó, hogy minden frame-ben nézi

        if (ClosestTarget != null)
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bulletsPerSecond)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
        
    }

    private void Shoot(){
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(ClosestTarget.transform);
    }
    
    private void FindClosest()
    {
        float ClosestEnemyDistance = Mathf.Infinity;
        ClosestTarget = null;
        GameObject[] AllTargets = GameObject.FindGameObjectsWithTag("Summon");

        foreach (GameObject currentTartget in AllTargets)
        {
            float distanceToTarget = Vector3.Distance(currentTartget.transform.position, transform.position);
            if (distanceToTarget < ClosestEnemyDistance && distanceToTarget<=targetingRange)
            {
                ClosestEnemyDistance = distanceToTarget;
                ClosestTarget = currentTartget;
            }
        }
    }

    private void OnDrawGizmosSelected(){
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
