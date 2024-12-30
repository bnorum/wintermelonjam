using System;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    public GameObject mainMenu;
     public Button back;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        back.onClick.AddListener(OnQuitClicked); 
    }

    private void OnQuitClicked()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
