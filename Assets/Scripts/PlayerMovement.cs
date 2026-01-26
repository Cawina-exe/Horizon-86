using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 10f;
    public float rotateSpeed = 10f;

    [Header("Ground Check")]
    public Transform feetPoint;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator animator; // Drag the Animator component here

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // If you forgot to drag it in, we try to find it automatically
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. INPUT
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, 0, z).normalized;

        // 2. ANIMATION LOGIC (The new part!)
        // If the direction magnitude is > 0, it means we are moving
        bool isMoving = moveDirection.magnitude > 0.01f;

        if (animator != null)
        {
            animator.SetBool("IsWalking", isMoving);
        }

        // 3. JUMP
        if (feetPoint != null)
        {
            isGrounded = Physics.CheckSphere(feetPoint.position, groundRadius, groundLayer);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            // Optional: If you have a Jump animation, trigger it here
            // animator.SetTrigger("Jump"); 
        }

        // 4. ROTATION
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // 5. PHYSICS MOVEMENT
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }
}