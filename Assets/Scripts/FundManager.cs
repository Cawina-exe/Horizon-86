using UnityEngine;

public class FundManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject fundsObject; 
    public int requiredNegotiations = 4;

    private int currentNegotiations = 0;

    void Start()
    {
        
        if (fundsObject != null) fundsObject.SetActive(false);
    }

   
    public void AddNegotiation()
    {
        currentNegotiations++;
        Debug.Log("Negotiated with: " + currentNegotiations + " out of " + requiredNegotiations);

       
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