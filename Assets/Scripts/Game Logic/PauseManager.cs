using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public bool isPaused = false;
    public Canvas pauseCanvas;
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
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        isPaused = false;
        pauseCanvas.enabled = false;   
    }
}
