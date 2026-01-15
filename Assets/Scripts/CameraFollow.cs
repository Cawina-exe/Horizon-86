using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Drag your Player here
    public Vector3 offset;         // The distance between camera and player
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate where the camera should be
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move there
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}