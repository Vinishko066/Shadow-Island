using UnityEngine;

public class Movement_survivor : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;  // speed of the survivor

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get WASD input
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        moveInput.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        moveInput.Normalize(); // prevent faster diagonal movement

        // Update animation
        animator.SetBool("isMoving", moveInput != Vector2.zero);

        // Flip sprite horizontally based on X input
        if (moveInput.x != 0)
            sr.flipX = moveInput.x < 0;
    }

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
