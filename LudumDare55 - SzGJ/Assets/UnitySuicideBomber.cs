using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySuicideBomber : MonoBehaviour
{
    [Header("Bomber unit's statistics")]
    public float speedMax = 6.5f;
    public int damage = 300;
    public float SetSpeedBackInSeconds = 5f;
    private Vector2 direction;
    private Vector2 direction_mouse;
    private GameObject[] AllTargets;
    private GameObject ClosestTarget;
    public GameObject grafics;
    private SpriteRenderer spriteRenderer;
    public Color frozenColor;
    private AudioManager audioManager;
    [SerializeField] GameObject ExplosionParticle;

    public List<GameObject> currentCollisions = new List<GameObject>();

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
            transform.position = Vector2.MoveTowards(transform.position, ClosestTarget.transform.position, speed * Time.deltaTime);
            GetFacingDirecetion();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Tower")
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1f, 1f);
            currentCollisions.Add(other.gameObject);
            //Debug.Log(currentCollisions.Count);
            StartCoroutine(BeforeBoom(other));
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
    IEnumerator BeforeBoom(Collision2D other)
    {
        while (true)
        {
            if (ClosestTarget != null)
            {
                gameObject.GetComponent<HealthSummon>().setHealth(5000);
                yield return new WaitForSeconds(0.2f);
                audioManager.PlaySFX(audioManager.suicideAttackSfx);
                foreach (GameObject item in currentCollisions)
                {
                    if (item != null)
                    {
                        item.gameObject.GetComponent<HealthTower>().TakeDamage(damage);
                    }
                }
                Instantiate(ExplosionParticle, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
