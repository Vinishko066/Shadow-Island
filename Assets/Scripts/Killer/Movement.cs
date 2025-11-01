using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float changeDirectionTime = 2f; // seconds before picking a new random direction

    private Vector2 moveDirection;
    private float speed;
    private float timer;

    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        PickNewDirection();
    }

    void Update()
    {
        // Move NPC
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Update animation parameter
        animator.SetBool("isMoving", moveDirection != Vector2.zero);

        // Flip sprite based on X direction
        if (moveDirection.x != 0)
            sr.flipX = moveDirection.x < 0;

        // Change direction after timer
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            timer = 0f;
            PickNewDirection();
        }
    }

    void PickNewDirection()
    {
        // Random direction in X and Y (-1, 0, or 1)
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
