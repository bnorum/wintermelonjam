using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject customizationMenu;
    public Button playGame;
    public Button howToPlay;
    public Button credits;
    public Button quit;


    void Start()
    {
        mainMenu.SetActive(true);
        customizationMenu.SetActive(false);
        playGame.onClick.AddListener(OnPlayGameClicked);
        howToPlay.onClick.AddListener(OnHowToPlayClicked);
        credits.onClick.AddListener(OnCreditsClicked);
        quit.onClick.AddListener(OnQuitClicked);
    }

    void OnPlayGameClicked()
    {
        customizationMenu.SetActive(true);
        mainMenu.SetActive(false);  
    }

    void OnHowToPlayClicked()
    {
        //how to play
    }

    void OnCreditsClicked()
    {
        //credits
    }
    
    void OnQuitClicked()
    {
        Application.Quit(); // Works in a built application        
    }
}
