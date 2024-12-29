using UnityEngine;

public class BossApproachingScaler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.Abs((Mathf.Sin(Time.time))*3);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
