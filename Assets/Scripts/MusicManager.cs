using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Settings")]
    public float fadeDuration = 1.5f; 

    private AudioSource musicSource;
    private Coroutine currentFade;
    private float maxVolume;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        maxVolume = musicSource.volume; 
    }

   
    public void ChangeMusic(AudioClip newClip)
    {
      
        if (musicSource.clip == newClip) return;

        
        if (currentFade != null) StopCoroutine(currentFade);

       
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
}