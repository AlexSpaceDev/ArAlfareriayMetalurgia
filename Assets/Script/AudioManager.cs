using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource sfxSource;   // Sonidos de UI / botones
    public AudioSource bgmSource;   // Música de fondo

    [Header("Background Music")]
    public AudioClip backgroundMusic; // Música de fondo opcional

    /// Reproducir un sonido de botón (o cualquier SFX)
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    /// Reproducir música en loop
    public void PlayBGM(AudioClip music)
    {
        bgmSource.clip = music;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    private void Start()
    {
        if (backgroundMusic != null)
            PlayBGM(backgroundMusic);
    }

}
