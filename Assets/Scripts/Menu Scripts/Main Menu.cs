using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject customizationMenu;
    public Button credits;
    public Button quit;


    void Start()
    {
        mainMenu.SetActive(true);
        customizationMenu.SetActive(false);
        // startGame.onClick.AddListener(OnStartButtonClicked);
        credits.onClick.AddListener(OnCreditsClicked);
        quit.onClick.AddListener(OnQuitClicked);
    }

    void OnStartButtonClicked()
    {
      //credits

    }

    void OnCreditsClicked()
    {
        Debug.Log("OnPlayGameClicked");
        customizationMenu.SetActive(true);
        mainMenu.SetActive(false); 
    }
    
    void OnQuitClicked()
    {
        Application.Quit(); // Works in a built application        
    }
}
