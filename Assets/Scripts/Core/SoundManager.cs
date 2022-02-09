using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        // Doesn't duplicate on new level
        if (instance == null)
        {
            instance = this;
            // Won't destroy when switched to new level
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject); // duplicates
        }
    }
    public void PlaySound(AudioClip _sound)
    {
        if(_sound != null)
            source.PlayOneShot(_sound);
    }
}
