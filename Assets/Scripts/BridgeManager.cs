using System.Collections;
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

    [Header("Animation Settings")]
    public float fallDistance = 5f; // How deep the broken bridge sinks
    public float fallSpeed = 3f;    // How fast it sinks

    private bool bridgeBuilt = false;

    void Update()
    {
        // If it's already built, do nothing
        if (bridgeBuilt) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // Logic: Close enough + Have 3 Rocks + Press E
        if (dist < interactRange && currentRocks >= rocksNeeded && Input.GetKeyDown(KeyCode.E))
        {
            bridgeBuilt = true; // Lock it so you can't press E twice

            // Start the cinematic sequence!
            StartCoroutine(BuildBridgeSequence());
        }
    }

    // A Coroutine allows us to pause time and wait for animations to finish
    IEnumerator BuildBridgeSequence()
    {
        Debug.Log("Bridge sequence started: Sinking old bridge...");

        // 1. Turn off the broken bridge's colliders so the player doesn't get dragged down with it
        Collider[] brokenColliders = brokenBridge.GetComponentsInChildren<Collider>();
        foreach (Collider col in brokenColliders)
        {
            col.enabled = false;
        }

        // Calculate where the broken bridge needs to go
        Vector3 startPos = brokenBridge.transform.position;
        Vector3 targetPos = startPos - new Vector3(0, fallDistance, 0);

        // 2. Animate the broken bridge falling down
        while (Vector3.Distance(brokenBridge.transform.position, targetPos) > 0.01f)
        {
            brokenBridge.transform.position = Vector3.MoveTowards(brokenBridge.transform.position, targetPos, fallSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }

        // 3. Hide the broken bridge completely once it's underwater
        brokenBridge.SetActive(false);

        // 4. Activate the full bridge! 
        // (Because you have RiseUpObject.cs on it, it will automatically rise up right now!)
        fullBridge.SetActive(true);

        Debug.Log("Bridge rebuilt! Onward to 2006.");
    }

    public void AddRock()
    {
        currentRocks++;
    }
}