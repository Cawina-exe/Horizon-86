using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Settings")]
    public float openSpeed = 2f;
    public float slideAmount = 5f; // How many meters down (or up) it moves
    public bool moveDown = true;   // Check true to go down (into ground), false to go up

    private bool isOpening = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Calculate where the door should end up
        float direction = moveDown ? -1f : 1f;
        targetPosition = transform.position + new Vector3(0, slideAmount * direction, 0);
    }

    void Update()
    {
        // If the door is unlocked, slide it smoothly
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);
        }
    }

    // This detects when the player touches the wall
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player has the key
            PickupSystem playerInventory = other.GetComponent<PickupSystem>();

            if (playerInventory != null && playerInventory.hasKey == true)
            {
                Debug.Log("Key used! Opening Wall.");
                isOpening = true;
                // Optional: Destroy the wall entirely if you prefer disintegration
                // Destroy(gameObject, 2f); 
            }
            else
            {
                Debug.Log("Locked! You need the Key.");
            }
        }
    }
}