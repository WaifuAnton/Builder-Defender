using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float maxSearchRadius = 10;
    [SerializeField] float lookForTargetTimerMax = 0.2f;
    [SerializeField] float shootTimerMax = 0.2f;

    const int PROJECTILE_SPAWN_POSITION_INDEX = 2;

    Enemy targetEnemy;
    float lookForTargetTimer;
    float shootTimer;
    Vector3 projectileSpanPosition;

    private void Awake()
    {
        projectileSpanPosition = transform.GetChild(PROJECTILE_SPAWN_POSITION_INDEX).position;
    }

    private void Update()
    {
        HandleTargetting();
        HandleShooting();
    }

    void LookForTargets()
    {
        Collider2D[] colliders2d = Physics2D.OverlapCircleAll(transform.position, maxSearchRadius);
        foreach (Collider2D collider2d in colliders2d)
        {
            Enemy enemy = collider2d.GetComponent<Enemy>();
            if (enemy == null)
                continue;
            if (targetEnemy == null ||
                Vector3.Distance(transform.position, enemy.transform.position) <
                Vector3.Distance(transform.position, targetEnemy.transform.position))
                targetEnemy = enemy;
        }
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

    void HandleShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer > 0)
            return;
        shootTimer = shootTimerMax;
        if (targetEnemy != null)
            ArrowProjectile.Create(projectileSpanPosition, targetEnemy);
    }
}
