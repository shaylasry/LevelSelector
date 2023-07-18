using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic()
    {
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }
}
