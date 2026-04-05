using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuPanel;
    public GameObject contactsPanel;

    [Header("Skybox Time-Lapse")]
    [Tooltip("Drag your 4 skyboxes here in order (1986, 2006, 2016, 2026)")]
    public Material[] eraSkyboxes;
    public float timePerSkybox = 5f;   
    public float transitionSpeed = 2f;  

    [Header("Lighting")]
    public Light mainSun; 

    private int currentSkyIndex = 0;

    void Start()
    {
       
        if (eraSkyboxes != null && eraSkyboxes.Length > 0)
        {
            RenderSettings.skybox = eraSkyboxes[0];
            StartCoroutine(TimeLapseRoutine());
        }
    }

    IEnumerator TimeLapseRoutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(timePerSkybox);

          
            float startIntensity = mainSun != null ? mainSun.intensity : 1f;
            float timer = 0f;

            while (timer < transitionSpeed)
            {
                timer += Time.deltaTime;
                if (mainSun != null)
                    mainSun.intensity = Mathf.Lerp(startIntensity, 0f, timer / transitionSpeed);
                yield return null;
            }

           
            currentSkyIndex++;
            if (currentSkyIndex >= eraSkyboxes.Length) currentSkyIndex = 0; 
            RenderSettings.skybox = eraSkyboxes[currentSkyIndex];

         
            timer = 0f;
            while (timer < transitionSpeed)
            {
                timer += Time.deltaTime;
                if (mainSun != null)
                    mainSun.intensity = Mathf.Lerp(0f, startIntensity, timer / transitionSpeed);
                yield return null;
            }
        }
    }

  

    public void PlayGame()
    {
        SceneManager.LoadScene("Horizon86");
    }

    public void OpenContacts()
    {
        menuPanel.SetActive(false);
        contactsPanel.SetActive(true);
    }

    public void CloseContacts()
    {
        contactsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quitting...");
        Application.Quit();
    }
}