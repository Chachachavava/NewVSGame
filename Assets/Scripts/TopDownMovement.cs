using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    [Header("Input Keys")]
    public KeyCode dashKey = KeyCode.Space;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private float dashCooldownLeft = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        HandleDash();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = movement * dashSpeed;
        }
        else if (movement != Vector2.zero)
        {
            rb.velocity = movement * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void HandleDash()
    {
        if (dashCooldownLeft > 0)
        {
            dashCooldownLeft -= Time.deltaTime;
        }
        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                dashCooldownLeft = dashCooldown;
            }
        }
        else if (Input.GetKeyDown(dashKey) && dashCooldownLeft <= 0 && movement != Vector2.zero)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
        }
    }
    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = isDashing ? Color.red : Color.green;
            Gizmos.DrawRay(transform.position, movement * 2f);
        }
    }
}