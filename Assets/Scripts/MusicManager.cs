using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Settings")]
    public float fadeDuration = 1.5f;
    [Tooltip("How quiet the music gets when someone is talking (0 is silent, 1 is full volume)")]
    public float duckVolume = 0.15f;

    private AudioSource musicSource;
    private Coroutine currentFade;
    private Coroutine duckCoroutine;
    private float maxVolume;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        maxVolume = musicSource.volume; // Remembers your default volume setting
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return;

        if (currentFade != null) StopCoroutine(currentFade);
        if (duckCoroutine != null) StopCoroutine(duckCoroutine); // Stop ducking if we change zones

        currentFade = StartCoroutine(FadeRoutine(newClip));
    }

    IEnumerator FadeRoutine(AudioClip newClip)
    {
        if (musicSource.isPlaying)
        {
            while (musicSource.volume > 0)
            {
                musicSource.volume -= maxVolume * (Time.deltaTime / fadeDuration);
                yield return null;
            }
        }

        musicSource.clip = newClip;
        musicSource.Play();

        while (musicSource.volume < maxVolume)
        {
            musicSource.volume += maxVolume * (Time.deltaTime / fadeDuration);
            yield return null;
        }
        musicSource.volume = maxVolume;
    }

    // --- NEW DUCKING SYSTEM ---
    public void DuckMusic(float duration)
    {
        if (duckCoroutine != null) StopCoroutine(duckCoroutine);
        duckCoroutine = StartCoroutine(DuckRoutine(duration));
    }

    IEnumerator DuckRoutine(float duration)
    {
        float startVol = musicSource.volume;
        float timer = 0f;

        // 1. Fade down quickly (0.5 seconds)
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVol, duckVolume, timer / 0.5f);
            yield return null;
        }
        musicSource.volume = duckVolume;

        // 2. Wait for the audio clip to finish playing
        yield return new WaitForSeconds(duration);

        // 3. Fade back up smoothly (1 second)
        timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(duckVolume, maxVolume, timer / 1f);
            yield return null;
        }
        musicSource.volume = maxVolume;
    }
}