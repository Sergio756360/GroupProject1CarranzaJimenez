using UnityEngine;

public class AnimationMovement : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rb; // Added to apply physical jump force if needed

    public float jumpForce = 7f;
    private bool isGrounded = true;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Trigger jump only when Space is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerAnim.SetTrigger("Jump_trig");
            isGrounded = false;

            // rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }


        float moveInput = Input.GetAxisRaw("Horizontal");
        playerAnim.SetFloat("Speed_f", Mathf.Abs(moveInput));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
