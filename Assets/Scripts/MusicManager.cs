using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern: keep only one MusicManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);   // 🔹 survive scene changes

        audioSource = GetComponent<AudioSource>();

        // Just in case settings weren’t set in Inspector
        audioSource.loop = true;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    // Optional: call this later to change volume from a settings menu
    public void SetVolume(float volume)
    {
        if (audioSource != null)
            audioSource.volume = volume;
    }

    // Optional: call this to switch tracks (e.g., boss theme, ending)
    public void SwitchTrack(AudioClip newClip, bool restartIfSame = false)
    {
        if (audioSource == null) return;

        if (!restartIfSame && audioSource.clip == newClip)
            return;

        audioSource.clip = newClip;
        audioSource.Play();
    }
}