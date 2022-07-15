using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6;
    [SerializeField] int damage = 10;
    [SerializeField] float maxSearchRadius = 10;
    [SerializeField] float lookForTargetTimerMax = 0.2f;

    Transform targetTransform;
    Rigidbody2D rigidbody2d;
    float lookForTargetTimer;
    HealthSystem healthSystem;

    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private void Start()
    {
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        rigidbody2d = GetComponent<Rigidbody2D>();
        lookForTargetTimer = UnityEngine.Random.Range(0, lookForTargetTimerMax);
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += OnDied;
    }

    private void Update()
    {
        HandleMovement();
        HandleTargetting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(damage);
            Destroy(gameObject);
        }
    }

    void LookForTargets()
    {
        Collider2D[] colliders2d = Physics2D.OverlapCircleAll(transform.position, maxSearchRadius);
        foreach (Collider2D collider2d in colliders2d)
        {
            Building building = collider2d.GetComponent<Building>();
            if (building == null)
                continue;
            if (targetTransform == null ||
                Vector3.Distance(transform.position, building.transform.position) <
                Vector3.Distance(transform.position, targetTransform.position))
                targetTransform = building.transform;
        }
        if (targetTransform == null)
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
    }
    
    void HandleMovement()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            rigidbody2d.velocity = moveDir * moveSpeed;
        }
        else
            rigidbody2d.velocity = Vector3.zero;
    }

    void HandleTargetting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0)
        {
            lookForTargetTimer = lookForTargetTimerMax;
            LookForTargets();
        }
    }

    void OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
