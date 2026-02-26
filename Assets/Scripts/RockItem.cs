using UnityEngine;

public class RockItem : MonoBehaviour
{
    public float collectRange = 3f;
    private Transform player;
    private BridgeManager bridgeManager;

    void Start()
    {
        // Find the player and the manager automatically
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bridgeManager = FindFirstObjectByType<BridgeManager>();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        // Check if player is close AND presses E
        if (dist < collectRange && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (bridgeManager != null)
        {
            bridgeManager.AddRock();
            Debug.Log("Rock collected with E!");
            Destroy(gameObject);
        }
    }
}