using System.Collections;
using UnityEngine;

public class FundCollectible : MonoBehaviour
{
    [Header("Setup")]
    public float interactRange = 5f; // Increased default range so you can easily pick it up!

    [Tooltip("Drag your road panels here IN THE EXACT ORDER they should appear!")]
    public Transform[] pathPanels;

    [Header("Rising Animation Settings")]
    public float riseDistance = 5f; // How deep underground they start
    public float riseSpeed = 4f;    // How fast they slide up
    public float delayBetweenPanels = 0.3f; // The "staircase" delay between each block

    private Transform player;
    private bool isCollected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Step 1: Hide all panels AND secretly move them underground right when the game starts
        foreach (Transform panel in pathPanels)
        {
            if (panel != null)
            {
                panel.gameObject.SetActive(false);
                panel.position = panel.position - new Vector3(0, riseDistance, 0);
            }
        }
    }

    void Update()
    {
        if (isCollected) return;

        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            // Interaction check
            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
                isCollected = true;
                Debug.Log("Funds Collected! Building the rising path...");

                // Hide the money visual but keep the script running
                MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes) mesh.enabled = false;

                Collider col = GetComponent<Collider>();
                if (col != null) col.enabled = false;

                // Start the master sequence
                StartCoroutine(BuildPathSequence());
            }
        }
    }

    // This coroutine acts as the "Director", pointing at each panel one by one
    IEnumerator BuildPathSequence()
    {
        foreach (Transform panel in pathPanels)
        {
            if (panel != null)
            {
                panel.gameObject.SetActive(true); // Turn it on (it is still underground)

                // Tell this specific panel to start rising!
                StartCoroutine(RisePanel(panel));

                // Wait a fraction of a second before starting the next panel
                yield return new WaitForSeconds(delayBetweenPanels);
            }
        }
    }

    // This coroutine handles the actual sliding animation for a single panel
    IEnumerator RisePanel(Transform panel)
    {
        // Calculate where it is supposed to stop (back to its original starting height)
        Vector3 targetPos = panel.position + new Vector3(0, riseDistance, 0);

        // Slide it smoothly up until it reaches the target
        while (Vector3.Distance(panel.position, targetPos) > 0.01f)
        {
            panel.position = Vector3.MoveTowards(panel.position, targetPos, riseSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap it perfectly into place at the end just to be safe
        panel.position = targetPos;
    }
}