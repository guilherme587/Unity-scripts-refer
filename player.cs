using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variáveis de movimento
    public float speed = 5f;
    public float jumpForce = 7f;
    public float doubleJumpForce = 5f;
    private bool isGrounded;
    private bool canDoubleJump;

    // Variáveis de animação
    private Animator animator;
    private bool isMoving;

    // Variáveis de física
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica se o jogador está no chão
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Ground"));

        // Verifica se o jogador pode realizar pulo duplo
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // Movimentação horizontal
        float move = Input.GetAxis("Horizontal");
        if (move != 0)
        {
            isMoving = true;
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);
        }
        else
        {
            isMoving = false;
        }
        transform.position += new Vector3(move * speed * Time.deltaTime, 0, 0);

        // Pulo
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                    canDoubleJump = false;
                }
            }
        }

        // Atualiza as animações
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", rb.velocity.y);
    }
}
