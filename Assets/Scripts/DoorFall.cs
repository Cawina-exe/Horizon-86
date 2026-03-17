using UnityEngine;

public class DoorFall : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    public float fallSpeed = 150f;

    private Transform player;
    private bool isFalling = false;
    private Quaternion targetRotation;


    private Collider[] allColliders;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        targetRotation = transform.rotation * Quaternion.Euler(0, 0, 90f);

      
   
        allColliders = GetComponentsInChildren<Collider>();
    }

    void Update()
    {
     
        if (isFalling)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, fallSpeed * Time.deltaTime);
            return; 
        }

    
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Door kicked down! Colliders disabled.");
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