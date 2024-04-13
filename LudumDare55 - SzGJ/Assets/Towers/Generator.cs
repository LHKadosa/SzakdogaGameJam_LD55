using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject TowerSlot_1;
    [SerializeField] private GameObject TowerSlot_2;
    [SerializeField] private GameObject TowerSlot_3;
    [SerializeField] private GameObject TowerSlot_4;
    [SerializeField] private GameObject TowerSlot_5;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (TowerSlot_1 != null) Gizmos.DrawLine(this.transform.position, TowerSlot_1.transform.position);
        if (TowerSlot_2 != null) Gizmos.DrawLine(this.transform.position, TowerSlot_2.transform.position);
        if (TowerSlot_3 != null) Gizmos.DrawLine(this.transform.position, TowerSlot_3.transform.position);
        if (TowerSlot_4 != null) Gizmos.DrawLine(this.transform.position, TowerSlot_4.transform.position);
        if (TowerSlot_5 != null) Gizmos.DrawLine(this.transform.position, TowerSlot_5.transform.position);
    }
    public void OnDestroy()
    {
        if (TowerSlot_1 != null) TowerSlot_1.GetComponent<HealthTower>().TakeDamage(999);
        if (TowerSlot_2 != null) TowerSlot_2.GetComponent<HealthTower>().TakeDamage(999);
        if (TowerSlot_3 != null) TowerSlot_3.GetComponent<HealthTower>().TakeDamage(999);
        if (TowerSlot_4 != null) TowerSlot_4.GetComponent<HealthTower>().TakeDamage(999);
        if (TowerSlot_5 != null) TowerSlot_5.GetComponent<HealthTower>().TakeDamage(999);
    }
}
