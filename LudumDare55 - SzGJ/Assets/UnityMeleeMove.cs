using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMeleeMove : MonoBehaviour
{
    [Header("Melee unit's statistics")]
    public float speedMax = 1f;
    public int damage = 50;
    public float SetSpeedBackInSeconds = 5f;
    public float HitTowerInSeconds = 0.5f;
    private Vector2 direction; //majd a sprite elforgatásához
    private Vector2 direction_mouse;
    private GameObject[] AllTargets;
    private GameObject ClosestTarget;

    private float speed;

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
        speed = speedMax/4; //Valami idözitövel vissza lehetne majd állítani
        StartCoroutine(setSpeedBack());
    }
    IEnumerator setSpeedBack()
    {
        while (true)
        {
            yield return new WaitForSeconds(SetSpeedBackInSeconds);
            speed = speedMax;
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
            other.gameObject.GetComponent<HealthTower>().TakeDamage(damage);
            this.transform.position -= (other.transform.position - this.transform.position);
        }
    }
    private void GetFacingDirecetion()
    {
        direction = new Vector2(ClosestTarget.transform.position.x - transform.position.x, ClosestTarget.transform.position.y - transform.position.y).normalized;
        //Debug.Log("X: " + dir.x + ", " + "Y: " + dir.y);
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
}
