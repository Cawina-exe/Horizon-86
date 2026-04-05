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
   
        if (voiceAudio != null && !voiceAudio.isPlaying)
        {
            voiceAudio.Play();
            Debug.Log(gameObject.name + " says: Hello there!");

          
            MusicManager music = player.GetComponent<MusicManager>();
            if (music != null && voiceAudio.clip != null)
            {
              
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