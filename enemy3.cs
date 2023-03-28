using UnityEngine;

public class AreaPatrollingEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float projectileSpeed = 5f;
    public float projectileLifetime = 3f;
    public float projectileFireRate = 1f;
    public int damage = 1;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Vector2 patrolDirection = Vector2.right;
    private float fireTimer = 0f;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Move();
        Fire();
    }

    void Move()
    {
        rb.velocity = patrolDirection * speed * Time.deltaTime;
    }

    void Fire()
    {
        if (fireTimer <= 0f)
        {
            fireTimer = 1f / projectileFireRate;

            GameObject projectileGO = Instantiate(Resources.Load("Projectile"), transform.position, Quaternion.identity) as GameObject;
            EnemyProjectile projectile = projectileGO.GetComponent<EnemyProjectile>();
            projectile.SetDirection((playerTransform.position - transform.position).normalized);
            projectile.speed = projectileSpeed;
            projectile.projectileLifetime = projectileLifetime;
            projectile.damage = damage;
            projectile.playerLayer = playerLayer;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            patrolDirection *= -1f;
        }
    }
}