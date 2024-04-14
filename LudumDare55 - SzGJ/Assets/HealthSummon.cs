using UnityEngine.UI;
using UnityEngine;

public class HealthSummon : MonoBehaviour
{
    [Header("Attributes")]
    public float maxHealth;
    public float hitPoints;

    public Slider healthBar;
    public GameObject health;

    void Start()
    {
        health.SetActive(false);
        hitPoints = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        UpdateHealthBar();

        if (hitPoints < maxHealth)
        {
            health.SetActive(true);
        }

        if (hitPoints <= 0) { Die(); }
    }

    void UpdateHealthBar()
    {
        healthBar.value = Mathf.Clamp(hitPoints / maxHealth, 0, 1);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
