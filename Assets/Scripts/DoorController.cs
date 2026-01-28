using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;        // Drag your Player object here!
    public PickupSystem playerInventory; // Drag your Player object here too!

    [Header("Settings")]
    public float openRange = 4.0f;  // How close you need to be
    public float slideAmount = 5f;
    public float openSpeed = 2f;

    private bool isOpening = false;
    private Vector3 targetPos;

    void Start()
    {
        // Calculate where the door goes (Down into the ground)
        targetPos = transform.position + Vector3.down * slideAmount;

        // Auto-find scripts if you forgot to drag them in
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerInventory == null) playerInventory = player.GetComponent<PickupSystem>();
    }

    void Update()
    {
        // 1. Check Distance
        float dist = Vector3.Distance(transform.position, player.position);

        // 2. Logic: If close + Press E + Has Key + Not already open
        if (dist < openRange && Input.GetKeyDown(KeyCode.E) && !isOpening)
        {
            if (playerInventory.hasKey)
            {
                Debug.Log("Key Used! Opening Wall.");
                isOpening = true;
            }
            else
            {
                Debug.Log("Locked! You need the Key.");
            }
        }

        // 3. Move the Wall
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, openSpeed * Time.deltaTime);
        }
    }

    // Draw a red circle in the Scene view to show the range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, openRange);
    }
}