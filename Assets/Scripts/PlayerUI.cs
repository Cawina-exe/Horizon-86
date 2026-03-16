using UnityEngine;
using TMPro; // Required for TextMeshPro

public class PlayerUI : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject interactUIObj;      
    public TextMeshProUGUI interactText; 
    public float lookRange = 4.0f;        

    private PickupSystem inventory;

    void Start()
    {
      
        inventory = GetComponent<PickupSystem>();

        
        if (interactUIObj != null) interactUIObj.SetActive(false);
    }

    void Update()
    {
        
        Collider[] hits = Physics.OverlapSphere(transform.position, lookRange);
        bool targetFound = false;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Key"))
            {
                interactText.text = "Press [E] to pick up Key";
                targetFound = true;
                break; 
            }
            else if (hit.CompareTag("Rock"))
            {
                interactText.text = "Press [E] to collect Rock";
                targetFound = true;
                break;
            }
            else if (hit.CompareTag("Wall"))
            {
        
                if (inventory != null && inventory.hasKey)
                    interactText.text = "Press [E] to open Wall";
                else
                    interactText.text = "Wall Locked (Find the Key)";

                targetFound = true;
                break;
            }
            else if (hit.CompareTag("Bridge"))
            {
                interactText.text = "Press [E] to rebuild Bridge";
                targetFound = true;
                break;
            }
        }

   
        if (interactUIObj != null)
        {
            interactUIObj.SetActive(targetFound);
        }
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRange);
    }
}