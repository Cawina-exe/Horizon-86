using UnityEngine;

public class MenuCameraPan : MonoBehaviour
{
    [Header("Cinematic Movement")]
    public float swaySpeed = 0.5f;   
    public float swayAngle = 10f;   

    private Quaternion startRotation;

    void Start()
    {
       
        startRotation = transform.rotation;
    }

    void Update()
    {
        
        float angle = Mathf.Sin(Time.time * swaySpeed) * swayAngle;
        transform.rotation = startRotation * Quaternion.Euler(0, angle, 0);
    }
}