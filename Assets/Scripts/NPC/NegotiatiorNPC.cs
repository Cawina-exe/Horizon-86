using UnityEngine;

public class NegotiatorNPC : MonoBehaviour
{
    [Header("Setup")]
    public FundManager manager; // Drag your FundManager empty object here!
    private Transform player;

    [Header("Settings")]
    public float interactRange = 3f;
    public bool hasSpoken = false; // Keeps track so you can't talk to the same guy 4 times

    [Header("Audio (Optional)")]
    public AudioSource talkSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (talkSound == null) talkSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If we already secured his vote/funds, stop checking for input
        if (hasSpoken) return;

        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Negotiation successful!");
                hasSpoken = true;

                // Play their voice line if they have one
                if (talkSound != null) talkSound.Play();

                // Tell the Manager to add +1 to the score!
                if (manager != null) manager.AddNegotiation();
            }
        }
    }
}