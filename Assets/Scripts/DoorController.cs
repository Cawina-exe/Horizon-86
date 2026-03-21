using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;
    public PickupSystem playerInventory;

    [Header("Settings")]
    public float openRange = 4.0f;
    public float slideAmount = 5f;
    public float openSpeed = 2f;

    [Header("Audio")]
    public AudioSource openSound; // <-- NEW: The audio slot!

    private bool isOpening = false;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position + Vector3.down * slideAmount;

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerInventory == null) playerInventory = player.GetComponent<PickupSystem>();

        // Auto-find the Audio Source if you forget to drag it in
        if (openSound == null) openSound = GetComponent<AudioSource>();
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

                // --- NEW: Play the heavy stone sound! ---
                if (openSound != null)
                {
                    openSound.Play();
                }

                // --- NEW: Turn off the collider so you can walk through! ---
                Collider wallCollider = GetComponent<Collider>();
                if (wallCollider != null) wallCollider.enabled = false;
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, openRange);
    }
}