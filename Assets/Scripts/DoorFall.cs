using UnityEngine;

public class DoorFall : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    public float fallSpeed = 150f;

    [Header("Audio & Visuals")]
    public AudioSource crashSound;
    public ParticleSystem dustParticles; // The new particle slot!

    private Transform player;
    private bool isFalling = false;
    private bool hasHitGround = false; // Keeps track so it only plays once
    private Quaternion targetRotation;

    private Collider[] allColliders;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetRotation = transform.rotation * Quaternion.Euler(0, 0, 90f);
        allColliders = GetComponentsInChildren<Collider>();

        if (crashSound == null) crashSound = GetComponent<AudioSource>();
        if (dustParticles != null) dustParticles.Stop(); // Ensure it's off at the start
    }

    void Update()
    {
        // 1. If the door is falling...
        if (isFalling)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, fallSpeed * Time.deltaTime);

            // 2. IMPACT CHECK: If the angle between its current rotation and the target is basically 0...
            if (!hasHitGround && Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                hasHitGround = true; // Lock it so it doesn't trigger 60 times a second

                // BOOM! Play the effects!
                if (crashSound != null) crashSound.Play();
                if (dustParticles != null) dustParticles.Play();

                Debug.Log("Door hit the ground! Dust and Sound triggered.");
            }

            return;
        }

        // 3. Waiting for player to kick the door
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Door kicked!");
                isFalling = true;
                DisableColliders();
            }
        }
    }

    void DisableColliders()
    {
        foreach (Collider col in allColliders)
        {
            col.enabled = false;
        }
    }
}