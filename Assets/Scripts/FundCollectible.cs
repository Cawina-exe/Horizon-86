using System.Collections;
using UnityEngine;

public class FundCollectible : MonoBehaviour
{
    [Header("Setup")]
    public float interactRange = 5f; 

    [Tooltip("Drag your road panels here IN THE EXACT ORDER they should appear!")]
    public Transform[] pathPanels;

    [Header("Rising Animation Settings")]
    public float riseDistance = 5f; 
    public float riseSpeed = 4f;    
    public float delayBetweenPanels = 0.3f; 
    private Transform player;
    private bool isCollected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

      
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

        
            if (dist < interactRange && Input.GetKeyDown(KeyCode.E))
            {
                isCollected = true;
                Debug.Log("Funds Collected! Building the rising path...");

                MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes) mesh.enabled = false;

                Collider col = GetComponent<Collider>();
                if (col != null) col.enabled = false;

             
                StartCoroutine(BuildPathSequence());
            }
        }
    }


    IEnumerator BuildPathSequence()
    {
        foreach (Transform panel in pathPanels)
        {
            if (panel != null)
            {
                panel.gameObject.SetActive(true); 

             
                StartCoroutine(RisePanel(panel));

            
                yield return new WaitForSeconds(delayBetweenPanels);
            }
        }
    }

  
    IEnumerator RisePanel(Transform panel)
    {
        
        Vector3 targetPos = panel.position + new Vector3(0, riseDistance, 0);

     
        while (Vector3.Distance(panel.position, targetPos) > 0.01f)
        {
            panel.position = Vector3.MoveTowards(panel.position, targetPos, riseSpeed * Time.deltaTime);
            yield return null;
        }

    
        panel.position = targetPos;
    }
}