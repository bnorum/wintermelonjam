using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    //assign these in prefab
    public float maxExperience;
    public float currentExperience;

    void Start()
    {
        currentExperience = 0;
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "XP")
        {
            Debug.Log("proper collision");
            currentExperience += collision.gameObject.GetComponent<xpOrb>().value;
            Destroy(collision.gameObject);
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
