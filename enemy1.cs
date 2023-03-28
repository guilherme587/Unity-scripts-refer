using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 1;

    private Rigidbody2D rb;
    private Vector2 direction;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}

// public class ProjectileEnemy : MonoBehaviour
// {
//     public float speed = 3f;
//     public int damage = 1;

//     private Rigidbody2D rb;
//     private Vector2 direction;
//     private Transform player;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     void Update()
//     {
//         
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             PlayerController player = other.GetComponent<PlayerController>();
//             if (player != null)
//             {
//                 player.TakeDamage(damage);
//                 Destroy(gameObject);
//             }
//         }
//     }
// }
