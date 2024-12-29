using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PauseManager : MonoBehaviour
{

    public bool isPaused = false;
    public Canvas pauseCanvas;
    public TextMeshProUGUI upgradeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
        pauseCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
        isPaused = true;
        pauseCanvas.enabled = true;
        upgradeText.text = DisplayUpgrades();
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        isPaused = false;
        pauseCanvas.enabled = false;   
    }

    public string DisplayUpgrades() {
        
        Dictionary<string, int> upgradeCounts = new Dictionary<string, int>();

        foreach (var upgrade in PlayerStats.Singleton.collectedUpgrades) {
            if (upgradeCounts.ContainsKey(upgrade.upgradeName)) {
            upgradeCounts[upgrade.upgradeName]++;
            } else {
            upgradeCounts[upgrade.upgradeName] = 1;
            }
        }

        string upgradeList = "";
        foreach (var upgrade in upgradeCounts) {
            if (upgrade.Value > 1) {
            upgradeList += upgrade.Value + "x " + upgrade.Key + "\n";
            } else {
            upgradeList += upgrade.Key + "\n";
            }
        }

        return ("Upgrades List:\n" + upgradeList);
    }
}
