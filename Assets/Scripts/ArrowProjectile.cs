using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20;
    [SerializeField] int damage = 10;
    [SerializeField] float timeToDie = 2;

    Enemy targetEnemy;
    Vector3 lastMoveDir;

    public static ArrowProjectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);
        ArrowProjectile arrowProjectile = arrowTransform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);
        return arrowProjectile;
    }

    private void Update()
    {
        Vector3 moveDir;
        if (targetEnemy != null)
        {
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDir = moveDir;
        }
        else
            moveDir = lastMoveDir;

        transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(moveDir));
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0)
            Destroy(gameObject);
    }

    void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
