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
            else if (hit.CompareTag("Negotiator"))
            {
                NegotiatorNPC npc = hit.GetComponent<NegotiatorNPC>();
                if (npc != null)
                {
                    if (npc.negotiationComplete)
                        interactText.text = "Negotiation complete.";
                    else if (npc.isNegotiating)
                        interactText.text = "Listening..."; // Shows while the audio plays!
                    else
                        interactText.text = "Press [E] to Negotiate";
                }
                targetFound = true;
                break;
            }
            else if (hit.CompareTag("Funds"))
            {
                interactText.text = "Press [E] to collect European Funds";
                targetFound = true;
                break;
            }
            // ---> THE EXTRA '}' WAS RIGHT HERE. I REMOVED IT! <---
            else if (hit.CompareTag("Bridge"))
            {
                BridgeManager bridge = hit.GetComponent<BridgeManager>();

                if (bridge != null)
                {
                    if (bridge.currentRocks >= bridge.rocksNeeded)
                    {
                        interactText.text = "Press [E] to build the bridge";
                    }
                    else
                    {
                        interactText.text = "Find the 3 rocks to build the bridge";
                    }
                }

                targetFound = true;
                break;
            }
            else if (hit.CompareTag("NPC"))
            {
                interactText.text = "Press [E] to Talk";
                targetFound = true;
                break;
            }
            else if (hit.CompareTag("Phone"))
            {
                interactText.text = "Press [E] to Listen";
                targetFound = true;
                break;
            }
            else if (hit.CompareTag("Door"))
            {
                interactText.text = "Press [E] to Open Door";
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