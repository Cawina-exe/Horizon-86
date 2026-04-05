using UnityEngine;
using System.Collections;

public class PickupSystem : MonoBehaviour
{
    [Header("Settings")]
    public float pickUpRange = 2f;
    public LayerMask itemLayer;
    public Animator playerAnimator;

    [Header("Inventory")]
    public bool hasKey = false; 

    private bool isPickingUp = false;

    void Update()
    {
        if (isPickingUp) return;

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

      
        if (playerAnimator != null) playerAnimator.SetTrigger("trigPickup");

      
        yield return new WaitForSeconds(0.5f);

       
        hasKey = true;          
        Destroy(item);         
        Debug.Log("Key Picked Up! Go find the wall.");

      
        yield return new WaitForSeconds(0.5f);
        isPickingUp = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}