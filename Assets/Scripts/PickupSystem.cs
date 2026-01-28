using UnityEngine;
using System.Collections;

public class PickupSystem : MonoBehaviour
{
    [Header("Settings")]
    public float pickUpRange = 2f;
    public LayerMask itemLayer;
    public Animator playerAnimator;

    [Header("Inventory")]
    public bool hasKey = false; // This stores if we have the key!

    private bool isPickingUp = false;

    void Update()
    {
        if (isPickingUp) return;

        // Press E to interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickUpRange, itemLayer);

        foreach (var hit in hitColliders)
        {
            // Check if it is the Key
            if (hit.gameObject.name.Contains("Key") || hit.gameObject.tag == "Key")
            {
                StartCoroutine(PickupSequence(hit.gameObject));
                return;
            }
        }
    }

    IEnumerator PickupSequence(GameObject item)
    {
        isPickingUp = true;

        // 1. Play Animation
        if (playerAnimator != null) playerAnimator.SetTrigger("trigPickup");

        // 2. Wait for hand to go down (adjust this time to match your animation)
        yield return new WaitForSeconds(0.5f);

        // 3. "Store" the item
        hasKey = true;          // Remember we have it
        Destroy(item);          // Delete the object from the world (Hides it)
        Debug.Log("Key Picked Up! Go find the wall.");

        // 4. Wait for animation to finish
        yield return new WaitForSeconds(0.5f);
        isPickingUp = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}