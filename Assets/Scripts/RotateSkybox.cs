using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How fast the sky rotates. Positive or negative numbers change the direction.")]
    public float rotateSpeed = 1.5f;

    void Update()
    {
        
        if (RenderSettings.skybox != null)
        {
           
            float currentRotation = RenderSettings.skybox.GetFloat("_Rotation");

            
            currentRotation += rotateSpeed * Time.deltaTime;

            
            currentRotation = Mathf.Repeat(currentRotation, 360f);

           
            RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
        }
    }
}