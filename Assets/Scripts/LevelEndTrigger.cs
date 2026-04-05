using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Setup")]
    public GameObject creditsPanel;

    [Header("Settings")]
    public float creditsDuration = 6f; 
    public string mainMenuSceneName = "MainMenu"; 

    private bool isEnding = false;

    void Start()
    {
        
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnding)
        {
            isEnding = true;
            Debug.Log("Player took the leap! Rolling credits...");

            StartCoroutine(RollCreditsSequence());
        }
    }

    IEnumerator RollCreditsSequence()
    {
     
        if (creditsPanel != null) creditsPanel.SetActive(true);

       
        yield return new WaitForSeconds(creditsDuration);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}