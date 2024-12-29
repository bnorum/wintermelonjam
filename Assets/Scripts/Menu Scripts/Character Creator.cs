using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    // Assign these in the Unity Inspector
    public Button MagnetButton;
    public Button GravityGunButton;
    public GameObject customization;
    public GameObject mainMenu;
    public Button back;
    public Button startButton;
    public TMP_Text descriptionText;
    public TMP_Text weaponNameText;
    public TMP_Text TitleText;

    // Descriptions for the buttons
    [TextArea]
    public string MagnetDescription = "Throw magnetic Boomerangs, then recall them to deal damage.";
    [TextArea]
    public string GravityGunDescription = "Double Barreled Gravity Gun.\nLeft Click shoots an exploding round, Right Click shoots an imploding round.";

    // Default texts
    public string DefaultWeaponName = "Default Weapon Name";
    public string DefaultDescription = "Hover over a button to see its description.";

    void Start()
    {
        descriptionText.text = DefaultDescription;
        weaponNameText.text = DefaultWeaponName;
        TitleText.gameObject.SetActive(true);
        AddHoverListeners(MagnetButton, MagnetDescription, "Boomerang");
        AddHoverListeners(GravityGunButton, GravityGunDescription, "Impulser");
        MagnetButton.onClick.AddListener(() => OnMagnetClick());
        GravityGunButton.onClick.AddListener(() => OnGravityGunClick());
        back.onClick.AddListener(() => OnBackClick());
        startButton.onClick.AddListener(() => OnStartClick());
    }

    void AddHoverListeners(Button button, string description, string weaponName)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        pointerEnter.callback.AddListener((eventData) =>
        {
            descriptionText.text = description;
            weaponNameText.text = weaponName;
            weaponNameText.gameObject.SetActive(true);
            descriptionText.gameObject.SetActive(true);
            TitleText.gameObject.SetActive(false);
        });

        EventTrigger.Entry pointerExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        pointerExit.callback.AddListener((eventData) =>
        {
            descriptionText.text = DefaultDescription;
            weaponNameText.text = DefaultWeaponName;
            weaponNameText.gameObject.SetActive(false);
            descriptionText.gameObject.SetActive(false);
            TitleText.gameObject.SetActive(true);
        });

        trigger.triggers.Add(pointerEnter);
        trigger.triggers.Add(pointerExit);
    }

    void OnMagnetClick()
    {
        LoadingParameters.weaponAbility = 0;
        weaponNameText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        TitleText.text = "Weapon Selected:\n Boomerang";
        TitleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }

    void OnGravityGunClick()
    {
        LoadingParameters.weaponAbility = 1;
        weaponNameText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        TitleText.text = "Weapon Selected:\n Impulser";
        TitleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }
    private void OnBackClick()
    {
        customization.SetActive(false);
        mainMenu.SetActive(true);      
    }
    private void OnStartClick()
    {
        customization.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");   
    }
}
