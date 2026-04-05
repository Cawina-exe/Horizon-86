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
    public float fallDistance = 5f; 
    public float fallSpeed = 3f;    

    private bool bridgeBuilt = false;

    void Update()
    {
        
        if (bridgeBuilt) return;

        float dist = Vector3.Distance(transform.position, player.position);

       
        if (dist < interactRange && currentRocks >= rocksNeeded && Input.GetKeyDown(KeyCode.E))
        {
            bridgeBuilt = true; 

          
            StartCoroutine(BuildBridgeSequence());
        }
    }

    
    IEnumerator BuildBridgeSequence()
    {
        Debug.Log("Bridge sequence started: Sinking old bridge...");

        Collider[] brokenColliders = brokenBridge.GetComponentsInChildren<Collider>();
        foreach (Collider col in brokenColliders)
        {
            col.enabled = false;
        }

        
        Vector3 startPos = brokenBridge.transform.position;
        Vector3 targetPos = startPos - new Vector3(0, fallDistance, 0);

     
        while (Vector3.Distance(brokenBridge.transform.position, targetPos) > 0.01f)
        {
            brokenBridge.transform.position = Vector3.MoveTowards(brokenBridge.transform.position, targetPos, fallSpeed * Time.deltaTime);
            yield return null; 
        }

    
        brokenBridge.SetActive(false);

     
        fullBridge.SetActive(true);

        Debug.Log("Bridge rebuilt! Onward to 2006.");
    }

    public void AddRock()
    {
        currentRocks++;
    }
}