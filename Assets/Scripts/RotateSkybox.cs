using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast the sky rotates. Positive or negative numbers change the direction.")]
    public float rotateSpeed = 1.5f;

    void Update()
    {
        // 1. Check if we actually have a skybox material assigned in the scene
        if (RenderSettings.skybox != null)
        {
            // 2. Get the current rotation of the skybox
            float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");

            // 3. Add a small amount to the rotation based on our speed and time
            currentRotation += rotateSpeed * Time.deltaTime;

            // 4. Keep the number between 0 and 360 so it loops perfectly
            currentRotation = Mathf.Repeat(currentRotation, 360f);

            // 5. Apply the new rotation back to the skybox material
            RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
        }
    }
}