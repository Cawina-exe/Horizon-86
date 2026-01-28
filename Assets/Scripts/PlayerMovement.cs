using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 8f;
    public float rotateSpeed = 10f;

    [Header("Ground Check")]
    public Transform feetPoint;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. INPUT
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(x, 0, z).normalized;

        // Check Input Status
        bool isMoving = moveDirection.magnitude > 0.01f;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // 2. EXCLUSIVE ANIMATION LOGIC (The Fix)
        if (animator != null)
        {
            // Walk is ONLY true if moving AND NOT sprinting
            animator.SetBool("IsWalking", isMoving && !isSprinting);

            // Run is ONLY true if moving AND sprinting
            animator.SetBool("IsRunning", isMoving && isSprinting);
        }

        // 3. JUMP
        if (feetPoint != null)
            isGrounded = Physics.CheckSphere(feetPoint.position, groundRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            if (animator != null) animator.SetTrigger("trigJump");
        }

        // 4. PHYSICS & ROTATION
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }

        float currentSpeed = isSprinting ? runSpeed : walkSpeed;
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
    }
}