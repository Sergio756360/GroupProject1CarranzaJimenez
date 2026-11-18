using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction talkAction;
    public GameObject projectilePrefab;
    public float speed = 3.0f;
    public float jumpForce = 7.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    private AudioSource audioSource;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private Vector2 move;
    private Vector2 moveDirection = Vector2.right;
    private int currentHealth;
    private bool isGrounded = true;
    private bool isInvincible;
    private float damageCooldown;

    public int health { get { return currentHealth; } }

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;

        if (talkAction != null)
            talkAction.Enable();
    }

    private void Update()
    {
        move = new Vector2(
            (Input.GetKey(KeyCode.D) ? 1.0f : 0.0f) - (Input.GetKey(KeyCode.A) ? 1.0f : 0.0f),
            0.0f);

        if (move.x != 0.0f)
            moveDirection = move;

        if (animator != null)
        {
            animator.SetFloat("Look X", moveDirection.x);
            animator.SetFloat("Look Y", moveDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2d.linearVelocity = new Vector2(rigidbody2d.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0.0f)
                isInvincible = false;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2d.linearVelocity = new Vector2(move.x * speed, rigidbody2d.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}

