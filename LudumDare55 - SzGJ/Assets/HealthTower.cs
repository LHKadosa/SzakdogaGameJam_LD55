using UnityEngine.UI;
using UnityEngine;

public class HealthTower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject TowerChunkRemain;
    [SerializeField] private int moneyAfterDeath;

    private float hitPoints;
    private SummonController sc;

    public Slider healthBar;
    public GameObject health;

    [SerializeField] private GameObject WinCondition;

    void Start()
    {
        sc = GameObject.Find("SummonController").GetComponent<SummonController>();
        health.SetActive(false);
        hitPoints = maxHealth;
        UpdateHealthBar();
        WinCondition = GameObject.Find("Wincondition");
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        UpdateHealthBar();

        if (hitPoints < maxHealth)
        {
            if (!this.gameObject.scene.isLoaded) return;
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
        WinCondition.GetComponent<WinCon>().CheckTowerNumber();
        if (!this.gameObject.scene.isLoaded) return;
        sc.addMoney(moneyAfterDeath);
        Instantiate(TowerChunkRemain, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
