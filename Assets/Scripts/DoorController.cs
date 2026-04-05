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
    public AudioSource openSound; 

    private bool isOpening = false;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position + Vector3.down * slideAmount;

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerInventory == null) playerInventory = player.GetComponent<PickupSystem>();

    
        if (openSound == null) openSound = GetComponent<AudioSource>();
    }

    void Update()
    {
      
        float dist = Vector3.Distance(transform.position, player.position);

     
        if (dist < openRange && Input.GetKeyDown(KeyCode.E) && !isOpening)
        {
            if (playerInventory.hasKey)
            {
                Debug.Log("Key Used! Opening Wall.");
                isOpening = true;

             
                if (openSound != null)
                {
                    openSound.Play();
                }

             
                Collider wallCollider = GetComponent<Collider>();
                if (wallCollider != null) wallCollider.enabled = false;
            }
            else
            {
                Debug.Log("Locked! You need the Key.");
            }
        }

       
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