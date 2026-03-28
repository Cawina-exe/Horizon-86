using UnityEngine;

public class FundManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject fundsObject; // Drag your FundsMonney object here!
    public int requiredNegotiations = 4;

    private int currentNegotiations = 0;

    void Start()
    {
        // Make sure the funds are hidden right when the level starts
        if (fundsObject != null) fundsObject.SetActive(false);
    }

    // The NPCs will call this function when you speak to them
    public void AddNegotiation()
    {
        currentNegotiations++;
        Debug.Log("Negotiated with: " + currentNegotiations + " out of " + requiredNegotiations);

        // Check if we hit the magic number!
        if (currentNegotiations >= requiredNegotiations)
        {
            Debug.Log("All negotiations complete! European Funds unlocked.");

            if (fundsObject != null)
            {
                fundsObject.SetActive(true);
            }
        }
    }
}