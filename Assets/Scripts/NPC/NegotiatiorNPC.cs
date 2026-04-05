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
        isNegotiating = true; /

        if (talkSound != null && talkSound.clip != null)
        {
         
            MusicManager music = player.GetComponent<MusicManager>();
            if (music != null)
            {
                music.DuckMusic(talkSound.clip.length);
            }

            talkSound.Play();

           
            yield return new WaitForSeconds(talkSound.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }

        
        negotiationComplete = true;
        isNegotiating = false;

        if (manager != null) manager.AddNegotiation();
    }
}