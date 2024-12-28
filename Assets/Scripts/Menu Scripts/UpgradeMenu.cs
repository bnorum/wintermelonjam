using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class UpgradeMenu : MonoBehaviour
{
    [Header("Upgrade 1")]
    public TextMeshProUGUI name1;
    public Button select1;
    public Image image1;
    [Header("Upgrade 2")]
    public TextMeshProUGUI name2;
    public Button select2;
    public Image image2;
    [Header("Upgrade 3")]
    public TextMeshProUGUI name3;
    public Button select3;
    public Image image3;
    public PlayerExperience pE;

    void Start()
    {
        select1.onClick.AddListener(On1Click);
        select2.onClick.AddListener(On2Click);
        select3.onClick.AddListener(On3Click);
    }

    private void On1Click()
    {
       pE.ReceiveUpgrade(0);
       gameObject.SetActive(false);
    }

    private void On2Click()
    {
        pE.ReceiveUpgrade(1);
        gameObject.SetActive(false);
    }

    private void On3Click()
    {
        pE.ReceiveUpgrade(2);
        gameObject.SetActive(false);
    }

    public void SetUpgrade(int num, string name, Sprite sprite)
    {
        switch(num)
        {
            case 1:
                name1.text = name;
                image1.sprite = sprite;
                break;
            case 2:
                name2.text = name;
                image2.sprite = sprite;
                break;
            case 3:
                name3.text = name;
                image3.sprite = sprite;
                break;
        }
    }
}
