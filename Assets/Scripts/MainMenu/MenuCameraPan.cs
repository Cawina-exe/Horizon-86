using UnityEngine;

public class MenuCameraPan : MonoBehaviour
{
    [Header("Cinematic Movement")]
    public float swaySpeed = 0.5f;   // How fast the camera pans
    public float swayAngle = 10f;    // How far left/right it turns

    private Quaternion startRotation;

    void Start()
    {
        // Remember exactly where you positioned the camera in the Scene view
        startRotation = transform.rotation;
    }

    void Update()
    {
        // Use a Sine wave to smoothly swing back and forth infinitely
        float angle = Mathf.Sin(Time.time * swaySpeed) * swayAngle;
        transform.rotation = startRotation * Quaternion.Euler(0, angle, 0);
    }
}