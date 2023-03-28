using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    // Movimentação
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    // Inventário
    public int maxInventorySize = 10;
    private List<Item> inventory = new List<Item>();

    // Ataque
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimentação
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // Ataque
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void AddToInventory(Item item)
    {
        if (inventory.Count < maxInventorySize)
        {
            inventory.Add(item);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveFromInventory(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
        }
    }

    public void UseInventoryItem(Item item)
    {
        item.Use();
        RemoveFromInventory(item);
    }

    public void TakeDamage(int damage)
    {
        // Lógica para lidar com o dano ao jogador
    }
}
