using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    //assign these in prefab
    public float maxExperience;
    private float currentExperience;

    void Start()
    {
        currentExperience = 0;
    }
    public void OnCollisionStay2D(Collision2D collision) {

        if (collision.gameObject.tag == "Experience")
        {
            currentExperience += collision.gameObject.GetComponent<xpOrb>().value;
        }
    }
    void Update()
    {
        if(currentExperience >= maxExperience)
        {
            //GiveUpgrade();
        }
    }
}
