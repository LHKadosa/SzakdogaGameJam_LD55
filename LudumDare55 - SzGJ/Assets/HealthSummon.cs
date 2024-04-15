using UnityEngine.UI;
using UnityEngine;

public class HealthSummon : MonoBehaviour
{
    [Header("Attributes")]
    public float maxHealth;
    private float hitPoints;

    public Slider healthBar;
    public GameObject health;
    public GameObject bloodParticle;
    private AudioManager audioManager;
    private SummonController sc;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        sc = GameObject.Find("SummonController").GetComponent<SummonController>();
        health.SetActive(false);
        hitPoints = maxHealth;
        UpdateHealthBar();
        audioManager.PlaySFX(audioManager.unitSpawnSfx);
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
        sc.uiButtonManager.beSad();
        Instantiate(bloodParticle, this.transform.position, Quaternion.identity);
        audioManager.PlaySFX(audioManager.unitDeathSfx);
        Destroy(gameObject);
    }

    public void setHealth(int newHealth)
    {
        hitPoints = newHealth;
    }
}
