using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CharacterCreator : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject customizationMenu;
    
    public Button gameStart;
    public Button back;
    [Header("Movement")]
    public Button leftArrowMovement;
    public Button rightArrowMovement;
    public List<Sprite> movementImages = new List<Sprite>();
    public Image movementSpriteHolder;
    private int currentMovement = 0;

    [Header("Weapons")]
    public Button leftArrowWeapon;
    public Button rightArrowWeapon;
    public List<Sprite> weaponImages = new List<Sprite>();
    public Image weaponSpriteHolder;
    private int currentWeapon = 0;
    void Start()
    {
        gameStart.onClick.AddListener(OnGameStart);
        back.onClick.AddListener(OnBackClick);
        leftArrowMovement.onClick.AddListener(OnLeftMovement);
        rightArrowMovement.onClick.AddListener(OnRightMovement);
        leftArrowWeapon.onClick.AddListener(OnLeftWeapon);
        rightArrowWeapon.onClick.AddListener(OnRightWeapon);
    }

    private void OnBackClick()
    {
        customizationMenu.SetActive(false);
        mainMenu.SetActive(true);      
    }

    private void OnGameStart()
    {
        LoadingParameters.movementAbility = currentMovement;
        LoadingParameters.weaponAbility = currentWeapon;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    private void OnRightWeapon()
    {
        currentWeapon ++;
        if(currentWeapon > weaponImages.Count-1)
        {
            currentWeapon = 0;
        }
        weaponSpriteHolder.sprite = weaponImages[currentWeapon];
    }

    private void OnLeftWeapon()
    {
        currentWeapon --;
        if(currentWeapon < 0)
        {
            currentWeapon = weaponImages.Count-1;
        }
        weaponSpriteHolder.sprite = weaponImages[currentWeapon];
    }

    private void OnRightMovement()
    {
        currentMovement ++;
        if(currentMovement > movementImages.Count-1)
        {
            currentMovement = 0;
        }
        movementSpriteHolder.sprite = movementImages[currentMovement];
    }

    private void OnLeftMovement()
    {
        currentMovement --;
        if(currentMovement < 0)
        {
            currentMovement = movementImages.Count-1;
        }
        movementSpriteHolder.sprite = movementImages[currentMovement];
    }
}
