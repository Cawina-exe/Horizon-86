using System.Collections;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    [Header("Lamp Setup")]
    public GameObject[] displayLamps;
    public int lampsNeeded = 3;
    public int collectedLamps = 0;

    [Header("Finale Visuals (Green Energy)")]
    [Tooltip("Put glowing lights or particle effects here to turn on at the end!")]
    public GameObject[] energyBeams;
    [Tooltip("The big wall blocking the 2026 area")]
    public Transform blockingWall;
    public float slideDistance = 6f;
    public float slideSpeed = 2f;

    [Header("Finale Era Transition (2016 -> 2026)")]
    public Material newSkybox;
    public Color newFogColor;
    public Light mainSun; // Drag your Directional Light here
    public Color newSunColor = Color.white;
    public float newSunIntensity = 1f;
    public float fadeDuration = 3f;

    [Header("Interaction Settings")]
    public float interactRange = 4f;
    public AudioSource successSound;

    private Transform player;
    public bool isFinalActionDone = false;
    private Material oldSkybox;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (successSound == null) successSound = GetComponent<AudioSource>();

        // Hide lamps and energy beams at the start
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

            // If close + Press E + Has all 3 lamps!
            if (dist < interactRange && Input.GetKeyDown(KeyCode.E) && collectedLamps >= lampsNeeded)
            {
                isFinalActionDone = true;
                if (successSound != null) successSound.Play();
                Debug.Log("Green Transition Initiated!");

                // START THE CINEMATIC CUTSCENE!
                StartCoroutine(FinaleSequence());
            }
        }
    }

    // Called instantly when you press E on a world lamp
    public void AddLamp()
    {
        if (collectedLamps < displayLamps.Length && displayLamps[collectedLamps] != null)
            displayLamps[collectedLamps].SetActive(true);

        collectedLamps++;
    }

    IEnumerator FinaleSequence()
    {
        // 1. Turn on the Green Energy Beams!
        foreach (GameObject beam in energyBeams)
        {
            if (beam != null) beam.SetActive(true);
        }

        // 2. FADE TO BLACK
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

        // 3. SWAP THE ERA (in the dark)
        float currentRotation = oldSkybox.HasProperty("_Rotation") ? oldSkybox.GetFloat("_Rotation") : 0f;
        if (newSkybox.HasProperty("_Rotation")) newSkybox.SetFloat("_Rotation", currentRotation);
        if (newSkybox.HasProperty("_Exposure")) newSkybox.SetFloat("_Exposure", 0f);

        RenderSettings.skybox = newSkybox;
        RenderSettings.fogColor = newFogColor;
        if (mainSun != null) mainSun.color = newSunColor;

        // Secretly fix the old skybox
        if (oldSkybox.HasProperty("_Exposure")) oldSkybox.SetFloat("_Exposure", 1f);

        // 4. FADE BACK UP AND SLIDE THE WALL DOWN
        timer = 0f;
        Vector3 wallTarget = blockingWall != null ? blockingWall.position + Vector3.down * slideDistance : Vector3.zero;

        while (timer < halfTime)
        {
            timer += Time.deltaTime;

            // Brighten sky
            if (newSkybox.HasProperty("_Exposure"))
                newSkybox.SetFloat("_Exposure", Mathf.Lerp(0f, 1f, timer / halfTime));
            if (mainSun != null)
                mainSun.intensity = Mathf.Lerp(0f, newSunIntensity, timer / halfTime);

            // Slide wall
            if (blockingWall != null)
                blockingWall.position = Vector3.MoveTowards(blockingWall.position, wallTarget, slideSpeed * Time.deltaTime);

            yield return null;
        }

        // 5. SAFETY LOCKS
        if (newSkybox.HasProperty("_Exposure")) newSkybox.SetFloat("_Exposure", 1f);
        if (mainSun != null) mainSun.intensity = newSunIntensity;

        // Finish sliding the wall if it's slow
        if (blockingWall != null)
        {
            while (Vector3.Distance(blockingWall.position, wallTarget) > 0.01f)
            {
                blockingWall.position = Vector3.MoveTowards(blockingWall.position, wallTarget, slideSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    // Safety fix for Unity Editor stopping
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