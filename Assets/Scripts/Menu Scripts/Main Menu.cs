using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject customizationMenu;
    public GameObject creditsMenu;
    public GameObject loreMenu;
    public Button loreButton;
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
        loreButton.onClick.AddListener(onLoreNextClicked);

    }

    private void onLoreNextClicked()
    {
        customizationMenu.SetActive(true);
        loreMenu.SetActive(false);
    }

    void OnStartMissionClicked()
    {
        loreMenu.SetActive(true);
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
