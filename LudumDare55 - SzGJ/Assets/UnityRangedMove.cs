using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityRangedMove : MonoBehaviour
{
    [Header("Ranged unit's statistics")]
    [SerializeField] private GameObject bulletPrefab;
    public float speedMax = 0.75f;
    //public int damage = 25;
    private Vector2 direction;
    private Vector2 direction_mouse;
    private GameObject[] AllTargets;
    private GameObject ClosestTarget;
    public GameObject grafics;
    private SpriteRenderer spriteRenderer;
    public Color frozenColor;
    private AudioManager audioManager;
    [SerializeField] private Animator animator;

    [SerializeField] private float targetingRange = 0.1f;
    [SerializeField] private float bulletsPerSecond = 1f;
    private float timeUntilFire;

    private float SetSpeedBackInSeconds = 5f;
    private float speed;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        spriteRenderer = grafics.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        speed = speedMax;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), speed * Time.deltaTime);
            GetFacingDirectionBasedOnMousePosition();
            return;
        }
        FindClosest();
        if (ClosestTarget != null)
        {
            float distanceFormTarget = Vector2.Distance(ClosestTarget.transform.position, transform.position);
            if (distanceFormTarget > targetingRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, ClosestTarget.transform.position, speed * Time.deltaTime);
                GetFacingDirecetion();
            }
            else if (distanceFormTarget <= targetingRange && bulletsPerSecond < Time.time)
            {
                GetFacingDirecetion();
                timeUntilFire += Time.deltaTime;
                if (timeUntilFire >= 1f / bulletsPerSecond)
                {
                    Shoot();
                    timeUntilFire = 0f;
                }
            }
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

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BulletSummon bulletScript = bulletObj.GetComponent<BulletSummon>();
        bulletScript.SetTarget(ClosestTarget.transform);
        audioManager.PlaySFX(audioManager.rangedAttackSfx);
        StartCoroutine(AttackAnimTimer());
    }

    public void SlowDown()
    {
        speed = speedMax / 4; //Valami idözitövel vissza lehetne majd állítani
        spriteRenderer.color = frozenColor;
        StartCoroutine(setSpeedBack());
    }
    IEnumerator setSpeedBack()
    {
        while (true)
        {
            yield return new WaitForSeconds(SetSpeedBackInSeconds);
            speed = speedMax;
            spriteRenderer.color = Color.white;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (ClosestTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, ClosestTarget.transform.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, targetingRange);
        }
    }
    private void GetFacingDirecetion()
    {
        direction = new Vector2(ClosestTarget.transform.position.x - transform.position.x, ClosestTarget.transform.position.y - transform.position.y).normalized;
        if (direction.x < 0.01f)
        {
            gameObject.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction.x > 0.01f)
        {
            gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void GetFacingDirectionBasedOnMousePosition()
    {
        direction_mouse = new Vector2(GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).y - transform.position.y).normalized;
        if (direction_mouse.x < 0.01f)
        {
            gameObject.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction_mouse.x > 0.01f)
        {
            gameObject.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);
        }
    }
    IEnumerator AttackAnimTimer()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Attack", false);
    }
}
