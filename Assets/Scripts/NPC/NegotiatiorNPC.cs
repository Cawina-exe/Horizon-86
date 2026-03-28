using System.Collections;
using UnityEngine;

public class NegotiatorNPC : MonoBehaviour
{
    [Header("Setup")]
    public FundManager manager;
    private Transform player;

    [Header("Settings")]
    public float interactRange = 3f;

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
            // --- NEW: Audio Ducking! ---
            // Find the Music Manager on the Player and tell it how long to stay quiet
            MusicManager music = player.GetComponent<MusicManager>();
            if (music != null)
            {
                music.DuckMusic(talkSound.clip.length);
            }

            // Play the negotiation audio
            talkSound.Play();

            // Wait for the exact length of the audio file
            yield return new WaitForSeconds(talkSound.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }

        // The audio is done! Complete the negotiation.
        negotiationComplete = true;
        isNegotiating = false;

        if (manager != null) manager.AddNegotiation();
    }
}