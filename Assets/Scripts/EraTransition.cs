using System.Collections;
using UnityEngine;

public class EraTransition : MonoBehaviour
{
    [Header("New Era Settings")]
    public Material newSkybox;
    public Color newFogColor;
    public Color newSunColor = Color.white;
    public float newSunIntensity = 1f;

    [Header("Transition Settings")]
    public float fadeDuration = 3f;
    public Light mainSun;

  
    private Material oldSkybox;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (newSkybox != null && RenderSettings.skybox != newSkybox)
            {
                StartCoroutine(TransitionRoutine());
            }
        }
    }

    IEnumerator TransitionRoutine()
    {
       
        oldSkybox = RenderSettings.skybox;

        float startExposure = oldSkybox.HasProperty("_Exposure") ? oldSkybox.GetFloat("_Exposure") : 1f;
        float startSunIntensity = mainSun != null ? mainSun.intensity : 1f;

        float halfTime = fadeDuration / 2f;
        float timer = 0f;

       
        while (timer < halfTime)
        {
            timer += Time.deltaTime;
            float percent = timer / halfTime;

            if (oldSkybox.HasProperty("_Exposure"))
                oldSkybox.SetFloat("_Exposure", Mathf.Lerp(startExposure, 0f, percent));

            if (mainSun != null)
                mainSun.intensity = Mathf.Lerp(startSunIntensity, 0f, percent);

            yield return null;
        }

        
        float currentRotation = oldSkybox.HasProperty("_Rotation") ? oldSkybox.GetFloat("_Rotation") : 0f;
        if (newSkybox.HasProperty("_Rotation"))
            newSkybox.SetFloat("_Rotation", currentRotation);

        if (newSkybox.HasProperty("_Exposure"))
            newSkybox.SetFloat("_Exposure", 0f);

        RenderSettings.skybox = newSkybox;
        RenderSettings.fogColor = newFogColor;
        if (mainSun != null) mainSun.color = newSunColor;

       
        if (oldSkybox.HasProperty("_Exposure"))
        {
            oldSkybox.SetFloat("_Exposure", 1f);
        }

        timer = 0f;
        while (timer < halfTime)
        {
            timer += Time.deltaTime;
            float percent = timer / halfTime;

            if (newSkybox.HasProperty("_Exposure"))
                newSkybox.SetFloat("_Exposure", Mathf.Lerp(0f, 1f, percent));

            if (mainSun != null)
                mainSun.intensity = Mathf.Lerp(0f, newSunIntensity, percent);

            yield return null;
        }

        if (newSkybox.HasProperty("_Exposure")) newSkybox.SetFloat("_Exposure", 1f);
        if (mainSun != null) mainSun.intensity = newSunIntensity;
    }

   
    void OnApplicationQuit()
    {
        if (RenderSettings.skybox != null && RenderSettings.skybox.HasProperty("_Exposure"))
            RenderSettings.skybox.SetFloat("_Exposure", 1f);

        if (newSkybox != null && newSkybox.HasProperty("_Exposure"))
            newSkybox.SetFloat("_Exposure", 1f);

        if (oldSkybox != null && oldSkybox.HasProperty("_Exposure"))
            oldSkybox.SetFloat("_Exposure", 1f);
    }
}