using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(-10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateHealthBar();

        if (currentHealth <= 0) { Die(); }
    }

    void UpdateHealthBar()
    {
        healthBar.value = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
