using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private GameObject TowerChunkRemain;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if(hitPoints <= 0)
        {
            if (!this.gameObject.scene.isLoaded) return;
            Instantiate(TowerChunkRemain, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
