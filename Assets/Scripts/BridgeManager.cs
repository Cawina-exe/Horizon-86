using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    [Header("Bridge Objects")]
    public GameObject brokenBridge;
    public GameObject fullBridge;

    [Header("Requirements")]
    public int rocksNeeded = 3;
    public int currentRocks = 0;
    public float interactRange = 5f;
    public Transform player;

    private bool bridgeBuilt = false;

    void Update()
    {
        if (bridgeBuilt) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Logic: Close enough + Have 3 Rocks + Press E
        if (dist < interactRange && currentRocks >= rocksNeeded && Input.GetKeyDown(KeyCode.E))
        {
            BuildBridge();
        }
    }

    void BuildBridge()
    {
        bridgeBuilt = true;
        brokenBridge.SetActive(false); // Hide the broken one
        fullBridge.SetActive(true);    // Show the full one
        Debug.Log("Bridge built successfully!");
    }

    public void AddRock()
    {
        currentRocks++;
    }
}