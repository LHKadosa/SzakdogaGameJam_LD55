using UnityEngine.UI;
using UnityEngine;

public class HealthTower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject TowerChunkRemain;

    private float hitPoints;

    public Slider healthBar;
    public GameObject health;
    void Start()
    {
        health.SetActive(false);
        hitPoints = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        UpdateHealthBar();

        if (hitPoints < maxHealth)
        {
            health.SetActive(true);
        }

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = Mathf.Clamp(hitPoints / maxHealth, 0, 1);
    }

    void Die()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(TowerChunkRemain, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
