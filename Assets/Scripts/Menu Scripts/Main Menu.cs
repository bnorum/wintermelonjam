using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject customizationMenu;
    public GameObject creditsMenu;
    public Button startMission;
    public Button creditScreen;
    public Button quit;


    void Start()
    {
        mainMenu.SetActive(true);
        customizationMenu.SetActive(false);
        startMission.onClick.AddListener(OnStartMissionClicked);
        quit.onClick.AddListener(OnQuitClicked);
        creditScreen.onClick.AddListener(OnCreditScreenClicked);

    }



    void OnStartMissionClicked()
    {
        customizationMenu.SetActive(true);
        mainMenu.SetActive(false); 
    }
    
    void OnQuitClicked()
    {
        Application.Quit(); // Works in a built application        
    }
    void OnCreditScreenClicked()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);     
    }
}
