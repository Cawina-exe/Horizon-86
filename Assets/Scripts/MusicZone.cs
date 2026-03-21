using UnityEngine;

public class MusicZone : MonoBehaviour
{
    [Header("Track for this Area")]
    public AudioClip areaMusic;

    void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Player"))
        {
            
            MusicManager manager = other.GetComponent<MusicManager>();
            if (manager != null && areaMusic != null)
            {
                manager.ChangeMusic(areaMusic);
            }
        }
    }
}