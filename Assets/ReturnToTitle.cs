using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ReturnToTitle : MonoBehaviour
{

    public Sprite medal1;
    public Sprite medal2;
    public Image medalImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Singleton.usingBoomberang)
        {
            if (medalImage != null)
            {
                
            medalImage.sprite = medal1;
            }
        }
        else
        {
            if (medalImage != null)
            {
                
            medalImage.sprite = medal1;
            }
        }
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("Menu");
    }
}
