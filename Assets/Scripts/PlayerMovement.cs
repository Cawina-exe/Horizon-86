using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 8f;
    public Animator animator;
    public Transform feetPoint;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0, z).normalized;

        bool isMoving = direction.magnitude > 0.1f;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        // --- ANIMATION LOGIC ---
        if (animator != null)
        {
            // If we are moving and NOT holding shift -> Walking
            animator.SetBool("IsWalking", isMoving && !isSprinting);

            // If we are moving AND holding shift -> Running
            animator.SetBool("IsRunning", isMoving && isSprinting);
        }

        // --- MOVEMENT ---
        float currentSpeed = isSprinting ? runSpeed : walkSpeed;
        rb.velocity = new Vector3(direction.x * currentSpeed, rb.velocity.y, direction.z * currentSpeed);

        // --- ROTATION ---
        if (isMoving)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // --- JUMP ---
        isGrounded = Physics.CheckSphere(feetPoint.position, 0.2f, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            animator.SetTrigger("trigJump");
        }
    }
}