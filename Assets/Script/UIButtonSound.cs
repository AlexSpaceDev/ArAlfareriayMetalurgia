using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip sound;

    public void PlaySound()
    {
        AudioManager manager = FindFirstObjectByType<AudioManager>();
        manager.PlaySFX(sound);
    }
}
