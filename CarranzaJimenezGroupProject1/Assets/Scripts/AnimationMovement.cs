using UnityEngine;

public class AnimationMovement : MonoBehaviour
{
    private Animator playerAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim.SetTrigger("Jump_trig");
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);

        float moveInput = Input.GetAxisRaw("Horizontal");
        playerAnim.SetFloat("Speed_f", Mathf.Abs(moveInput));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
