using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;      // Make sure this is NOT 0 in Inspector
    public float jumpForce = 10f;
    public float rotateSpeed = 10f;

    [Header("Ground Check")]
    public Transform feetPoint;       // Drag the "Pes" (Feet) object here
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;     // Select "Chao" or "Default" here

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Safety check: verify Rigidbody exists
        if (rb == null) Debug.LogError("Player is missing a Rigidbody component!");
    }

    void Update()
    {
        // 1. INPUT (WASD is automatic in Unity)
        float x = Input.GetAxisRaw("Horizontal"); // A / D
        float z = Input.GetAxisRaw("Vertical");   // W / S

        // Calculate direction
        moveDirection = new Vector3(x, 0, z).normalized;

        // Debugging: If you press keys, this should show up in Console
        if (moveDirection.magnitude > 0.1f)
        {
            // Debug.Log("Keys pressed: " + moveDirection); 
        }

        // 2. JUMP (Space bar)
        // Check if feet are touching the ground layer
        if (feetPoint != null)
        {
            isGrounded = Physics.CheckSphere(feetPoint.position, groundRadius, groundLayer);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // 3. ROTATION (Look where you walk)
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // 4. PHYSICS MOVEMENT
        // Apply velocity to X and Z, keep Y (gravity)
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }
}