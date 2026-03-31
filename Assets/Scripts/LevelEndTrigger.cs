using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // We need this to load the Main Menu!

public class LevelEndTrigger : MonoBehaviour
{
    [Header("Setup")]
    public GameObject creditsPanel; // Drag your Credits Panel here

    [Header("Settings")]
    public float creditsDuration = 6f; // How many seconds the credits stay on screen
    public string mainMenuSceneName = "MainMenu"; // Type the EXACT name of your menu scene here

    private bool isEnding = false;

    void Start()
    {
        // Automatically hide the credits panel when the level starts
        if (creditsPanel != null) creditsPanel.SetActive(false);
    }

    // This triggers the moment the player touches the invisible box
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
        // 1. Show the black credits screen instantly (hiding the fall)
        if (creditsPanel != null) creditsPanel.SetActive(true);

        // 2. Wait for the player to read the credits
        yield return new WaitForSeconds(creditsDuration);

        // 3. Load the Main Menu Scene!
        SceneManager.LoadScene(mainMenuSceneName);
    }
}