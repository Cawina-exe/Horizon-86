using System.Collections;
using UnityEngine;

public class NegotiatorNPC : MonoBehaviour
{
    [Header("Setup")]
    public FundManager manager;
    private Transform player;

    [Header("Settings")]
    public float interactRange = 3f;

    // We split this into two states now!
    public bool isNegotiating = false;
    public bool negotiationComplete = false;

    [Header("Audio")]
    public AudioSource talkSound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (talkSound == null) talkSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If we are currently talking, or already finished, do nothing.
        if (isNegotiating || negotiationComplete) return;

        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(NegotiationRoutine());
            }
        }
    }

    IEnumerator NegotiationRoutine()
    {
        isNegotiating = true; // Lock the interaction

        if (talkSound != null && talkSound.clip != null)
        {
            talkSound.Play();
            // Wait for the exact length of the audio file!
            yield return new WaitForSeconds(talkSound.clip.length);
        }
        else
        {
            // Fallback just in case you forget the audio
            yield return new WaitForSeconds(2f);
        }

        // The audio is done! Now we complete it.
        negotiationComplete = true;
        isNegotiating = false;

        if (manager != null) manager.AddNegotiation();
    }
}