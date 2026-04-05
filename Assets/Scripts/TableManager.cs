using System.Collections;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [Header("Lamp Setup")]
    public GameObject[] displayLamps;
    public int lampsNeeded = 3;
    public int collectedLamps = 0;

    [Header("Finale Visuals (Green Energy)")]
    public GameObject[] energyBeams;
    public Transform blockingWall;
    public float slideDistance = 6f;
    public float slideSpeed = 2f;

    [Header("Finale Era Transition")]
    public Material newSkybox;
    public Color newFogColor;
    public Light mainSun;
    public Color newSunColor = Color.white;
    public float newSunIntensity = 1f;
    public float fadeDuration = 3f;

    [Header("Interaction & Audio")]
    public float interactRange = 4f;
    public AudioSource successSound;     
    public AudioSource wallOpenSound;   

    private Transform player;
    public bool isFinalActionDone = false;
    private Material oldSkybox;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

       
        if (successSound == null) successSound = GetComponent<AudioSource>();

        foreach (GameObject lamp in displayLamps)
            if (lamp != null) lamp.SetActive(false);

        foreach (GameObject beam in energyBeams)
            if (beam != null) beam.SetActive(false);
    }

    void Update()
    {
        if (isFinalActionDone) return;

        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (dist < interactRange && Input.GetKeyDown(KeyCode.E) && collectedLamps >= lampsNeeded)
            {
                isFinalActionDone = true;

               
                if (successSound != null) successSound.Play();

                Debug.Log("Green Transition Initiated!");
                StartCoroutine(FinaleSequence());
            }
        }
    }

    public void AddLamp()
    {
        if (collectedLamps < displayLamps.Length && displayLamps[collectedLamps] != null)
            displayLamps[collectedLamps].SetActive(true);

        collectedLamps++;
    }

    IEnumerator FinaleSequence()
    {
        foreach (GameObject beam in energyBeams)
        {
            if (beam != null) beam.SetActive(true);
        }

        
        oldSkybox = RenderSettings.skybox;
        float startExposure = oldSkybox.HasProperty("_Exposure") ? oldSkybox.GetFloat("_Exposure") : 1f;
        float startSunIntensity = mainSun != null ? mainSun.intensity : 1f;
        float halfTime = fadeDuration / 2f;
        float timer = 0f;

        while (timer < halfTime)
        {
            timer += Time.deltaTime;
            if (oldSkybox.HasProperty("_Exposure"))
                oldSkybox.SetFloat("_Exposure", Mathf.Lerp(startExposure, 0f, timer / halfTime));
            if (mainSun != null)
                mainSun.intensity = Mathf.Lerp(startSunIntensity, 0f, timer / halfTime);
            yield return null;
        }

       
        float currentRotation = oldSkybox.HasProperty("_Rotation") ? oldSkybox.GetFloat("_Rotation") : 0f;
        if (newSkybox.HasProperty("_Rotation")) newSkybox.SetFloat("_Rotation", currentRotation);
        if (newSkybox.HasProperty("_Exposure")) newSkybox.SetFloat("_Exposure", 0f);

        RenderSettings.skybox = newSkybox;
        RenderSettings.fogColor = newFogColor;
        if (mainSun != null) mainSun.color = newSunColor;

        if (oldSkybox.HasProperty("_Exposure")) oldSkybox.SetFloat("_Exposure", 1f);

        
        if (wallOpenSound != null) wallOpenSound.Play();

       
        timer = 0f;
        Vector3 wallTarget = blockingWall != null ? blockingWall.position + Vector3.down * slideDistance : Vector3.zero;

        while (timer < halfTime)
        {
            timer += Time.deltaTime;

            if (newSkybox.HasProperty("_Exposure"))
                newSkybox.SetFloat("_Exposure", Mathf.Lerp(0f, 1f, timer / halfTime));
            if (mainSun != null)
                mainSun.intensity = Mathf.Lerp(0f, newSunIntensity, timer / halfTime);

            if (blockingWall != null)
                blockingWall.position = Vector3.MoveTowards(blockingWall.position, wallTarget, slideSpeed * Time.deltaTime);

            yield return null;
        }

        if (newSkybox.HasProperty("_Exposure")) newSkybox.SetFloat("_Exposure", 1f);
        if (mainSun != null) mainSun.intensity = newSunIntensity;

        if (blockingWall != null)
        {
            while (Vector3.Distance(blockingWall.position, wallTarget) > 0.01f)
            {
                blockingWall.position = Vector3.MoveTowards(blockingWall.position, wallTarget, slideSpeed * Time.deltaTime);
                yield return null;
            }
        }
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