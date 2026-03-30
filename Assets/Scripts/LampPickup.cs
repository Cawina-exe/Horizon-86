using UnityEngine;

public class LampPickup : MonoBehaviour
{
    [Header("Setup")]
    public TableManager tableManager; 
    public float interactRange = 3f;
    public AudioSource pickupSound;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
            
                if (pickupSound != null && pickupSound.clip != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound.clip, transform.position);
                }

                
                if (tableManager != null) tableManager.AddLamp();

                
                gameObject.SetActive(false);
            }
        }
    }
}