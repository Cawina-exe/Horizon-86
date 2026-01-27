using UnityEngine;
using System.Collections; // Needed for Coroutines (Timing)

public class PickupSystem : MonoBehaviour
{
    [Header("Settings")]
    public Transform holdPoint;
    public float pickUpRange = 2f;
    public LayerMask itemLayer;
    public Animator playerAnimator; // Drag your Animator here

    [Header("Timing")]
    public float pickupDelay = 0.6f; // How long to wait before object sticks to hand

    private GameObject heldObject;
    private bool isPickingUp = false; // Prevents spamming E

    void Update()
    {
        // Prevent moving or interacting while animation is playing
        if (isPickingUp) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickup()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickUpRange, itemLayer);

        foreach (var hit in hitColliders)
        {
            // Found an item! Start the animation sequence
            StartCoroutine(PickupSequence(hit.gameObject));
            return;
        }
    }

    // This is a "Coroutine" - it allows us to pause code execution
    IEnumerator PickupSequence(GameObject item)
    {
        isPickingUp = true; // Lock input

        // 1. Play Animation
        if (playerAnimator != null) playerAnimator.SetTrigger("trigPickup");

        // 2. Wait for the hand to reach the ground
        // (Adjust "pickupDelay" in Inspector to match your animation speed)
        yield return new WaitForSeconds(pickupDelay);

        // 3. Attach object (The Logic)
        heldObject = item;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        heldObject.GetComponent<Collider>().enabled = false;
        heldObject.transform.SetParent(holdPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        // 4. Wait for animation to finish standing up before moving again
        yield return new WaitForSeconds(0.5f);

        isPickingUp = false; // Unlock input
    }

    void DropObject()
    {
        heldObject.transform.SetParent(null);

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        heldObject.GetComponent<Collider>().enabled = true;
        heldObject = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}