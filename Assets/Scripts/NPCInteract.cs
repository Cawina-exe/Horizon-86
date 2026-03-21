using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    public AudioSource voiceAudio;

    private Transform player;

    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;

       
        if (voiceAudio == null) voiceAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
     
        float dist = Vector3.Distance(transform.position, player.position);

       
        if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
    }

    void Talk()
    {
        // Only play the audio if it is NOT already playing
        if (voiceAudio != null && !voiceAudio.isPlaying)
        {
            voiceAudio.Play();
            Debug.Log(gameObject.name + " says: Hello there!");

            // --- NEW: Tell the music to duck! ---
            MusicManager music = player.GetComponent<MusicManager>();
            if (music != null && voiceAudio.clip != null)
            {
                // Send the exact length of the audio clip to the Music Manager
                music.DuckMusic(voiceAudio.clip.length);
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}