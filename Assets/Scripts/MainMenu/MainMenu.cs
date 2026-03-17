using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject contactsPanel;

   
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